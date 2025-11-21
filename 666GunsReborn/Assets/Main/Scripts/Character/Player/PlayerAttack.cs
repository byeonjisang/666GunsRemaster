using UnityEngine;

namespace Character.Player
{
    public class PlayerAttack
    {
        // 플레이어 참조
        private Player _player;
        private bool attackRequested = false;

        private bool _isAttacking = false;

        // 생성자
        public PlayerAttack(Player player)
        {
            _player = player;
        }

        // 버튼을 누른 순간 호출
        public void RequestAttack()
        {
            attackRequested = true;
            Weapons.WeaponManager1.Instance.OnFire();
        }

        // 버튼에서 손 뗀 순간 호출
        public void CancelAttackRequest()
        {
            attackRequested = false;
            if (_isAttacking)
                StopAttack();
        }

        // 공격 시작
        public void StartAttack()
        {
            if (_isAttacking)
                return;

            _isAttacking = true;
            _player.Anim.SetBool("IsAttack", true);
        }

        // 공격 중지
        public void StopAttack()
        {
            _isAttacking = false;
            _player.Anim.SetBool("IsAttack", false);
            Weapons.WeaponManager1.Instance.OffFire();
        }


        private void Update()
        {
            // 1) 공격 요청이 켜져 있고, 아직 공격 중이 아니면서 발사 가능하면 발사 시작
            if (attackRequested && !_isAttacking)
            {
                StartAttack();
            }
        }  
    }
}