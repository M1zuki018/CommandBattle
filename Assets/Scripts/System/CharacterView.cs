using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キャラクターのUIを整えます
/// </summary>
public class CharacterView : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Text _hpText;

    public void UpdateView(CharacterModel character)
    {
        _nameText.text = character.Name;
        _hpSlider.maxValue = character.MaxHP;
        _hpSlider.value = character.HP;
        _hpText.text = $"{character.HP}/{character.MaxHP}";
    }

    public void PlayDamageAnimation()
    {
        // ダメージを受けたときのアニメーションを再生
    }
}
