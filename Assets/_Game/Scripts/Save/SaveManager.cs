using System;
using UnityEngine;

namespace DeadTired.Save
{
    public class SaveManager: MonoBehaviour
    {
        public void SaveGame(SaveData data)
        {
            PlayerPrefs.SetInt(SaveKeys.PlayerLatestLevelKey, data.latestLevel);
        }
        
        
        public void OnApplicationFocus(bool hasFocus)
        {
            throw new NotImplementedException();
        }

        public void OnApplicationPause(bool pauseStatus)
        {
            throw new NotImplementedException();
        }

        public void OnApplicationQuit()
        {
            throw new NotImplementedException();
        }
    }
}