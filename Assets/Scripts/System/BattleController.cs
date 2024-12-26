using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleController : MonoBehaviour
{
    [SerializeField] private BattleModel _battleModel;
    [SerializeField] private BattleView _battleView;
    [SerializeField] private CharacterUIView _characterUIView;
    [SerializeField] private CommandPanelView _firstCommandPanelView, _commandPanelView;
    [SerializeField] private SkillPanelView _skillPanelView;
    [SerializeField] private ItemPanelView _itemPanelView;
    [SerializeField] private Skill _skillController;
    
    [SerializeField] private List<CharacterDataSO> _characterDataList;
    [SerializeField] private List<ItemData> _playerItems; // プレイヤーのアイテムリスト
    
    private int _currentCharacterIndex = 0;
    private int _movedCharacterIndex = 0;
    
    private List<CharacterModel> players = new List<CharacterModel>();
    private List<CharacterModel> enemies = new List<CharacterModel>();
    private void Start()
    {
        // 初期化
        InitializeBattle();
    }

    /// <summary>
    /// プレイヤーと敵の初期データ設定を行う
    /// </summary>
    private void InitializeBattle()
    {
        //データ設定
        players = new List<CharacterModel>
        {
            new CharacterModel(_characterDataList[0]),
            new CharacterModel(_characterDataList[1]),
            new CharacterModel(_characterDataList[2]),
            new CharacterModel(_characterDataList[3])
        };

        enemies = new List<CharacterModel>
        {
            new CharacterModel(_characterDataList[4]),
        };

        _battleModel = new BattleModel(players, enemies);
        
        if (_battleView == null)
        {
            Debug.LogError("BattleView is not assigned!");
            return;
        }
        
        //UI更新
        _battleView.UpdateAllViews(_battleModel.Players, _battleModel.Enemies);
        _itemPanelView.InitializeItemPanel(_playerItems, OnItemSelected);
        
        StartBattleFlow();
    }
    
    /// <summary>
    /// バトル開始演出
    /// </summary>
    private void StartBattleFlow()
    {
        _battleView.ShowBattleStartAnimation(() => { ShowInitialCommand(); });
    }
    
    /// <summary>
    /// 戦闘前のパネルを開き、初期化を行う
    /// </summary>
    private void ShowInitialCommand()
    {
        //_characterUIView.Show();
        _movedCharacterIndex = 0;
        _firstCommandPanelView.Show();
        _firstCommandPanelView.Initialize(
            null,
            OnFightSelected,
            OnEscapeSelected,
            null,
            null
        );
    }
    
    /// <summary>
    ///　「たたかう」が選択された場合
    /// </summary>
    private void OnFightSelected()
    {
        _firstCommandPanelView.Hide();
        StartCharacterTurn();
    }
    
    /// <summary>
    /// 「逃げる」が選択された場合
    /// </summary>
    private void OnEscapeSelected()
    {
        int num = Random.Range(1, 100);
        if (num <= 5)
        {
            //TODO: 逃走演出を行う
            Debug.Log("Player escaped!");
            EndBattle();
        }
        else
        {
            //TODO: 「にげられなかった」とパネルを表示したあとに、ShowInitialCommand()に戻る
            Debug.Log("Player can't escape!");
        }
    }
    
    /// <summary>
    /// 次のキャラクターへ
    /// </summary>
    private void StartCharacterTurn()
    {
        //Indexがプレイヤーとして登録されているキャラクターの人数を超えたら、戦闘処理フェーズを開始する
        if (_currentCharacterIndex >= _battleModel.Players.Count)
        {
            ExecuteBattleActions();
            return;
        }

        //Debug.Log("Action charactor" + _currentCharacterIndex);
        //行動するキャラクターの情報を渡しつつ、行動選択開始
        var character = _battleModel.Players[_currentCharacterIndex];
        ShowCommandSelection(character);
    }
    
    /// <summary>
    /// コマンドの選択を行う
    /// </summary>
    private void ShowCommandSelection(CharacterModel character)
    {
        _commandPanelView.Show(); //コマンドパネルを開く
        _commandPanelView.Reset();
        _commandPanelView.Initialize(
            character.Sprite2,
            () => OnAttackSelected(character),
            () => ShowSkillSelected(character),
            () => OnDefendSelected(character),
            () => ShowItemSelected(character)
        );
    }
    
    /// <summary>
    /// 「こうげき」ボタンに割り当てられる処理
    /// </summary>
    private void OnAttackSelected(CharacterModel character)
    {
        _commandPanelView.Hide();
        _battleModel.EnqueueAction(new CharacterAction { ActionType = ActionTypeEnum.Attack, TargetIndex = 0 });
        _currentCharacterIndex++;
        StartCharacterTurn();
    }
    
    
    /// <summary>
    /// 「スキル」ボタンに割り当てられる処理
    /// </summary>
    private void ShowSkillSelected(CharacterModel character)
    {
        _skillPanelView.Show();
        _skillPanelView.Initialize(character, character.Skills, OnSkillSelected);
        _commandPanelView.Hide();
    }

    /// <summary>
    /// スキルパネルでスキルが選択されたら呼び出される処理。次のキャラクターの行動選択へ進める
    /// </summary>
    private void OnSkillSelected()
    {
        _skillPanelView.Hide();
        _battleModel.EnqueueAction(new CharacterAction { ActionType = ActionTypeEnum.Skill, Skill = _skillPanelView.SelectedSkill});
        _currentCharacterIndex++;
        StartCharacterTurn();
    }
    
    /// <summary>
    /// 「防御」ボタンに割り当てられる処理
    /// </summary>
    private void OnDefendSelected(CharacterModel character)
    {
        _commandPanelView.Hide();
        character.Speed += 20;
        _battleModel.EnqueueAction(new CharacterAction { ActionType = ActionTypeEnum.Guard });
        _currentCharacterIndex++;
        StartCharacterTurn();
    }
    
    /// <summary>
    /// 「アイテム」ボタンに割り当てられる処理
    /// </summary>
    private void ShowItemSelected(CharacterModel character)
    {
        _itemPanelView.Show();
        _itemPanelView.UpdateItemQuantities();
        _commandPanelView.Hide();
    }
    
    /// <summary>
    /// アイテムパネルでスキルが選択されたら呼び出される処理。次のキャラクターの行動選択へ進める
    /// </summary>
    private void OnItemSelected(ItemData itemData)
    {
        _itemPanelView.Hide();
        _battleModel.EnqueueAction(new CharacterAction { ActionType = ActionTypeEnum.Item, Item = _itemPanelView.SelectedItem });
        _currentCharacterIndex++;
        StartCharacterTurn();
    }

    /// <summary>
    /// 戦闘処理フェーズ
    /// </summary>
    private void ExecuteBattleActions()
    {
        while (_battleModel.ActionsQueue.Count > 0)
        {
            var action = _battleModel.DequeueAction();
            ProcessAction(action);
        }
        
        _currentCharacterIndex = 0;
        ShowInitialCommand();
    }
    
    /// <summary>
    /// 各行動の処理
    /// </summary>
    private void ProcessAction(CharacterAction action)
    {
        Action executeAction = action.ActionType switch
        {
            ActionTypeEnum.Attack => () => ExecuteAttackCommand(0),
            ActionTypeEnum.Skill => () => ExecuteSkillCommand(action.Skill),
            ActionTypeEnum.Guard => () => ExecuteGuardCommand(),
            ActionTypeEnum.Item => () => ExecuteItemCommand(action.Item),
            _ => () => Debug.LogError("未対応のアクションタイプです"),
        };
        executeAction();
    }
    
    /// <summary>
    /// 「こうげき」コマンドの処理
    /// </summary>
    public void ExecuteAttackCommand(int targetIndex)
    {
        if (targetIndex < 0 || targetIndex >= enemies.Count)
        {
            Debug.LogError("無効なターゲットインデックスです！");
            return;
        }

        CharacterModel target = enemies[targetIndex];
        target.TakeDamage(players[_movedCharacterIndex].CalculateDamage(target.Defense));
        Debug.Log(target.HP);

        // エフェクトやアニメーションを追加（例: エフェクト再生）
        ShowAttackEffect(target);
        _battleView.UpdateAllViews(_battleModel.Players, _battleModel.Enemies);
        
        //次のキャラクターへ進む
        _movedCharacterIndex++;
    }

    private void ShowAttackEffect(CharacterModel target)
    {
        Debug.Log($"{target.Name}に攻撃エフェクトを再生！");
        // TODO: 攻撃エフェクトやアニメーションを実装
    }

    /// <summary>
    /// 「スキル」コマンドの処理
    /// </summary>
    public void ExecuteSkillCommand(SkillDataSO skill)
    {
        _skillController.SkillActivate(skill, players[_movedCharacterIndex], enemies[0]);
        _battleView.UpdateAllViews(_battleModel.Players, _battleModel.Enemies);
        
        //次のキャラクターへ進む
        _movedCharacterIndex++;
    }

    /// <summary>
    /// 「防御」コマンドの処理
    /// </summary>
    public void ExecuteGuardCommand()
    {
        players[_movedCharacterIndex].Speed -= 20;
        Debug.Log($"{players[_movedCharacterIndex].Name} は身を守った");
        
        //次のキャラクターへ進む
        _movedCharacterIndex++;
    }

    /// <summary>
    /// 「アイテム」コマンドの処理
    /// </summary>
    public void ExecuteItemCommand(ItemData item)
    {
        Debug.Log($"{players[_movedCharacterIndex].Name} はアイテムを使用した");
        
        //次のキャラクターへ進む
        _movedCharacterIndex++;
    }
    
    /// <summary>
    /// バトル終了
    /// </summary>
    private void EndBattle()
    {
        
    }
    
}