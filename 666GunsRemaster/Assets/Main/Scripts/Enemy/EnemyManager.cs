using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MonsterType
{
    Police1,
    Police2,
    // �߰����� ���� Ÿ���� ���⿡ ������ �� �ֽ��ϴ�.
}

public class EnemyManager : MonoBehaviour
{
    // ������ ���͵��� ����Ʈ
    [SerializeField]
    private List<Police1> police1Datas;
    [SerializeField]
    private List<Police2> police2Datas;

    //���͵��� ������Ʈ
    [SerializeField]
    private GameObject police1Prefab;
    [SerializeField]
    private GameObject police2Prefab;

    void Start()
    {
        SpawnPoliceMonsters(MonsterType.Police1, police1Datas);
        SpawnPoliceMonsters(MonsterType.Police2, police2Datas);
    }

    private void SpawnPoliceMonsters<T>(MonsterType type, List<T> monsterDatas) where T : class
    {
        for (int i = 0; i < monsterDatas.Count; i++)
        {
            SpawnMonster(type, monsterDatas[i]);
        }
    }

    public void SpawnMonster(MonsterType type, object monsterData)
    {
        GameObject monsterPrefab = GetMonsterPrefab(type);

        // ���� ��ġ ���� (��: -10, 10 ���̿��� ���� ��ġ ����)
        Vector2 randomPosition = GetRandomSpawnPosition();

        GameObject newMonster = Instantiate(monsterPrefab, randomPosition, Quaternion.identity);

        switch (type)
        {
            case MonsterType.Police1:
                Police1Stats police1Stats = newMonster.GetComponent<Police1Stats>();
                police1Stats.SetData(monsterData as Police1);
                police1Stats.DebugMonsterInfo();
                break;

            case MonsterType.Police2:
                Police2Stats police2Stats = newMonster.GetComponent<Police2Stats>();
                police2Stats.SetData(monsterData as Police2);
                police2Stats.DebugMonsterInfo();
                break;

            // �߰����� ���� Ÿ�Կ� ���� ó���� ���⿡ �߰� ����
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        // ����: X, Y ��ǥ�� -10���� 10 ���̿��� �������� ����
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-10f, 10f);

        return new Vector2(randomX, randomY);
    }


    private GameObject GetMonsterPrefab(MonsterType type)
    {
        switch (type)
        {
            case MonsterType.Police1:
                return police1Prefab;
            case MonsterType.Police2:
                return police2Prefab;
            // �߰����� ���� Ÿ�Կ� ���� �����յ� ���⿡ �߰� ����
            default:
                return null;
        }
    }
}
