using Assets.YMH.Script;
using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //무기 데이터
    protected WeaponData weaponData;
    //총알 오브젝트
    protected GameObject bulletObject;

    public void Init(WeaponType weaponType)
    {
        string path = "Datas/Weapon/" + weaponType.ToString();
        weaponData = Resources.Load<WeaponData>(path);
    }

    public void Fire(GameObject bulletObject, Transform bulletPos, Quaternion bulletRot)
    {
        //GameObject bullet = Instantiate(bulletObject, bulletPos.position, bulletRot);
        GameObject bullet = ObjectPool.Instance.GetBullet();
        bullet.transform.position = bulletPos.position;
        bullet.transform.rotation = bulletRot;
        bullet.GetComponent<Bullet>().SetSpeed(weaponData.damage, weaponData.bulletSpeed);
    }
}