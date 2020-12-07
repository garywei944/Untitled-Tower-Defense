using UnityEngine;

namespace Sandbox.Gary
{
    public class CameraMove : MonoBehaviour
    {
        public float moveSpeed = 50f;
        public float scrollSpeed = 1000f;
        public float space = 50f;

        private void Update()
        {
            if (Input.GetKey(KeyCode.A) || Input.mousePosition.x < space)
            {
                transform.position += Vector3.left * (moveSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) || Input.mousePosition.x > Screen.width - space)
            {
                transform.position += Vector3.right * (moveSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W) || Input.mousePosition.y > Screen.height - space)
            {
                transform.position += Vector3.forward * (moveSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S) || Input.mousePosition.y < space)
            {
                transform.position += Vector3.back * (moveSpeed * Time.deltaTime);
            }

            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                transform.position += Vector3.down * (scroll * scrollSpeed * Time.deltaTime);
                if (transform.position.y < 20)
                {
                    transform.position += Vector3.up * (20 - transform.position.y);
                }

                if (transform.position.y > 80)
                {
                    transform.position += Vector3.up * (80 - transform.position.y);
                }
            }
        }
    }
}
