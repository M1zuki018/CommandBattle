using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スキルパネルのUIを管理します
/// </summary>
public class SkillPanelView : MonoBehaviour
{
    [SerializeField] private GameObject _skillButtonPrefab;
    [SerializeField, Header("ScrollViewのTransform")] private Transform _content; 
    private List<SkillDataSO> skillList; // スキルデータのリスト
    private SkillDataSO _selectedSkill;
    
    private Action onSkillSelected; // コールバック
    public SkillDataSO SelectedSkill => _selectedSkill;　//外部からどのスキルが選択されているか取得可能にする
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(CharacterModel character, List<SkillDataSO> skills, Action onSkillSelectedCallback)
    {
        onSkillSelected = onSkillSelectedCallback;
        skillList = skills;

        // 既存のボタンを全て削除する
        foreach (Transform child in _content)
        {
            Destroy(child.gameObject);
        }

        // スキルごとにボタンを生成
        foreach (var skill in skillList)
        {
            GameObject button = Instantiate(_skillButtonPrefab, _content);

            // ボタンのUIを設定
            var skillButton = button.GetComponent<SkillButtonView>();
            skillButton.Setup(character, skill, OnSkillSelected);
        }
    }

    private void OnSkillSelected(SkillDataSO skill)
    {
        _selectedSkill = skill;
        onSkillSelected?.Invoke();
    }
}
