using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 戦闘の進行状況（ターン情報、勝敗）を管理
/// </summary>
public class BattleModel
{
    /// <summary>プレイヤーのCharacterModelのリスト</summary>
    public List<CharacterModel> Players { get; private set; }
    /// <summary>エネミーのCharacterModelのリスト</summary>
    public List<CharacterModel> Enemies { get; private set; }
    
    /// <summary>各キャラクターが選択した行動のキュー</summary>
    public Queue<CharacterAction> ActionsQueue { get; private set; } = new Queue<CharacterAction>();
    /// <summar>現在のターン数</summary>
    public int CurrentTurn { get; private set; }

    /// <summary>
    ///  バトル情報の初期化
    /// </summary>
    public BattleModel(List<CharacterModel> players, List<CharacterModel> enemies)
    {
        Players = players;
        Enemies = enemies;
        CurrentTurn = 0;
    }

    /// <summary>
    /// ターンを進める
    /// </summary>
    public void NextTurn()
    {
        CurrentTurn++;
    }
    
    /// <summary>
    /// 末尾にアクションを追加
    /// </summary>
    public void EnqueueAction(CharacterAction action)
    {
        ActionsQueue.Enqueue(action);
    }

    /// <summary>
    /// 先頭のアクションを取り出す
    /// </summary>
    public CharacterAction DequeueAction()
    {
        return ActionsQueue.Dequeue();
    }

    /// <summary>
    /// 勝敗判定。ゲームオーバー
    /// </summary>
    public bool IsBattleOver()
    {
        return Players.All(p => p.IsDead()) || Enemies.All(e => e.IsDead());
    }
}