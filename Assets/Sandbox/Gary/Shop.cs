using UnityEngine;

namespace Sandbox.Gary
{
    public class Shop : MonoBehaviour
    {
        public TurretDesign minigun;
        public TurretDesign missile;
        public TurretDesign laser;
        
        public void OnPurseMinigun()
        {
            Debug.Log("Purchase Minigun");
            BuildManager.Instance.SelectedTurret = minigun;
        }
        public void OnPurseMissile()
        {
            Debug.Log("Purchase Missile");
            BuildManager.Instance.SelectedTurret = missile;
        }
        public void OnPurseLaser()
        {
            Debug.Log("Purchase Laser");
            BuildManager.Instance.SelectedTurret = laser;
        }
    }
}
