using Character.Player.State;
using System.Collections;
using UnityEngine;

namespace Character.Player
{
    public class PlayerDashState : MonoBehaviour, IPlayerState
    {
        private PlayerController _playerController;
        private Ghost ghost;

        private Rigidbody2D rigid;
        private Animator anim;

        private int _dashCount;
        [SerializeField]
        private int _currentDashCount;
        private float _dashSpeed;
        private float _dashDuration;
        private float _currentDashDuration;
        private float _dashCooldown;
        private float _fillInTime;

        private bool _isDashing = false;
        private bool _isCooldown = false;
        private bool _isFillIn = false;

        private void Awake()
        {
            ghost = GetComponent<Ghost>();

            rigid = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void DashStateInit()
        {
            _dashCount = _playerController.DashCount;
            _currentDashCount = _dashCount;
            _dashSpeed = _playerController.DashSpeed;
            _dashDuration = _playerController.DashDuration;
            _currentDashDuration = _dashDuration;
            _dashCooldown = _playerController.DashCooldown;
            _fillInTime = _playerController.DashFillInTime;
        }

        public void Movement(PlayerController playerController)
        {
            if (_playerController == null)
            {
                _playerController = playerController;
                DashStateInit();
            }

            if (_isCooldown || _currentDashCount <= 0)
                return;

            _currentDashCount -= 1;
            _isDashing = true;
            _isCooldown = true;
            ghost.makeGhost = true;

            //대쉬 쿨타임 측정 시작
            StartCoroutine(DashCoolDown());

            //대쉬 개수 채우는 중
            StartCoroutine(FillInDash());
        }

        private void FixedUpdate()
        {
            //대쉬 시작
            if (_isDashing)
            {
                Vector2 tmpDir = new Vector2(_playerController.Joystick.Horizontal, _playerController.Joystick.Vertical);

                rigid.velocity = tmpDir.normalized * _dashSpeed;
                _currentDashDuration -= Time.deltaTime;
                anim.SetBool("IsDash", true);

                //대쉬 소리 재생

                //대쉬 지속시간
                if(_currentDashDuration <= 0)
                {
                    _isDashing = false;
                    _currentDashDuration = _dashDuration;
                    rigid.velocity = Vector2.zero;
                    anim.SetBool("IsDash", false);
                    ghost.makeGhost = false;
                }
            }
        }

        //대쉬 쿨타임 측정
        private IEnumerator DashCoolDown()
        {
            yield return new WaitForSeconds(_dashCooldown);

            _isCooldown = false;
        }

        private IEnumerator FillInDash()
        {
            if (_isFillIn)
                yield break;

            _isFillIn = true;
            yield return new WaitForSeconds(_fillInTime);

            _currentDashCount += 1;
            _isFillIn = false;
            if (_currentDashCount < _dashCount)
            {
                StartCoroutine(FillInDash());
            }
        }
    }
}