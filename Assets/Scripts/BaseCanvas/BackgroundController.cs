using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 背景を変更します
/// </summary>
public class BackgroundController : MonoBehaviour
{
    [SerializeField] private BackgroundSO _backgroundSO;
    [SerializeField] private Image _backgroundRenderer;
    private int currentIndex = 0;
    
    /// <summary>
    /// 指定したインデックスの背景に切り替え
    /// </summary>
    public void SetBackground(int index)
    {
        if (index >= 0 && index < _backgroundSO.GetBackgroundSpriteCount())
        {
            currentIndex = index;
            _backgroundRenderer.sprite = _backgroundSO?.GetBackgroundSprite(index);
        }
    }

    /// <summary>
    /// 次の背景に切り替え
    /// </summary>
    [ContextMenu("NextBackground")]
    public void NextBackground()
    {
        currentIndex = (currentIndex + 1) % _backgroundSO.GetBackgroundSpriteCount();
        SetBackground(currentIndex);
    }

    /// <summary>
    /// 前の背景に切り替え
    /// </summary>
    [ContextMenu("PreviousBackground")]
    public void PreviousBackground()
    {
        currentIndex = (currentIndex - 1 + _backgroundSO.GetBackgroundSpriteCount()) % _backgroundSO.GetBackgroundSpriteCount();
        SetBackground(currentIndex);
    }
}