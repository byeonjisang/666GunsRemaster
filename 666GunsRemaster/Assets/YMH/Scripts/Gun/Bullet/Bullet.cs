
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Character.Gun.Bullet
{

    public class Bullet : MonoBehaviour
    {
        public IObjectPool<Bullet> Pool { get; set; }

        protected float damage;
        protected float speed;
        protected int penetrationCount;
        protected LayerMask blockObejct;
    }
}