
using System;
using UnityEngine;

/// <summary>
/// アイテムの処理をまとめたクラス
/// </summary>
public class Item : MonoBehaviour
{
    private ItemData _item;
    private CharacterModel _user, _target;

    /// <summary>
    /// アイテム処理
    /// </summary>
    public void ItemActivate(ItemData itemData, CharacterModel user, CharacterModel target)
    {
        _item = itemData;
        _user = user;
        _target = target;
        
        ItemEffectCheck();
    }

    /// <summary>
    /// アイテム効果を確認して、対応する効果を呼び出します
    /// </summary>
    private void ItemEffectCheck()
    {
        Action effect = _item.EffectType switch
        {
            EffectTypeEnum.Attack => () => Heal(),
            EffectTypeEnum.Heal => () => Heal(),
        };
        
        Debug.Log(effect);
        effect?.Invoke();
    }
    
    /// <summary>
    /// 回復効果
    /// </summary>
    private void Heal()
    {
        Debug.Log("アイテムを使います。");
        _target.HP += _item.EffectValue;
        Debug.Log("回復した！");
    }
}
