public class CharacterAction
{
    public ActionTypeEnum ActionType { get; set; }
    public int TargetIndex { get; set; } // 攻撃/スキルの対象
    public string SkillName { get; set; } // スキルの場合の詳細
    public string ItemName { get; set; } // アイテムの場合の詳細
}