using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionEntered : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("isPlayerEnter", true);
            GameManager.instance.Timer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("isPlayerEnter", false);
        }
    }
}
