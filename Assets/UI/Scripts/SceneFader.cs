using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Sandbox.Gary
{
    public class SceneFader : MonoBehaviour
    {
        public AnimationCurve curve;
        public Image blackImg;

        // Start is called before the first frame update
        private void Start()
        {
            // GameManager.Instance.ResetGame();
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            var t = 1.0f;
            while (t > 0)
            {
                t -= Time.deltaTime;
                var a = curve.Evaluate(t);
                blackImg.color = new Color(0, 0, 0, a);
                yield return null;
            }
        }

        public void FadeOut(int index)
        {
            StartCoroutine(FadeOutByTime(index));
        }

        private IEnumerator FadeOutByTime(int index)
        {
            var t = 0.0f;
            while (t < 1)
            {
                t += Time.deltaTime;
                var a = curve.Evaluate(t);
                blackImg.color = new Color(0, 0, 0, a);
                yield return null;
            }

            SceneManager.LoadScene(index, LoadSceneMode.Single);
        }
    }
}
