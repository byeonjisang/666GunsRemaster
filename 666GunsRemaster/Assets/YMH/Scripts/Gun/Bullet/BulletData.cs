using System.Collections;
using UnityEngine;

namespace Gun.Bullet
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "BulletData", order = 0)]
    public class BulletData : ScriptableObject
    {

        public float damage;
        public float speed;
        public int penetrationCount;
        public LayerMask blockObejct;
    }
}