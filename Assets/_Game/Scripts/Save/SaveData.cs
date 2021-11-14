using System;

namespace DeadTired.Save
{
    [Serializable]
    public class SaveData
    {
        public int latestLevel;
        
        public SaveData()
        {}

        public SaveData(int latestLevel)
        {
            this.latestLevel = latestLevel;
        }
    }
}