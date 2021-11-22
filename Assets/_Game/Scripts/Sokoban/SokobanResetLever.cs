using System.Collections;
using DeadTired.Interactables;
using UnityEngine;

namespace DeadTired.Sokoban
{
    public class SokobanResetLever : Lever
    {
        public override void OnPlayerInteract()
        {
            base.OnPlayerInteract();
            leverPulled = false;
        }
    }
}