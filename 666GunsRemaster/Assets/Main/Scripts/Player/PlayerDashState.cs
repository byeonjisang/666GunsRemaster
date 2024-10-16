using Character.Player.State;
using System.Collections;
using UnityEngine;

namespace Character.Player
{
    public class PlayerDashState : MonoBehaviour, IPlayerState
    {
        private PlayerController _playerController;

        private Rigidbody2D rigid;

        private float dashTimeLeft;
        private bool isDashing = false;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        public void Movement(PlayerController playerController)
        {
            if (_playerController == null)
            {
                _playerController = playerController;
            }

            dashTimeLeft = _playerController.DashDuration;
            isDashing = true;
        }

        private void FixedUpdate()
        {
            if (isDashing)
            {
                Vector2 tmpDir = new Vector2(_playerController.Joystick.Horizontal, _playerController.Joystick.Vertical);

                rigid.velocity = tmpDir.normalized * _playerController.DashSpeed * 5 * Time.fixedDeltaTime;
                dashTimeLeft -= Time.deltaTime;

                if(dashTimeLeft <= 0)
                {
                    isDashing = false;
                    rigid.velocity = Vector2.zero;
                }
            }
        }

        //private IEnumerator DashCoroutine()
        //{
        //    Vector2 tmpDir = new Vector2(_playerController.Joystick.Horizontal, _playerController.Joystick.Vertical);

        //    // 대쉬 방향이 0일 경우 대쉬하지 않음
        //    if (tmpDir == Vector2.zero)
        //    {
        //        yield break; // 대쉬하지 않고 코루틴 종료
        //    }

        //    float dashTimeLeft = _playerController.DashDuration;

        //    while (dashTimeLeft > 0)
        //    {
        //        rigid.velocity = tmpDir.normalized * _playerController.DashSpeed;
        //        dashTimeLeft -= Time.deltaTime;
        //        yield return null; // 다음 프레임까지 대기
        //    }

        //    // 대쉬가 끝나면 속도를 초기화합니다.
        //    rigid.velocity = Vector2.zero;
        //}
    }
}