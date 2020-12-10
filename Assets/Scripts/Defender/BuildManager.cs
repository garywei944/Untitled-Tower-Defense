using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Sandbox.Gary
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance;
        public TurretDesign selectedTurret;
        public NodeUI nodeUI;

        private Node _selectedNode;

        public TurretDesign SelectedTurret
        {
            get => selectedTurret;
            set => selectedTurret = value;
        }

        public bool CanBuild => selectedTurret != null && selectedTurret.prefab != null;

        public bool HasEnoughMoney
        {
            get
            {
                Assert.IsNotNull(selectedTurret);
                return PlayerStatus.Money >= Instance.SelectedTurret.cost;
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        public void SelectNode(Node node)
        {
            if (node == _selectedNode)
            {
                Unselect();
                return;
            }

            selectedTurret = null;
            _selectedNode = node;
            nodeUI.SetTarget(_selectedNode);
            nodeUI.ShowUI();
        }

        public void Unselect()
        {
            _selectedNode = null;
            nodeUI.HideUI();
        }
    }
}
