using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired
{
    public class GnomeStart : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
                AkSoundEngine.PostEvent("PlayOrbz", gameObject);
        }

        // Update is called once per frame
        
    }
}
