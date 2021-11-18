using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace DeadTired
{
    public class GlobalVolumeManager : MonoBehaviour
    {

        private Volume globalVolume;
        public VolumeProfile bodyProfile;
        public VolumeProfile ghostProfile;
 
        // Start is called before the first frame update
        void Start()
        {
            globalVolume = gameObject.GetComponent<Volume>();
        }

        public void setBodyVolume()
        {
            globalVolume.profile = bodyProfile;
        }

        public void setGhostvolume()
        {
            globalVolume.profile = ghostProfile;
        }
    }
}
