using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform shakeCamera;
    private bool isShakeRotate = false;
    public bool shakeOption = true;

    //흔들릴 Transform 값
    public Vector3 originPos;
    public Quaternion originRot;

    void Awake()
    {
        originPos = shakeCamera.localPosition;
        originRot = shakeCamera.localRotation;
    }

    void Update()
    {
        //테스트용(추후 옮겨질 예정)
        if(Input.GetKey(KeyCode.Z))
        {
            StartCoroutine(Shake());
        }
    }

    public void ShakeOptionSetting(bool value)
    {
        shakeOption = value;
    }

    public IEnumerator Shake(float dur = 0.01f, float magnitudePos = 0.03f, float magnitudeRot = 0.1f)
    {
        float time = 0f;

        while (time < dur)
        {
            Vector3 shakePos = Random.insideUnitSphere;
            shakeCamera.localPosition = shakePos * magnitudePos;

            if (isShakeRotate && shakeOption)
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
