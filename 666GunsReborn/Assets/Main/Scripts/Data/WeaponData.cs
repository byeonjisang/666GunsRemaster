using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Datas/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    // 무기 아이콘 or 이미지
    public Sprite weaponSprite;
    // 무기 이름
    public string weaponName;
    // 무기 공격력
    public float power;
    // 공격 속도
    public float fireSpeed;
    // 공격 범위
    public float fireDistance;
    // 무게
    public float weight;
    // 무기 타입
    public Weapon.WeaponType weaponType;
    // 탄알 개수
    public int maxMagazine;
    // 재장전 속도
    public float reloadTime;

    // 총알 속도
    public float bulletSpeed;
}