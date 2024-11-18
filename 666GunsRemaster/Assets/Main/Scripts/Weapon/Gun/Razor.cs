using Gun.Bullet;
using System.Collections;
using UnityEngine;

namespace Gun
{
    public class Razor : Gun
    {
        protected override void PlayFireSound()
        {
            Debug.Log("레이저 소리 재생");
            //레이저 총기 소리 재생
        }
    }
}