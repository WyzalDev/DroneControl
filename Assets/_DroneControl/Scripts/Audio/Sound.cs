using System;
using UnityEngine;

namespace _DroneControl.Audio
{
    [Serializable]
    public class Sound
    {
        public string name;
        
        public float volume;

        public AudioClip clip;
    }
}
