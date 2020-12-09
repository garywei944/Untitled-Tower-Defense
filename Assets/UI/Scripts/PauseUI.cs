using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sandbox.Gary
{
    public class PauseUI : MonoBehaviour
    {
        public SceneFader sceneFader;
        public GameObject ui;

        public void ContinueBtnClicked()
        {
            SwitchUI();
        }

        public void RetryBtnClicked()
        {
            SwitchUI();
            sceneFader.FadeOut(SceneManager.GetActiveScene().buildIndex);
        }

        public void MenuBtnClicked()
        {
            SwitchUI();
            sceneFader.FadeOut(0);
        }

        public void SwitchUI()
        {
            ui.SetActive(!ui.activeSelf);
            Time.timeScale = ui.activeSelf ? 0 : 1;
        }
    }
}
