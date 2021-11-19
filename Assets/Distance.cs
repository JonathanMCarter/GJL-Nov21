using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired
{
    public class Distance : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        Example();
    }
    void Example()
    {
        if(player)
        {
            float dist = Vector3.Distance(player.position, transform.position);
            Debug.Log("Distance to other: " + dist);

            AkSoundEngine.SetRTPCValue("Distance", dist);

        }
    }

}
}

