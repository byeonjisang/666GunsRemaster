using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform shakeCamera;
    private bool isShakeRotate = false;

    //Èçµé¸± Transform °ª
    public Vector3 originPos;
    public Quaternion originRot;

    void Awake()
    {
        originPos = shakeCamera.localPosition;
        originRot = shakeCamera.localRotation;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            StartCoroutine(Shake());
        }
    }

    public IEnumerator Shake(float dur = 0.01f, float magnitudePos = 0.03f, float magnitudeRot = 0.1f)
    {
        float time = 0f;

        while (time < dur)
        {
            Vector3 shakePos = Random.insideUnitSphere;
            shakeCamera.localPosition = shakePos * magnitudePos;

            if (isShakeRotate)
            {
                Vector3 shakeRot = new Vector3(0, 0,  Mathf.PerlinNoise(magnitudeRot, magnitudePos));

                shakeCamera.localRotation = Quaternion.Euler(shakeRot);
            }

            time += Time.deltaTime;
            yield return null;
        }

        originPos = shakeCamera.localPosition;
        originRot = shakeCamera.localRotation;
    }
}
