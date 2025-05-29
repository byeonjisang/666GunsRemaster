using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponTest : MonoBehaviour
{
    public GameObject weaponObject;

    private void Start()
    {
        weaponObject.transform.SetParent(this.transform); // ex: Hand_R
    }
}
