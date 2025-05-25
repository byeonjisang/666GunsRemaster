using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //무기 데이터
    protected WeaponStats weaponStats;
    //총알 오브젝트
    protected GameObject bulletObject;


    // 무기 데이터 초기화
    public void Initialized(int index, WeaponType weaponType)
    {
        string path = "Datas/Weapon/" + weaponType.ToString();
        WeaponData weaponData = Resources.Load<WeaponData>(path);

        weaponStats = gameObject.AddComponent<WeaponStats>();
        weaponStats.Initialized(index, weaponData);
    }

    // 무기가 현재 발사 가능한지 체크
    public bool CanFire()
    {
        if (weaponStats.IsReloading())
            return false;
        return true;
    }

    // Default 총알 발사
    public virtual void Fire(GameObject bulletObject, Transform bulletPos, Quaternion bulletRot)
    {
        GameObject bullet = ObjectPoolManager.Instance.GetFromPool("Bullet", bulletPos.position, bulletRot);
        bullet.GetComponent<Bullet>().SetInfo(weaponStats.Damage, weaponStats.BulletSpeed, bullet.transform.position);
        weaponStats.Fire();
    }

    public Sprite GetWeaponSprite()
    {
        return weaponStats.WeaponSprite;
    }

    public int[] GetBullet()
    {
        int[] bullet = new int[2];
        bullet[0] = weaponStats.MaxMagazine;
        bullet[1] = weaponStats.CurrentMagazine;
        return bullet;
    }
}