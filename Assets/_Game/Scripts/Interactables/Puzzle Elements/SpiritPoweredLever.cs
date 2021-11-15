using UnityEngine;

namespace DeadTired.Interactables
{
    public class SpiritPoweredLever : Lever
    {
        private bool isPowered;
        
        
        public override void OnPlayerInteract()
        {
            if (!isPowered) return;
            base.OnPlayerInteract();
        }
    }
}