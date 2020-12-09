using UnityEngine;
using UnityEngine.UI;

namespace Sandbox.Gary
{
    public class LivesUI : MonoBehaviour
    {
        private Text livesText;

        // Start is called before the first frame update
        private void Start()
        {
            livesText = gameObject.GetComponent<Text>();
        }

        // Update is called once per frame
        private void Update()
        {
            livesText.text = $"HP: {PlayerStatus.Lives}";
        }
    }
}
