using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponStats : MonoBehaviour
{
    private int index;

    private Sprite weaponSprite;
    public Sprite WeaponSprite { get {return weaponSprite; } private set { } }
    private string weaponName;
    private float power;
    public float Power { get {return power; } private set { } }
    private float fireSpeed;
    private float fireCooldown;
    private float fireDistance;
    public float FireDistance { get {return fireDistance; } private set { } }
    private float weight;
    private WeaponType weaponType;
    private int currentMagazine;
    public int CurrentMagazine { get {return currentMagazine; } private set { } }
    private int maxMagazine;
    public int MaxMagazine { get {return maxMagazine; } private set { } }
    
    private float currentReloadTime;
    private float reloadTime;

    private float bulletSpeed;
    public float BulletSpeed { get {return bulletSpeed; } private set { } }

    public void Initialized(int index, WeaponData weaponData)
    {
        this.index = index;
        weaponSprite = weaponData.weaponSprite;
        weaponName = weaponData.weaponName;
        power = weaponData.power;
        fireSpeed = weaponData.fireSpeed;
        fireDistance = weaponData.fireDistance;
        weight = weaponData.weight;
        weaponType = weaponData.weaponType;
        reloadTime = weaponData.reloadTime;
        maxMagazine = weaponData.maxMagazine;
        bulletSpeed = weaponData.bulletSpeed;

        currentMagazine = maxMagazine;
        currentReloadTime = 0f;
        fireCooldown = 0f;
    }

    private void Update()
    {
        if (currentReloadTime > 0)
        {
            currentReloadTime -= Time.deltaTime;
            WeaponUIEvents.OnUpdateReloadSlider?.Invoke(index, reloadTime, currentReloadTime);
            if (currentReloadTime <= 0)
            {
                Reload();
            }
        }

        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
            if (fireCooldown < 0)
            {
                fireCooldown = 0;
            }
        }
    }

    public bool IsReloading()
    {
        return currentReloadTime > 0;
    }

    public bool IsReadyToFire()
    {
        Debug.Log($"IsReadyToFire: fireCooldown={fireCooldown}, currentReloadTime={currentReloadTime}");
        return fireCooldown <= 0 && currentReloadTime <= 0;
    }

    public void Fire()
    {
        currentMagazine--;
        fireCooldown = 1.0f / fireSpeed;
        if (currentMagazine <= 0)
        {
            currentReloadTime = reloadTime;
        }
    }

    private void Reload()
    {
        currentMagazine = maxMagazine;
        WeaponUIEvents.OnUpdateBulletUI?.Invoke(index, maxMagazine, currentMagazine);
    }
}