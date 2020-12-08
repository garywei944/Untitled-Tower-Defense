using UnityEngine;

namespace Sandbox.Gary
{
    public class Shop : MonoBehaviour
    {
        public GameObject standardTurret;
        
        public void OnPurseMinigun()
        {
            Debug.Log("Purchase Minigun");
            BuildManager.Instance.SelectedTurret = standardTurret;
        }
        public void OnPurseMissile()
        {
            Debug.Log("Purchase Missile");
            BuildManager.Instance.SelectedTurret = standardTurret;
        }
        public void OnPurseLaser()
        {
            Debug.Log("Purchase Laser");
            BuildManager.Instance.SelectedTurret = standardTurret;
        }
    }
}
