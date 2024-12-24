using UnityEngine;
using System;

public class ISwitchableController<T, U> : MonoBehaviour where U : ISwitchableData<T>
{
    [SerializeField] private U _data; // データ（背景やBGM）
    private int _currentIndex = 0;

    /// <summary>
    /// 指定されたインデックスの項目を設定
    /// </summary>
    public void SetItem(int index, Action<T> applyAction)
    {
        if (index >= 0 && index < _data.GetItemCount())
        {
            _currentIndex = index;
            applyAction(_data.GetItem(index));
        }
    }

    /// <summary>
    /// 次の項目に切り替え
    /// </summary>
    public void NextItem(Action<T> applyAction)
    {
        _currentIndex = (_currentIndex + 1) % _data.GetItemCount();
        SetItem(_currentIndex, applyAction);
    }

    /// <summary>
    /// 前の項目に切り替え
    /// </summary>
    public void PreviousItem(Action<T> applyAction)
    {
        _currentIndex = (_currentIndex - 1 + _data.GetItemCount()) % _data.GetItemCount();
        SetItem(_currentIndex, applyAction);
    }
}