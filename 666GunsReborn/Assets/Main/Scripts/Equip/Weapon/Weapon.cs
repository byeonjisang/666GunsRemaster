using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected WeaponType weaponType;
    public WeaponType Type => weaponType;
    //무기 데이터
    protected WeaponStats weaponStats;

    [Header("Weapon Settings")]
    [SerializeField] protected Transform bulletSpawnPoint;

    [Header("IK Targets")]
    [SerializeField] protected Transform rightHandTarget;
    [SerializeField] protected Transform leftHandTarget;

    private RigController rigController;
    private Coroutine disableCoroutine;


    // 무기 데이터 초기화
    public void Initialized(int index, WeaponType weaponType)
    {
        this.weaponType = weaponType;
        string path = "Datas/Weapon/" + weaponType.ToString();
        WeaponData weaponData = Resources.Load<WeaponData>(path);

        weaponStats = gameObject.AddComponent<WeaponStats>();
        weaponStats.Initialized(index, weaponData);

        // 무기 타입에 따른 IK 타겟 설정
        Player player = FindObjectOfType<Player>();
        rigController = player.GetComponentInChildren<RigController>();
        if (rigController != null)
        {
            if (index == 0)
            {
                rigController.ApplyRigTargets(rightHandTarget, leftHandTarget);
                rigController.ApplyConstrained((int)weaponType, transform);

                rigController.SetRigActive((int)weaponType, true);
                rigController.SetAttackRigActive((int)weaponType, false);
                rigController.RigBuilderRebuild();
            }
        }
        else
        {
            Debug.LogError("RigController not found on Player.");
        }
    }

    // 무기가 현재 발사 가능한지 체크
    public bool CanFire()
    {
        if (weaponStats.IsReloading())
            return false;
        return true;
    }

    // Default 총알 발사
    public virtual void Fire(GameObject bulletObject)
    {
        GameObject bullet = ObjectPoolManager.Instance.GetFromPool("Bullet", bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Bullet>().SetInfo(weaponStats.Damage, weaponStats.BulletSpeed);
        weaponStats.Fire();

        rigController.SetAttackRigActive((int)weaponType, true);
        if (disableCoroutine != null)
            StopCoroutine(disableCoroutine);
        disableCoroutine = StartCoroutine(DisableAttackRigAfterDelay(1.3f));
    }

    private IEnumerator DisableAttackRigAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        rigController.SetAttackRigActive((int)weaponType, false);
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

    public void SetWeaponRig(bool isActive = true)
    {
        if (rigController == null)
        {
            Player player = FindObjectOfType<Player>();
            RigController rigController = player.GetComponentInChildren<RigController>();
        }

        if (isActive)
            rigController.ApplyRigTargets(rightHandTarget, leftHandTarget);

        rigController.ApplyConstrained((int)weaponType, transform);
        rigController.SetRigActive((int)weaponType, isActive);
        rigController.RigBuilderRebuild();
    }
}