using Character.Player;
using Character.Player.State;
using System.Collections;
using UnityEngine;

namespace Character.Player
{
    public class PlayerStopState : MonoBehaviour, IPlayerState
    {
        private PlayerController _playerController;

        private Rigidbody2D rigid;
        private Animator anim;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }
        public void Movement(PlayerController playerController)
        {
            if (!_playerController)
            {
                _playerController = playerController;
            }
            
            playerController.CurrentSpeed = 0;
            StopMovement();
        }

        private void StopMovement()
        {
            rigid.velocity = Vector2.zero;
            anim.SetFloat("Speed", rigid.velocity.magnitude);
        }
    }
}