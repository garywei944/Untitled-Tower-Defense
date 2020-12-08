using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sandbox.Gary
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance;
        public GameObject selectedTurret;

        public GameObject SelectedTurret
        {
            get => selectedTurret;
            set => selectedTurret = value;
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}
