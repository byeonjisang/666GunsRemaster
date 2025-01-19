using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToScreen : MonoBehaviour
{
    public GameObject gameObject;
    public Camera mainCamera;

    void Start()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                gameObject.transform.position = hit.point;
            }
        }
    }
}
