using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun.Bullet 
{
    public class PoliceBullet : Bullet
    {
        public float GetDamage() { return damage; }

        protected override void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
            rigid = GetComponent<Rigidbody2D>();
        }

        protected override void Start()
        {
            damage = 2;
            speed =  5;
            range = 10;
            penetration = 1;
            blockObject = LayerMask.GetMask("Player", "Wall");
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            
            if((blockObject.value & (1 << collision.gameObject.layer)) > 0)
            {
                currentPenetration -= 1;

                if(currentPenetration <= 0)
                {
                    rigid.AddForce(Vector2.zero, ForceMode2D.Force);
                    Destroy(gameObject);
                }
            }
        }
    }
}