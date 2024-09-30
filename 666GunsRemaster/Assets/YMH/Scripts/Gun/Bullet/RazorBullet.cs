using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun.Bullet
{
    public class RazorBullet : Bullet
    {
        private void Start()
        {
            Init();
        }

        private void Init()
        {
            damage = bulletData.damage;
            speed = bulletData.speed;
            penetrationCount = bulletData.penetrationCount;
            blockObejct = bulletData.blockObejct;
        }
    }
}