using UnityEngine;

namespace Sandbox.Gary
{
    public class Shop : MonoBehaviour
    {
        public TurretDesign minigunPrefab;
        public TurretDesign missilePrefab;
        
        public void OnPurseMinigun()
        {
            Debug.Log("Purchase Minigun");
            BuildManager.Instance.SelectedTurret = minigunPrefab;
        }
        public void OnPurseMissile()
        {
            Debug.Log("Purchase Missile");
            BuildManager.Instance.SelectedTurret = missilePrefab;
        }
        public void OnPurseLaser()
        {
            Debug.Log("Purchase Laser");
            BuildManager.Instance.SelectedTurret = minigunPrefab;
        }
    }
}
