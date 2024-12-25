using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スキルボタンの見た目を変更します
/// </summary>
public class SkillButtonView : MonoBehaviour
{
    [SerializeField] private Image iconImage;  // スキルアイコン表示用
    [SerializeField] private Text nameText;  // スキル名表示用
    [SerializeField] private Text costText;  // スキル名表示用
    [SerializeField] private Button button;  // ボタンコンポーネント

    private SkillDataSO skillData; // このボタンに関連付けられたスキル
    private Action<SkillDataSO> onSelectedCallback; // スキル選択時のコールバック

    /// <summary>
    /// 初期化
    /// </summary>
    public void Setup(CharacterModel character, SkillDataSO skill, Action<SkillDataSO> onSelected)
    {
        skillData = skill;
        onSelectedCallback = onSelected;

        // スキルデータに基づきUI更新
        iconImage.sprite = skill.Icon;
        nameText.text = skill.Name;
        costText.text = skill.ResourceCost.ToString();
        
        //リソースが足りていなかったらボタンを押せないようにする
        if (!CanUseSkill(skill.ResourceCost, GetResource(character, skill)))
        {
            button.interactable = false;
        }
        
        // ボタンにクリックイベントを追加
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onSelectedCallback(skillData));
    }

    /// <summary>
    ///  スキルが使えるか判定
    /// </summary>
    private bool CanUseSkill(int cost, int nowResource)
    {
        return cost <= nowResource;
    }

    /// <summary>
    /// スキルの消費リソースに応じて、キャラクターの現在のリソース量を返します
    /// </summary>
    private int GetResource(CharacterModel character, SkillDataSO skillData)
    {
        int nowResource = skillData.ResourceType switch
        {
            ResourceTypeEnum.SP => character.SP,
            ResourceTypeEnum.TP => character.TP,
            ResourceTypeEnum.HP => character.HP,
        };
        return nowResource;
    }
}
