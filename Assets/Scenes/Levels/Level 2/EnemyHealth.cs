using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float initHealth = 100f;
    private float _currentHealth;

    public Image hpBar;
    public GameObject selfObject;

    // Start is called before the first frame update
    private void Start()
    {
        _currentHealth = initHealth;
    }

    public void Damage(float amount)
    { 
        //TODO 
        _currentHealth -= amount;
        hpBar.fillAmount = _currentHealth / initHealth;
        if (_currentHealth <= 0)
        {
            Debug.Log("die");
            selfObject.GetComponent<Enemy>().Die();
        }
    }
}
