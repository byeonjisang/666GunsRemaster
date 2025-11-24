using UnityEngine;

namespace Character.Player
{
    public class EnemyScanner
    {
        Player _player;

        [SerializeField]
        private float _distance = 10;

        private GameObject nearestEnemy;
        public GameObject NearestEnemy { get { return nearestEnemy; } }

        public EnemyScanner(Player player)
        {
            _player = player;
        }

        public void ScannerUpdate()
        {
            //범위 안에 있는 모든 오브젝트 탐색
            RaycastHit[] hit = Physics.SphereCastAll(_player.transform.position, _distance, _player.transform.forward, 0);

            Debug.Log($"[EnemyScanner] Detected {hit.Length} objects.");
            foreach (RaycastHit h in hit)
            {
                switch (h.collider.tag)
                {
                    // 그 중 적들을 탐색
                    case "Enemy":
                        //가장 가까운 적 찾기
                        Debug.Log(h.collider.gameObject.name);
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
                float nearestDistance = Vector3.Distance(_player.transform.position, nearestEnemy.transform.position);
                float currentDistance = Vector3.Distance(_player.transform.position, enemyObject.transform.position);

            if (currentDistance < nearestDistance)
                {
                    nearestEnemy = enemyObject;
                    Debug.Log("가장 가까운 적: " + nearestEnemy.name);
                }
            }
            else
            {
                nearestEnemy = enemyObject;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_player.transform.position, _distance);
        }
    }   
}