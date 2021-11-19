using CarterGames.Assets.BuildVersions;
using TMPro;
using UnityEngine;

namespace DeadTired.UI
{
    public class BuildInformationDisplay : MonoBehaviour
    {
        [SerializeField] private BuildInformation buildInformation;
        private TMP_Text display;
        
        private void Awake()
        {
            display = GetComponent<TMP_Text>();
            display.text = $"Build #{buildInformation.BuildNumber} ({buildInformation.BuildDate})";
        }
    }
}