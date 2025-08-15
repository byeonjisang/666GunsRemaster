namespace Weapons
{
    public interface IWeapon
    {
        public void Initialization(int index, WeaponID weaponName);
        public void Fire();
        public void StopFire();
    }    
}
   