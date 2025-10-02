using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : TopDownController
{
    public void OnInteract()
    {
        CallInteractEvent(); // 상호작용 이벤트 실행 함수 호출
    }
}
