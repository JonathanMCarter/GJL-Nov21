namespace DeadTired.Interactables
{
    public class MultiLockDoor : LockedDoor
    {
        public int numberOfLocks;
        public int numberUnlocked;
        

        public override void UnlockDoor()
        {
            numberUnlocked++;
            CheckForComplete();
        }

        private void CheckForComplete()
        {
            if (!numberUnlocked.Equals(numberOfLocks)) return;
            IsUnlocked = true;
        }
    }
}