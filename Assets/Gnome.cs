using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired
{
    public class Gnome : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        AkSoundEngine.PostEvent("Gnomes", gameObject);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
