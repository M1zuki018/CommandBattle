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

    public void Setup(SkillDataSO skill)
    {
        skillData = skill;

        // スキルデータに基づきUI更新
        iconImage.sprite = skill.Icon;
        nameText.text = skill.Name;
        costText.text = skill.ResourceCost.ToString();

        // ボタンにクリックイベントを追加
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => OnSkillSelected());
    }

    private void OnSkillSelected()
    {
        Debug.Log($"{skillData.Name} selected!");
        // スキル選択時の挙動をここに記述
    }
}
