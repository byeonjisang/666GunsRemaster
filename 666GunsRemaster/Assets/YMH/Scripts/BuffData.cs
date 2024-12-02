using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Datas", menuName = "Datas/BuffData")]
public class BuffData : ScriptableObject
{
    [Header("Buff Info")]
    public Sprite BuffImage;     //버프 이미지
    public string BuffName;     //버프 이름
    [TextArea(3, 10)]
    public string BuffContent;  //버프 내용

    [Header("Buff Value")]
    public List<float> BuffValue;     //버프 값
}