using System.Collections;
using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    [SerializeField]
    private float distance;

    private GameObject nearestEnemy;

    private void Update()
    {
        //범위 안에 있는 모든 오브젝트 탐색
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, distance, transform.forward, 0);

        foreach (RaycastHit h in hit)
        {
            switch (h.collider.tag)
            {
                // 그 중 적들을 탐색
                case "Enemy":
                    //가장 가까운 적 찾기
                    GetNearestEnemy(h.collider.gameObject);
                    break;
            }
        }
    }

    private void GetNearestEnemy(GameObject enemyObject)
    {
        //찾은 적이 가장 가까운 적으로 이미 설정될 경우 빠져나감
        if (nearestEnemy == enemyObject)
            return;

        //가장 가까운 적이 없거나, 현재 적보다 더 가까운 적을 찾으면
        if(nearestEnemy != null)
        {
            float nearestDistance = Vector3.Distance(transform.position, nearestEnemy.transform.position);
            float currentDistance = Vector3.Distance(transform.position, enemyObject.transform.position);

           if (currentDistance < nearestDistance)
            {
                nearestEnemy.GetComponent<EnemyTest>().CheckedFromPlayer(false);
                nearestEnemy = enemyObject;
                nearestEnemy.GetComponent<EnemyTest>().CheckedFromPlayer(true);

                Debug.Log("가장 가까운 적: " + nearestEnemy.name);
            }
        }
        else
        {
            nearestEnemy = enemyObject;
        }
    }
}