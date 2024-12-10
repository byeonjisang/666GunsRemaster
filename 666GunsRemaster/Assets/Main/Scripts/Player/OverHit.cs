using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player
{
    public class Overhit : MonoBehaviour
    {
        private Animator anim;
        private float damage = 5;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            anim.SetTrigger("OverHit");
        }

        public void OffOverHit()
        {
            gameObject.SetActive(false);
        }

        public float GetDamage()
        {
            return damage;
        }
    }
}