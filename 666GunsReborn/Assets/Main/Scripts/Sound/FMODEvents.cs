using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

#region Sound Enums
public enum SFX
{
    GunShotSFX,
}

public enum Music
{
    LobbyBGM,
    InGameBGM,
}

public enum Ambience
{

}
#endregion

public class FMODEvents : Singleton<FMODEvents>
{
    [field: Header("SFX")]
    [field: SerializeField]
    public List<EventReference> SFX { get; private set; }

    [field: Header("Music")]
    [field: SerializeField]
    public List<EventReference> Music { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField]
    public List<EventReference> Ambience { get; private set; }
}