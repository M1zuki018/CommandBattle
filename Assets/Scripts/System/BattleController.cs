using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BattleController : MonoBehaviour
{
    [SerializeField] private BattleModel _battleModel;
     [SerializeField] private BattleView _battleView;
    [SerializeField] private CommandPanelView _commandPanelView;
    [SerializeField] private SkillPanelView _skillPanelView;
    [SerializeField] private ItemPanelView _itemPanelView;

    private int _currentCharacterIndex = 0;
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
        var players = new List<CharacterModel>
        {
            new CharacterModel("Hero", 100, 20, 10),
            new CharacterModel("Mage", 80, 25, 5)
        };

        var enemies = new List<CharacterModel>
        {
            new CharacterModel("Slime", 50, 10, 5),
            new CharacterModel("Goblin", 70, 15, 8)
        };

        _battleModel = new BattleModel(players, enemies);
        
        if (_battleView == null)
        {
            Debug.LogError("BattleView is not assigned!");
            return;
        }
        
        _battleView.UpdateAllViews(_battleModel.Players, _battleModel.Enemies);
        
        StartBattleFlow();
    }
    
    /// <summary>
    /// バトル開始演出
    /// </summary>
    private void StartBattleFlow()
    {
        ShowInitialCommand();
        
        _battleView.ShowBattleStartAnimation(() =>
        {
            ShowInitialCommand();
        });
        
    }
    
    /// <summary>
    /// 戦闘前のパネルを開き、初期化を行う
    /// </summary>
    private void ShowInitialCommand()
    {
        _commandPanelView.Show();
        _commandPanelView.Initialize(
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
        _commandPanelView.Hide();
        StartCharacterTurn();
    }
    
    /// <summary>
    /// 「逃げる」が選択された場合
    /// </summary>
    private void OnEscapeSelected()
    {
        // 逃げる処理
        Debug.Log("Player escaped!");
        EndBattle();
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

        var character = _battleModel.Players[_currentCharacterIndex];
        ShowCommandSelection(character);
    }
    
    /// <summary>
    /// コマンドの選択を行う
    /// </summary>
    private void ShowCommandSelection(CharacterModel character)
    {
        _commandPanelView.Show(); //コマンドパネルを開く
        _commandPanelView.Initialize(
            () => OnAttackSelected(character),
            () => OnSkillSelected(character),
            () => OnDefendSelected(character),
            () => OnItemSelected(character)
        );
    }
    
    /// <summary>
    /// 「こうげき」コマンド
    /// </summary>
    private void OnAttackSelected(CharacterModel character)
    {
        _commandPanelView.Hide();
        _battleModel.EnqueueAction(new CharacterAction { ActionType = ActionTypeEnum.Attack, TargetIndex = 0 });
        _currentCharacterIndex++;
        StartCharacterTurn();
    }
    
    
    /// <summary>
    /// 「スキル」コマンド
    /// </summary>
    private void OnSkillSelected(CharacterModel character)
    {
        /*
        _skillPanelView.Show();
        _skillPanelView.Initialize(skillName =>
        {
            _skillPanelView.Hide();
            _battleModel.EnqueueAction(new CharacterAction { ActionType = ActionTypeEnum.Skill, SkillName = skillName, TargetIndex = 0 });
            _currentCharacterIndex++;
            StartCharacterTurn();
        });
        */
    }
    
    /// <summary>
    /// 「防御」コマンド
    /// </summary>
    private void OnDefendSelected(CharacterModel character)
    {
        _commandPanelView.Hide();
        _battleModel.EnqueueAction(new CharacterAction { ActionType = ActionTypeEnum.Guard });
        _currentCharacterIndex++;
        StartCharacterTurn();
    }
    
    /// <summary>
    /// 「アイテム」コマンド
    /// </summary>
    private void OnItemSelected(CharacterModel character)
    {
        /*
        _itemPanelView.Show();
        _itemPanelView.Initialize(itemName =>
        {
            _itemPanelView.Hide();
            _battleModel.EnqueueAction(new CharacterAction { ActionType = ActionTypeEnum.Item, ItemName = itemName });
            _currentCharacterIndex++;
            StartCharacterTurn();
        });
        */
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
        StartCharacterTurn();
    }
    
    /// <summary>
    /// 各行動の処理
    /// </summary>
    private void ProcessAction(CharacterAction action)
    {
        Debug.Log($"Processing action: {action.ActionType}");
    }
    
    /// <summary>
    /// バトル終了
    /// </summary>
    private void EndBattle()
    {
        
    }
    
}