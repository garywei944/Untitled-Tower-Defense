using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sandbox.Gary
{
    public class WinLevel : MonoBehaviour
    {
        public SceneFader sceneFader;

        public void NextLevelBtnClicked()
        {
            sceneFader.FadeOut(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void MenuBtnClicked()
        {
            sceneFader.FadeOut(0);
        }
    }
}
