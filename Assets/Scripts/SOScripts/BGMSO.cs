using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGMの音声データを管理します
/// </summary>
[CreateAssetMenu(fileName = "BGMSO", menuName = "Audio/BGMSO")]
public class BGMSO : ScriptableObject, ISwitchableData<AudioClip>
{ 
    [SerializeField] private List<AudioClip> _bgmClips = new List<AudioClip>();
    
    public AudioClip GetItem(int index)
    {
        return _bgmClips[index];
    }

    public int GetItemCount()
    {
        return _bgmClips.Count;
    }
}
