using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景に関連したクラス
/// </summary>
[CreateAssetMenu(fileName = "BackgroundSO", menuName = "UI/Background")]
public class BackgroundSO : ScriptableObject
{
    [SerializeField] private List<Sprite> _backgroundSprites;

    /// <summary>
    /// 背景データを取得する
    /// </summary>
    public Sprite GetBackgroundSprite(int index)
    {
        return _backgroundSprites[index];
    }
    
    /// <summary>
    /// 登録されている背景データの数を取得する
    /// </summary>
    public int GetBackgroundSpriteCount()
    {
        return _backgroundSprites.Count;
    }
}
