using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [Header("Follow Settings")]
    // 따라가야할 좌표 축 설정
    [SerializeField] private bool x;
    [SerializeField] private bool y;
    [SerializeField] private bool z;

    [Header("Targe Setting")]
    // 따라갈 대상
    [SerializeField] private Transform target;

    private void Update()
    {
        if ( !target )
            return;

        transform.position = new Vector3(
            x ? target.position.x : transform.position.x,
            y ? target.position.y : transform.position.y,
            z ? target.position.z : transform.position.z
        );
    }
}
