namespace Weapons
{
    public interface IWeapon
    {
        /// <summary>
        /// Initializes the weapon with the given index and weapon name.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="weaponName"></param>
        public void Initialization(int index, WeaponID weaponName);
        public void Fire();
    }    
}
   