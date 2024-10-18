using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Police1,
    Police2
    // 추가적인 몬스터 타입을 여기에 정의할 수 있습니다.
}
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private ObjectPool police1Pool; // Police1을 위한 풀
    [SerializeField] private ObjectPool police2Pool; // Police2를 위한 풀

    public int police1Count = 0;
    public int police2Count = 0;

    void Start()
    {
        SpawnPoliceMonsters(MonsterType.Police1, police1Count); // 예시: Police1 5마리 스폰
        SpawnPoliceMonsters(MonsterType.Police2, police2Count); // 예시: Police2 3마리 스폰
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

        // 풀에서 몬스터 가져오기
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

            // 몬스터의 초기화 작업 실행 (예: Police1Stats, Police2Stats 설정)
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
        // 예시: X, Y 좌표를 -10에서 10 사이에서 랜덤으로 설정
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-10f, 10f);
        return new Vector2(randomX, randomY);
    }

    private Police1 GetPolice1Data()
    {
        // 여기에 Police1 데이터를 가져오는 로직 구현
        return new Police1();
    }

    private Police2 GetPolice2Data()
    {
        // 여기에 Police2 데이터를 가져오는 로직 구현
        return new Police2();
    }

    // 몬스터가 죽거나 사라질 때 호출하여 풀로 반환
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
