using UnityEngine;

[CreateAssetMenu(fileName = "Datas", menuName = "Datas/BuffData")]
public class BuffData : ScriptableObject
{
    public string BuffName;     //���� �̸�
    [TextArea(3, 10)]
    public string BuffContent;  //���� ����
}