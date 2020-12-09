using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Sandbox.Gary
{
    public class NodeUI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public GameObject ui;
        public Text costText;
        public Text sellText;
        public Button upgradeBtn;

        private Node _target;

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
            transform.position = target.GetUIPosition();

            // Disable Upgrade if upgraded
            if (target.isUpgraded)
            {
                costText.text = "- Complete -";
                upgradeBtn.interactable = false;
            }
            else
            {
                costText.text = $"${target.selectedTurretDesign.upgradeCost}";
                upgradeBtn.interactable = true;
            }

            sellText.text = $"${target.selectedTurretDesign.SellPrice}";
        }

        public void UpgradeBtnClicked()
        {
            _target.UpgradeTurret();
            BuildManager.Instance.Unselect();
        }

        public void SellBtnClicked()
        {
            _target.SellTurret();
            BuildManager.Instance.Unselect();
        }
    }
}
