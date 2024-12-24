using UnityEngine;

using UnityEngine.UI;

/// <summary>
/// 背景を変更します
/// </summary>
public class BackgroundController : SwitchableController<Sprite, BackgroundSO>
{
    [SerializeField] private Image _background;

    private void ApplyBackground(Sprite sprite)
    {
        _background.sprite = sprite;
        Debug.Log("Background applied");
    }

    [ContextMenu("NextBackground")]
    public void NextBackground()
    {
        NextItem(ApplyBackground);
    }

    [ContextMenu("PreviousBackground")]
    public void PreviousBackground()
    {
        PreviousItem(ApplyBackground);
    }
}
   