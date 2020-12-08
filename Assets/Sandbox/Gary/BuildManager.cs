using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sandbox.Gary
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance;
        public GameObject turretPrefab;

        public GameObject SelectedTurret
        {
            get => turretPrefab;
            set => turretPrefab = value;
        }

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
