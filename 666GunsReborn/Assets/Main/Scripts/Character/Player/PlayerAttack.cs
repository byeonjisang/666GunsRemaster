using UnityEngine;

namespace Character.Player
{
    public class PlayerAttack
    {
        // 플레이어 참조
        private Player _player;

        private bool _isAttacking = false;
        public bool IsAttacking => _isAttacking;

        // 생성자
        public PlayerAttack(Player player)
        {
            _player = player;
        }

        // 버튼을 누른 순간 호출
        public void RequestAttack()
        {
            _isAttacking = true;
            _player.Anim.SetBool("IsAttack", true);
            Weapons.WeaponManager1.Instance.OnFire();
        }

        // 버튼에서 손 뗀 순간 호출
        public void CancelAttackRequest()
        {
            _isAttacking = false;
            _player.Anim.SetBool("IsAttack", false);
            Weapons.WeaponManager1.Instance.OffFire();
        }
    }
}