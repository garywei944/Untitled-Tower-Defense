using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;

namespace Sandbox.Gary
{
    public class Node : MonoBehaviour
    {
        public Color hoverColor = Color.gray;
        public Color noEnoughMoneyColor = Color.magenta;
        public Vector3 offset = new Vector3(0, 0.5f, 0);
        public Vector3 uiOffset = new Vector3(0, 5f, 5f);
        public bool isUpgraded;

        [HideInInspector] public TurretDesign selectedTurretDesign;
        private Color _initColor;
        private Renderer _render;
        private GameObject _turret;

        // Start is called before the first frame update
        private void Start()
        {
            _render = GetComponent<MeshRenderer>();
            _initColor = _render.material.color;
        }

        private void OnMouseEnter()
        {
            // Change color of the node
            if (BuildManager.Instance.CanBuild)
            {
                if (BuildManager.Instance.HasEnoughMoney)
                {
                    _render.material.color = hoverColor;
                }
                else
                {
                    _render.material.color = noEnoughMoneyColor;
                }
            }
        }

        private void OnMouseExit()
        {
            _render.material.color = _initColor;
        }

        private void OnMouseDown()
        {
            // Return if it is clicked over UI
            if (EventSystem.current.IsPointerOverGameObject()) return;

            // Return if there is already a turret on the node
            if (_turret)
            {
                // Display NodeUI
                BuildManager.Instance.SelectNode(this);

                return;
            }

            // Unselect node
            BuildManager.Instance.Unselect();

            // Build turret if has enough money
            // Return if no turret selected
            if (!BuildManager.Instance.CanBuild) return;
            if (BuildManager.Instance.HasEnoughMoney)
            {
                BuildTurret();
                Debug.Log($"Balance: {PlayerStatus.Money}");
            }
            else
            {
                Debug.Log("No enough Balance");
            }
        }

        private void BuildTurret()
        {
            PlayerStatus.Money -= BuildManager.Instance.SelectedTurret.cost;
            _turret = Instantiate(BuildManager.Instance.SelectedTurret.prefab, GetPosition(), Quaternion.identity);
            selectedTurretDesign = BuildManager.Instance.SelectedTurret;
        }

        private Vector3 GetPosition()
        {
            return transform.position + offset;
        }

        public Vector3 GetUIPosition()
        {
            return transform.position + uiOffset;
        }

        public void UpgradeTurret()
        {
            // Return if no enough money
            if (PlayerStatus.Money < selectedTurretDesign.upgradeCost)
            {
                Debug.Log("No enough money to upgrade");
                return;
            }

            isUpgraded = true;

            PlayerStatus.Money -= selectedTurretDesign.upgradeCost;

            Destroy(_turret);
            _turret = Instantiate(selectedTurretDesign.upgradedPrefab, GetPosition(), Quaternion.identity);
        }

        public void SellTurret()
        {
            PlayerStatus.Money += selectedTurretDesign.SellPrice;
            Destroy(_turret);
            selectedTurretDesign = null;
            isUpgraded = false;
        }
    }
}
