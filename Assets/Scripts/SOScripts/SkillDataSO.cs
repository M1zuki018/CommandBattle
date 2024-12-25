using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

/// <summary>
/// スキルのデータ
/// </summary>
[CreateAssetMenu(fileName = "SkillData", menuName = "Battle/Skill")]
public class SkillDataSO : ScriptableObject
{
    [Header("表示関連")]
    public Sprite Icon;                 // スキルアイコン
    public string Name;                 // スキル名
    public string Description;          // スキルの詳細説明

    [Header("詳細設定")]
    public TargetTypeEnum TargetType;       // ターゲットタイプ
    public EffectTypeEnum EffectType;       // エフェクトタイプ
    public AttributeTypeEnum AttributeType; // 属性タイプ
    public int EffectValue; // 効果の値 
    [Tooltip("効果持続ターン数")]public int EffectDuration;          // 効果持続ターン数
    public int HitCount;                // 攻撃回数

    [Header("リソース設定")]
    public ResourceTypeEnum ResourceType;   // 消費リソース
    public int ResourceCost;            // リソースの消費量
    
    /// <summary>カスタム処理</summary>
    public event Action<Character, Character> OnSkillUse;
    
    /// <summary>
    /// 実行メソッド
    /// </summary>
    public void ExecuteSkill(Character user, Character target)
    {
        OnSkillUse?.Invoke(user, target);
    }
}