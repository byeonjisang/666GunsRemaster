using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScopeConroller : MonoBehaviour
{
    [Header("Scopre Setting")]
    //[SerializeField] private List<GameObject> scopeCameraObject;
    [SerializeField] private GameObject scopeUIObject;

    // 시작하면 스포크 ui 비활성화
    private void Start()
    {
        if (scopeUIObject != null)
            scopeUIObject.SetActive(false);
    }

    /// <summary>
    /// 스코프 조준 모드 활성화/비활성화
    /// </summary>
    /// <param name="isAiming"></param>
    public void Aim(bool isAiming, GameObject scopeCameraObject)
    {
        if (scopeCameraObject != null)
            scopeCameraObject.SetActive(isAiming);
        if (scopeUIObject != null)
            scopeUIObject.SetActive(isAiming);
    }
}
