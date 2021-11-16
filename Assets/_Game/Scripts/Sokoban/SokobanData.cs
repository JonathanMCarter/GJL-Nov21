using System;

namespace DeadTired.Sokoban
{
    [Serializable]
    public class SokobanData
    {
        public string id;
        public bool isComplete;
        
        public SokobanData(){}

        public SokobanData(string id, bool isComplete)
        {
            this.id = id;
            this.isComplete = isComplete;
        }
    }
}