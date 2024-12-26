using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キャラクターのUIを整えます
/// </summary>
public class CharacterView : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Slider _hpSlider, _spSlider;
    [SerializeField] private Image _tpSlider;
    [SerializeField] private Text _hpText, _spText;

    /// <summary>
    /// UIを更新します
    /// </summary>
    public void UpdateView(CharacterModel character)
    {
        _nameText.text = character.Name;
        _hpSlider.maxValue = character.MaxHP;
        _hpSlider.value = character.HP;
        _spSlider.maxValue = character.MaxSP;
        _spSlider.value = character.SP;
        _hpText.text = $"{character.HP}/{character.MaxHP}";
        _spText.text = $"{character.SP}/{character.MaxSP}";
        _tpSlider.fillAmount = character.TP / 100;
    }

    public void PlayDamageAnimation()
    {
        // ダメージを受けたときのアニメーションを再生
    }
}
