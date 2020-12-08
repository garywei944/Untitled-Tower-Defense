using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace Sandbox.Gary
{
    public class Node : MonoBehaviour
    {
        public Vector3 offset = new Vector3(0, 0.5f, 0);

        public Color hoverColor = Color.gray;
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
            if (!BuildManager.Instance.SelectedTurret) return;
            _render.material.color = hoverColor;
        }

        private void OnMouseExit()
        {
            if (!BuildManager.Instance.SelectedTurret) return;
            _render.material.color = _initColor;
        }

        private void OnMouseDown()
        {
            if (!BuildManager.Instance.SelectedTurret) return;
            Instantiate(BuildManager.Instance.SelectedTurret, transform.position + offset, Quaternion.identity);
        }
    }
}
