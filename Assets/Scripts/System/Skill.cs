using System;
using UnityEngine;

/// <summary>
/// スキルの処理をまとめたクラス
/// </summary>
public class Skill : MonoBehaviour
{
    private SkillDataSO _skill;
    private CharacterModel _user, _target;
    
    /// <summary>
    /// スキル処理
    /// </summary>
    public void SkillActivate(SkillDataSO skillData, CharacterModel user, CharacterModel target)
    {
        _skill = skillData;
        _user = user;
        _target = target;
        
        SkillEffectCheck();
    }

    /// <summary>
    /// スキル効果を確認して、対応する効果を呼び出します
    /// </summary>
    private void SkillEffectCheck()
    {
        Action effect = _skill.EffectType switch
        {
            EffectTypeEnum.Attack => () => Attack(),
            EffectTypeEnum.Heal => () => Heal(),
            EffectTypeEnum.Buff => () => Buff(),
            EffectTypeEnum.Debuff => () => Debuff(),
            EffectTypeEnum.AttackWithEffect => () => AttackWithEffect(),
            EffectTypeEnum.Revive => () => Revive(),
        };
        
        effect.Invoke();
    }

    /// <summary>
    /// 攻撃スキルの効果
    /// </summary>
    private void Attack()
    {
        _target.TakeDamage(_skill.EffectValue);
    }
    
    /// <summary>
    /// 回復スキルの効果
    /// </summary>
    private void Heal()
    {
        _target.HP += _skill.EffectValue;
        Debug.Log("回復した！");
    }

    /// <summary>
    /// バフを付与するスキルの効果
    /// </summary>
    private void Buff()
    {
        Debug.Log("バフを与える");
    }

    /// <summary>
    /// デバフを付与するスキルの効果
    /// </summary>
    private void Debuff()
    {
        Debug.Log("デバフを与える");
    }

    /// <summary>
    /// 攻撃しつつ効果を付与するスキルの効果
    /// </summary>
    private void AttackWithEffect()
    {
        Debug.Log("攻撃＋効果を与える");
    }

    /// <summary>
    /// 蘇生を行うスキルの効果
    /// </summary>
    private void Revive()
    {
        Debug.Log("蘇生を行う");
    }
    
    //TODO: 攻撃ターゲットの処理を実装する
}
