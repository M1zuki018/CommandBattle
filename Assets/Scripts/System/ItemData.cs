using UnityEngine;

/// <summary>
/// アイテムのデータ
/// </summary>
[System.Serializable]
public class ItemData
{
    public string Name; // アイテム名
    public string Description; // アイテムの説明
    public Sprite Icon; // アイテムアイコン
    public TargetTypeEnum TargetType; // 対象タイプ
    public EffectTypeEnum EffectType; // 効果タイプ
    public int EffectValue; // 効果値
    public int Quantity; // 所持数

    /// <summary>
    /// アイテムを使ったときに呼ばれるメソッド
    /// </summary>
    public void UseItem()
    {
        if (Quantity > 0)
        {
            Quantity--;
            Debug.Log($"Used item: {Name}. Remaining quantity: {Quantity}");
            // 効果を適用する処理は別途記述
        }
        else
        {
            Debug.Log($"No more {Name} left.");
        }
    }
}