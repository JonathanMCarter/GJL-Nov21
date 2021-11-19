using UnityEngine;

namespace DeadTired.UI
{
    public class QuitPanel : BasePanel
    {
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}