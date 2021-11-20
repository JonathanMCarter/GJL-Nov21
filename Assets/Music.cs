using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired
{
    public class Music : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            AkSoundEngine.PostEvent("Play_Music", gameObject);
        }

        // Update is called once per frame
        
    }
}
