using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired
{
    public class SpiritLineBehaviour : MonoBehaviour
    {

        public Transform playerObject;

        private Transform anchorObject;

        private LineRenderer lineRenderer;
        private bool trackPlayerToAnchor = false;

        // Start is called before the first frame update
        void Start()
        {
            lineRenderer = gameObject.GetComponent<LineRenderer>();

            //just in case but we want this deactivated at the start
            if(lineRenderer.enabled  == true)
            {
                lineRenderer.enabled  = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(trackPlayerToAnchor)
            {
                updateSpiritLine();
            }
        }

        public void activateSpiritLine(Transform playerTransform, Transform anchorTransform)
        {
            //want a reference to the locations of the player and the achor as either can move about.
            playerObject = playerTransform;
            anchorObject = anchorTransform;

            trackPlayerToAnchor = true;
            lineRenderer.enabled  = true;
        }

        public void deactiveSpiritLine()
        {
            
            trackPlayerToAnchor = false;
            lineRenderer.enabled  = false;

            playerObject = null;
            anchorObject = null;
        }

        private void updateSpiritLine()
        {
            //set the start and end positions of the spritline
            lineRenderer.SetPosition(0, playerObject.position);
            lineRenderer.SetPosition(1, anchorObject.localPosition);
        }
    }
}
