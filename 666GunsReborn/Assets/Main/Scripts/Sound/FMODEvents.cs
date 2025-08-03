using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : Singleton<FMODEvents>
{
    [field: Header("Gun SFX")]
    [field: SerializeField]
    public EventReference GunShotSFX { get; private set; }

    [field: Header("BGM")]
    [field: SerializeField]
    public EventReference BGM { get; private set; }

    
}
