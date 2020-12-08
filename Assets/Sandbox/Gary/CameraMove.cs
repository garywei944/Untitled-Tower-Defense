using UnityEngine;

namespace Sandbox.Gary
{
    public class CameraMove : MonoBehaviour
    {
        public float moveSpeed = 50f;
        public float scrollSpeed = 1000f;
        public float space = 50f;

        public float minX = -65;
        public float maxX = 0;
        public float minY = 20;
        public float maxY = 80;
        public float minZ = -15;
        public float maxZ = 60;


        private void Update()
        {
            var pos = transform.position;
            if (Input.GetKey(KeyCode.A) || IsInScreen() && Input.mousePosition.x < space)
            {
                pos += Vector3.left * (moveSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) || IsInScreen() && Input.mousePosition.x > Screen.width - space)
            {
                pos += Vector3.right * (moveSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W) || IsInScreen() && Input.mousePosition.y > Screen.height - space)
            {
                pos += Vector3.forward * (moveSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S) || IsInScreen() && Input.mousePosition.y < space)
            {
                pos += Vector3.back * (moveSpeed * Time.deltaTime);
            }

            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                pos += Vector3.down * (scroll * scrollSpeed * Time.deltaTime);
            }

            // Check position range
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
            transform.position = pos;
        }

        private static bool IsInScreen()
        {
            return Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width && Input.mousePosition.y >= 0 &&
                   Input.mousePosition.y <= Screen.height;
        }
    }
}
