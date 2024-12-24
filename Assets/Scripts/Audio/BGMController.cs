using UnityEngine;

/// <summary>
/// BGMを管理します
/// </summary>
public class BGMController : SwitchableController<AudioClip, BGMSO>
{
    [SerializeField] private AudioSource _audioSource;

    private void ApplyBGM(AudioClip clip)
    {
        _audioSource.clip = clip;
        if (Application.isPlaying)
        {
            _audioSource.Play();
        }
    }

    [ContextMenu("NextBGM")]
    public void NextBGM()
    {
        NextItem(ApplyBGM);
    }

    [ContextMenu("PreviousBGM")]
    public void PreviousBGM()
    {
        PreviousItem(ApplyBGM);
    }
}