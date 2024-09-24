using System.Collections;
using UnityEngine;

namespace Character.Player.State
{
    public interface IPlayerState 
    {
        void Movement(PlayerController playerController);
    }
}