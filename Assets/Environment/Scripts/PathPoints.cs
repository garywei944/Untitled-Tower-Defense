using UnityEngine;

namespace Sandbox.Gary
{
    public class PathPoints : MonoBehaviour
    {
        public static Transform[] pathPoints;

        private void Awake()
        {
            pathPoints = new Transform[transform.childCount];
            for (var i = 0; i < pathPoints.Length; i++)
            {
                pathPoints[i] = transform.GetChild(i);
            }
        }
    }
}
