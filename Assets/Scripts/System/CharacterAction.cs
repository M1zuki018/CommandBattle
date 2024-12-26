public class CharacterAction
{
    public ActionTypeEnum ActionType { get; set; }
    public int TargetIndex { get; set; } // 攻撃/スキルの対象
    public SkillDataSO Skill { get; set; } // スキルデータ
    public ItemData Item { get; set; } // アイテムの場合の詳細
}