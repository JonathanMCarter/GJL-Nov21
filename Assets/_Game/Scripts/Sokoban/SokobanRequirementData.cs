using System;

namespace DeadTired.Sokoban
{
    [Serializable]
    public class SokobanRequirementData
    {
        public string id;
        public bool isComplete;
        
        public SokobanRequirementData(){}

        public SokobanRequirementData(string id, bool isComplete)
        {
            this.id = id;
            this.isComplete = isComplete;
        }
    }
}