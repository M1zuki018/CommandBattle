using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景に関連したクラス
/// </summary>
[CreateAssetMenu(fileName = "BackgroundSO", menuName = "UI/Background")]
public class BackgroundSO : ScriptableObject, ISwitchableData<Sprite>
{
    [SerializeField] private List<Sprite> _backgroundSprites;

    public Sprite GetItem(int index)
    {
        return _backgroundSprites[index];
    }

    public int GetItemCount()
    {
        return _backgroundSprites.Count;
    }
}
