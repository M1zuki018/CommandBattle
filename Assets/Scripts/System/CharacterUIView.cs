using UnityEngine;

/// <summary>
/// 各キャラクターのHPバーなどが表示されているパネルの管理
/// </summary>
public class CharacterUIView : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
