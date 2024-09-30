using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Gun.Bullet
{

    public class Bullet : MonoBehaviour
    {
        public IObjectPool<Bullet> Pool { get; set; }
        public GunData gunData { private get; set; }

        protected float damage;


        protected virtual void Awake()
        {
            damage = gunData.damage;
            speed = gunData.speed;
        }
        protected virtual void Update()
        {

        }
    }
}