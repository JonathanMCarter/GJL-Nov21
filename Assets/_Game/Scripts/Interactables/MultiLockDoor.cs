namespace DeadTired.Interactables
{
    public class MultiLockDoor : BaseInteraction, IInteractable
    {
        public int numberOfLocks;

        public void OnPlayerInteract()
        {
            
        }

        protected override IInteractable GetInteractable() => this;
    }
}