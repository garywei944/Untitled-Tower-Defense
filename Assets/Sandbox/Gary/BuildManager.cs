using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sandbox.Gary
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance;
        public TurretDesign selectedTurret;

        public TurretDesign SelectedTurret
        {
            get => selectedTurret;
            set => selectedTurret = value;
        }

        public bool CanBuild => selectedTurret.prefab != null;
        public bool HasEnoughMoney => PlayerStatus.Money >= BuildManager.Instance.SelectedTurret.cost;

        private void Awake()
        {
            Instance = this;
        }
    }
}
