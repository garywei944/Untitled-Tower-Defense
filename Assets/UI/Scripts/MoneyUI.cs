using UnityEngine;
using UnityEngine.UI;

namespace Sandbox.Gary
{
    public class MoneyUI : MonoBehaviour
    {
        private Text _moneyText;

        // Start is called before the first frame update
        private void Start()
        {
            _moneyText = gameObject.GetComponent<Text>();
        }

        // Update is called once per frame
        private void Update()
        {
            _moneyText.text = $"${PlayerStatus.Money}";
        }
    }
}
