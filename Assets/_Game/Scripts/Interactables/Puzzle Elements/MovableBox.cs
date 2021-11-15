using DeadTired.Interactables;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class MovableBox : BaseInteraction, IInteractable
    {
        public void OnPlayerInteract()
        {
            // TODO - make the box move-able in the direction the player is facing...
        }

        protected override IInteractable GetInteractable() => this;
    }
}