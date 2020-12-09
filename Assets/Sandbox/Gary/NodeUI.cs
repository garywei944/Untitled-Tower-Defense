using System.Collections;
using UnityEngine;

namespace Sandbox.Gary
{
    public class NodeUI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public GameObject ui;

        private Node _target;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ShowUI();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                HideUI();
            }
        }

        public void ShowUI()
        {
            StartCoroutine(FadeIn());
        }

        public void HideUI()
        {
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeIn()
        {
            canvasGroup.alpha = 0;
            ui.SetActive(true);
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime * 5;
                yield return null;
            }
        }

        private IEnumerator FadeOut()
        {
            canvasGroup.alpha = 1;
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime * 5;
                yield return null;
            }

            ui.SetActive(false);
        }

        public void SetTarget(Node target)
        {
            _target = target;
            transform.position = _target.GetUIPosition();
        }
    }
}
