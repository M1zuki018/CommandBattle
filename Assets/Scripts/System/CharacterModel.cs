using UnityEngine;

/// <summary>
/// キャラクターのステータスと行動ロジック
/// </summary>
public class CharacterModel
{
    public string Name { get; private set; }
    public int HP { get; private set; }
    public int MaxHP { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }

    /// <summary>
    /// キャラクター情報を登録します
    /// </summary>
    public CharacterModel(string name, int maxHP, int attack, int defense)
    {
        Name = name;
        MaxHP = maxHP;
        HP = maxHP;
        Attack = attack;
        Defense = defense;
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    public void TakeDamage(int damage)
    {
        HP = Mathf.Max(HP - damage, 0);
    }

    /// <summary>
    /// 与えるダメージ量（1は保障された状態）
    /// </summary>
    public int CalculateDamage(int enemyDefense)
    {
        return Mathf.Max(Attack - enemyDefense, 1);
    }

    /// <summary>
    /// 死んだかどうかの判定
    /// </summary>
    public bool IsDead()
    {
        return HP <= 0;
    }
}