using System;
using UnityEngine;

namespace Sandbox.Gary
{
    [Serializable]
    public class TurretDesign
    {
        public GameObject prefab;
        public int cost;
        public int upgradeCost;
        public int SellPrice => cost / 2;
        public GameObject upgradedPrefab;
    }
}
