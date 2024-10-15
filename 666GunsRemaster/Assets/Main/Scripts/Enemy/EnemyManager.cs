using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MonsterType
{
    Police1,
    Police2,
    // 추가적인 몬스터 타입을 여기에 정의할 수 있습니다.
}

public class EnemyManager : MonoBehaviour
{
    // 생성할 몬스터들의 리스트
    [SerializeField]
    private List<Police1> police1Datas;
    [SerializeField]
    private List<Police2> police2Datas;

    //몬스터들의 오브젝트
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

        // 랜덤 위치 생성 (예: -10, 10 사이에서 랜덤 위치 선택)
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

            // 추가적인 몬스터 타입에 대한 처리도 여기에 추가 가능
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        // 예시: X, Y 좌표를 -10에서 10 사이에서 랜덤으로 설정
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
            // 추가적인 몬스터 타입에 대한 프리팹도 여기에 추가 가능
            default:
                return null;
        }
    }
}
