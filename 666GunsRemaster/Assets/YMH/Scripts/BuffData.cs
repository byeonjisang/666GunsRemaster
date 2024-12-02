using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Datas", menuName = "Datas/BuffData")]
public class BuffData : ScriptableObject
{
    [Header("Buff Info")]
    public Sprite BuffImage;     //���� �̹���
    public string BuffName;     //���� �̸�
    [TextArea(3, 10)]
    public string BuffContent;  //���� ����

    [Header("Buff Value")]
    public List<float> BuffValue;     //���� ��
}