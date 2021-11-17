using DeadTired.UI;
using JTools.Editor;
using UnityEditor;

namespace DeadTired.Editor
{
    [CustomEditor(typeof(PlayerOrbDisplay))]
    public class PlayerOrbDisplayEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Button.ColourButton("Test Orb UI", Colours.Blue, CallUpdateDisplay);
        }


        private void CallUpdateDisplay()
        {
            PlayerOrbDisplay.OnOrbCountChanged?.Invoke();
        }
    }
}