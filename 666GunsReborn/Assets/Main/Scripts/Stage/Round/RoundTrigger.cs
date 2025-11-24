using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoundTrigger : MonoBehaviour
{
    [Header("Round Trigger Settings")]
    // 라운드 컨트롤러 참조
    [SerializeField] private RoundController roundController;
    // 라운드 입장 트리거가 작동을 했는지 여부
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 트리거에 진입했을 경우 라운드 시작
        if (other.CompareTag("Player") && !isTriggered)
        {
            if (roundController != null)
            {
                // 라운드 시작
                isTriggered = true;
                roundController.StartRound();

                // 트리거는 더 이상 사용 안함으로 비활성화
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("RoundController가 비어있음!!");
            }
        }
    }
}
