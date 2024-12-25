using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターのステータスと行動ロジック
/// </summary>
public class CharacterModel
{
    public string Name { get; private set; }
    public int HP { get; set; }
    public int MaxHP { get; private set; }
    public int SP { get; private set; }
    public int MaxSP { get; private set; }
    
    public int TP { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    
    public int Speed { get; set; }
    
    public int Critical { get; private set; }
    
    public int CriticalDamage { get; private set; }
    
    public Sprite Sprite1 { get; private set; }
    public Sprite Sprite2 { get; private set; }
    
    public List<SkillDataSO> Skills { get; private set; }

    /// <summary>
    /// キャラクター情報を登録します
    /// </summary>
    public CharacterModel(CharacterDataSO characterDataSO)
    {
        Name = characterDataSO.Name;
        MaxHP = characterDataSO.MaxHP;
        HP = MaxHP;
        MaxSP = characterDataSO.MaxSP;
        SP = MaxSP;
        TP = 0;
        Attack = characterDataSO.Aatack;
        Defense = characterDataSO.Defense;
        Speed = characterDataSO.Speed;
        Critical = characterDataSO.Critical;
        CriticalDamage = characterDataSO.CriticalDamage;
        Sprite1 = characterDataSO.Sprite1;
        Sprite2 = characterDataSO.Sprite2;
        Skills = characterDataSO.Skills;
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