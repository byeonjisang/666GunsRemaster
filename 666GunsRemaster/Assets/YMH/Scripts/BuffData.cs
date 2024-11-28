using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Datas", menuName = "Datas/BuffData")]
public class BuffData : ScriptableObject
{
    public Sprite BuffImage;     //버프 이미지
    public string BuffName;     //버프 이름
    [TextArea(3, 10)]
    public string BuffContent;  //버프 내용
}