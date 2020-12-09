using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;

namespace Sandbox.Gary
{
    public class Node : MonoBehaviour
    {
        public Vector3 offset = new Vector3(0, 0.5f, 0);

        public Color hoverColor = Color.gray;
        public Color noEnoughMoneyColor = Color.magenta;
        private Color _initColor;
        private Renderer _render;

        // Start is called before the first frame update
        private void Start()
        {
            _render = GetComponent<MeshRenderer>();
            _initColor = _render.material.color;
        }

        private void OnMouseEnter()
        {
            if (!BuildManager.Instance.CanBuild) return;

            if (BuildManager.Instance.HasEnoughMoney)
            {
                _render.material.color = hoverColor;
            }
            else
            {
                _render.material.color = noEnoughMoneyColor;
            }
        }

        private void OnMouseExit()
        {
            if (!BuildManager.Instance.CanBuild) return;
            _render.material.color = _initColor;
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
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
            Instantiate(BuildManager.Instance.SelectedTurret.prefab, GetPosition(), Quaternion.identity);
        }

        private Vector3 GetPosition()
        {
            return transform.position + offset;
        }
    }
}
