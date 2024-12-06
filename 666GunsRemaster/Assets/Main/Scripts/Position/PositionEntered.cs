using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionEntered : MonoBehaviour
{
    private Animator animator;

    public Image pointerImage;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("isPlayerEnter", true);
            pointerImage.gameObject.SetActive(false);
            GameManager.instance.Timer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("isPlayerEnter", false);
            pointerImage.gameObject.SetActive(true);

        }
    }
}
