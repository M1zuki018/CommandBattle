using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Battle/CharacterData")]
public class CharacterDataSO : ScriptableObject
{
    public string Name;       
    public int MaxHP;                 
    public int MaxSP;            
    public int Aatack;
    public int Defense;
    public Sprite Sprite1;             // HPバーと一緒に表示するスプライト
    public Sprite Sprite2;             // コマンド部分で表示するスプライト
}