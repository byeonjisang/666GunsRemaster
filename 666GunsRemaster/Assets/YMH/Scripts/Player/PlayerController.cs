using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement_YMH playerMovement;

    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private FloatingJoystick joystick;

    private int health;
    private float moveSpeed;
    private float dashSpeed;
    private float dashDuration;
    private float dashCooldown;
    private bool isDashing = false;
    private float dashTimeLeft;
    private float cooldownTimeLeft;
    private bool isCooldown = false;

    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        StateInie();
    }
    private void StateInie()
    {
        health = playerData.health;
        moveSpeed = playerData.moveSpeed;
        dashSpeed = playerData.dashSpeed;
        dashDuration = playerData.dashDuration;
        dashCooldown = playerData.dashCooldown;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
}
