using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player 
{
    public class PlayerManager : MonoBehaviour
    {
        //�̱���
        public static PlayerManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
    }
}