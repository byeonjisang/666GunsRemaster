using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private List<BaseEnemy> enemies = new List<BaseEnemy>();

    void Awake()
    {
        base.Awake();
    }

    public void RegisterEnemy(BaseEnemy enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    public void UnregisterEnemy(BaseEnemy enemy)
    {
        enemies.Remove(enemy);
    }

    public void DamageAllEnemies(int damage)
    {
        foreach (var e in enemies)
            e.TakeDamage(damage);
    }

    public List<BaseEnemy> GetAllEnemies() => enemies;
}
