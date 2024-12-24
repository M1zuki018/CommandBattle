using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 攻撃、スキル、防御、アイテムの選択を行うCommandパネルを管理します
/// </summary>
public class CommandPanelView : MonoBehaviour
{
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _skillButton;
    [SerializeField] private Button _defendButton;
    [SerializeField] private Button _itemButton;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 登録されているメソッドを解除
    /// </summary>
    public void Reset()
    {
        _attackButton.onClick.RemoveAllListeners();
        _skillButton.onClick.RemoveAllListeners();
        _defendButton.onClick.RemoveAllListeners();
        _itemButton.onClick.RemoveAllListeners();
    }

    public void Initialize(Action onAttack, Action onSkill, Action onDefend, Action onItem)
    {
        _attackButton.onClick.AddListener(() => onAttack());
        _skillButton.onClick.AddListener(() => onSkill());
        _defendButton.onClick.AddListener(() => onDefend());
        _itemButton.onClick.AddListener(() => onItem());
    }
}