using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Police1,
    Police2
    // �߰����� ���� Ÿ���� ���⿡ ������ �� �ֽ��ϴ�.
}
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private ObjectPool police1Pool; // Police1�� ���� Ǯ
    [SerializeField] private ObjectPool police2Pool; // Police2�� ���� Ǯ

    public int police1Count = 0;
    public int police2Count = 0;

    void Start()
    {
        SpawnPoliceMonsters(MonsterType.Police1, police1Count); // ����: Police1 5���� ����
        SpawnPoliceMonsters(MonsterType.Police2, police2Count); // ����: Police2 3���� ����
    }

    private void SpawnPoliceMonsters(MonsterType type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnMonster(type);
        }
    }

    public void SpawnMonster(MonsterType type)
    {
        GameObject monster = null;

        // Ǯ���� ���� ��������
        switch (type)
        {
            case MonsterType.Police1:
                monster = police1Pool.GetObject();
                break;

            case MonsterType.Police2:
                monster = police2Pool.GetObject();
                break;
        }

        if (monster != null)
        {
            Vector2 randomPosition = GetRandomSpawnPosition();
            monster.transform.position = randomPosition;

            // ������ �ʱ�ȭ �۾� ���� (��: Police1Stats, Police2Stats ����)
            InitializeMonster(monster, type);
        }
    }

    private void InitializeMonster(GameObject monster, MonsterType type)
    {
        switch (type)
        {
            case MonsterType.Police1:
                Police1Stats police1Stats = monster.GetComponent<Police1Stats>();
                police1Stats.SetData(GetPolice1Data());
                police1Stats.DebugMonsterInfo();
                break;

            case MonsterType.Police2:
                Police2Stats police2Stats = monster.GetComponent<Police2Stats>();
                police2Stats.SetData(GetPolice2Data());
                police2Stats.DebugMonsterInfo();
                break;
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        // ����: X, Y ��ǥ�� -10���� 10 ���̿��� �������� ����
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-10f, 10f);
        return new Vector2(randomX, randomY);
    }

    private Police1 GetPolice1Data()
    {
        // ���⿡ Police1 �����͸� �������� ���� ����
        return new Police1();
    }

    private Police2 GetPolice2Data()
    {
        // ���⿡ Police2 �����͸� �������� ���� ����
        return new Police2();
    }

    // ���Ͱ� �װų� ����� �� ȣ���Ͽ� Ǯ�� ��ȯ
    public void ReturnMonster(GameObject monster, MonsterType type)
    {
        switch (type)
        {
            case MonsterType.Police1:
                police1Pool.ReturnObject(monster);
                break;

            case MonsterType.Police2:
                police2Pool.ReturnObject(monster);
                break;
        }
    }
}
