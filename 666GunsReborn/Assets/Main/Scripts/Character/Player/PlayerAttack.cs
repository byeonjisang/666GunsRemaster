using UnityEngine;

namespace Character.Player
{
    public class PlayerAttack
    {
        // 플레이어 참조
        private Player _player;

        // 생성자
        public PlayerAttack(Player player)
        {
            _player = player;
        }

        // 버튼을 누른 순간 호출
        public void RequestAttack()
        {
            _player.Anim.SetBool("IsAttack", true);
            _player.WeaponManager.OnFire();
        }

        // 버튼에서 손 뗀 순간 호출
        public void CancelAttackRequest()
        {
            _player.Anim.SetBool("IsAttack", false);
            _player.WeaponManager.OffFire();
        }
    }
}