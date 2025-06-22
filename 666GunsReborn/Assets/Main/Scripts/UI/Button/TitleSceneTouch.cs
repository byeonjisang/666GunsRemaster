using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneTouch : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            FadeManager.Instance.FadeAndLoadScene("Lobby");
        }
    }
}
