using Character.Player.State;
using Gun;
using UnityEngine;

namespace Character.Player
{
    public class PlayerDieState : MonoBehaviour, IPlayerState
    {
        private PlayerController _playerController;

        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();

            _playerController = GetComponent<PlayerController>();
        }

        public void Movement(PlayerController playerController)
        {
            if (!_playerController)
            {
                _playerController = playerController;
            }

            PlayerDie();
        }
        
        private void PlayerDie()
        {
            _playerController.CurrentSpeed = 0;
            anim.SetTrigger("Die");
            //사망 사운드
        }
    }
}