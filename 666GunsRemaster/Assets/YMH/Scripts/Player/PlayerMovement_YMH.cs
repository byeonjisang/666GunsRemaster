using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_YMH
{
    private void HandleMovement(FloatingJoystick joystick, Rigidbody2D rigid, Animator anim, SpriteRenderer sprite,float moveSpeed)
    {
        float joyStickInputX = joystick.Horizontal;
        float joyStickInputY = joystick.Vertical;

        float keyboardInputX = Input.GetAxis("Horizontal");
        float keyboardInputY = Input.GetAxis("Vertical");

        float finalInputX = joyStickInputX + keyboardInputX;
        float finalInputY = joyStickInputY + keyboardInputY;

        rigid.velocity = new Vector2(finalInputX * moveSpeed, finalInputY * moveSpeed);

        anim.SetFloat("Speed", rigid.velocity.magnitude);

        if (finalInputX != 0)
            sprite.flipX = finalInputX > 0;
    }
}
