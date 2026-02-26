using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy
{
    [CreateAssetMenu(fileName = "Throw Attack Strategy", menuName = "Enemy/AttackStrategy/Throw")]
    public class ThrowAttackStrategy : AttackStrategy
    {
        [SerializeField] private List<GameObject> _throwPrefab;

        public override void Execute(Enemy enemy)
        {
            if (_throwPrefab == null)
            {
                Debug.LogError("Throw Prefab is not assigned!");
                return;
            }

            // 투척 위치 가져오기
            Transform throwPoint = enemy.ActiveMuzzle[0];

            // 투척체 생성
            GameObject throwObject = ObjectPoolManager.Instance.GetFromPool("Throw_Enemy", throwPoint.position, throwPoint.rotation);
            //throwObject.GetComponent<Weapon.Throw.ThrowBase>().Initialization((float)enemy.EnemyStat.Attack, 10f);
        }
    }
}