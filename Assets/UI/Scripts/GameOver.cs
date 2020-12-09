using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Sandbox.Gary
{
    public class GameOver : MonoBehaviour
    {
        public Text roundsText;
        public SceneFader sceneFader;

        private void OnEnable()
        {
            StartCoroutine(ShowRoundsText());
        }

        public void RetryBtn()
        {
            sceneFader.FadeOut(SceneManager.GetActiveScene().buildIndex);
        }

        public void MenuBtn()
        {
            sceneFader.FadeOut(0);
        }

        private IEnumerator ShowRoundsText()
        {
            var rounds = 0;
            roundsText.text = rounds.ToString();
            yield return new WaitForSeconds(0.5f);

            while (rounds < PlayerStatus.Rounds)
            {
                rounds++;
                roundsText.text = rounds.ToString();
                yield return new WaitForSeconds(0.07f);
            }
        }
    }
}
