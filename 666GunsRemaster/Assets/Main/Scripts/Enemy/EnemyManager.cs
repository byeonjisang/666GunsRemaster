using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MonsterType
{
    Normal
}
public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyTest1> monsterDatas;
    [SerializeField]
    private GameObject monster;

    void Start()
    {
        for (int i = 0; i < monsterDatas.Count; i++)
        {
            var zombie = SpawnMonster((MonsterType)i);
            zombie.DebugMonsterInfo();
        }
    }

    public MonsterStats SpawnMonster(MonsterType type)
    {
        var newZombie = Instantiate(monster).GetComponent<MonsterStats>();
        newZombie.test1 = monsterDatas[(int)type];
        return newZombie;
    }
}
