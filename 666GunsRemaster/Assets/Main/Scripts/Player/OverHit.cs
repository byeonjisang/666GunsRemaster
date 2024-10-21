using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverHit : MonoBehaviour
{
    private Animator anim;

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
}
