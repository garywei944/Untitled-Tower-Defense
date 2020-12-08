using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sandbox.Gary
{
    public class EnemyHealth : MonoBehaviour
    {
        public float initHealth = 100f;
        private float _currentHealth;

        public Image hpBar;

        // Start is called before the first frame update
        private void Start()
        {
            _currentHealth = initHealth;
        }

        public void Damage(float amount)
        {
            _currentHealth -= amount;
            hpBar.fillAmount = _currentHealth / initHealth;
            if (_currentHealth <= 0)
            {
                EnemySpawner.EnemyAlive--;
                Destroy(gameObject);
            }
        }
    }
}
