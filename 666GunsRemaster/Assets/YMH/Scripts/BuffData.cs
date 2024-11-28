using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Datas", menuName = "Datas/BuffData")]
public class BuffData : ScriptableObject
{
    public Sprite BuffImage;     //���� �̹���
    public string BuffName;     //���� �̸�
    [TextArea(3, 10)]
    public string BuffContent;  //���� ����
}