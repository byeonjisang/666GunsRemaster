using UnityEngine;

namespace Weapons
{
    public interface IWeapon
    {
        public void Initialization(int index, WeaponID weaponName);
        public GameObject GetGameObject();
        public WeaponType GetWeaponType();
        public void Fire();
        public void StopFire();
    }
}
   