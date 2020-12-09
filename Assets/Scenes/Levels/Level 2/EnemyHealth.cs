using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float initHealth = 100f;
    private float _currentHealth;

    public Image hpBar;
    public GameObject selfObject;

    private AudioSource _audioSource;
    public AudioClip bulletClip;
    public AudioClip missileClip;
    public AudioClip laserClip;

    // Start is called before the first frame update
    private void Start()
    {
        _currentHealth = initHealth;
    }

    public void Damage(float amount, DamageType type)
    { 
        _currentHealth -= amount;
        hpBar.fillAmount = _currentHealth / initHealth;

        _audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>();
        }
        switch (type)
        {
            case DamageType.Bullet:
                _audioSource.PlayOneShot(bulletClip, 0.1f);
                break;
            case DamageType.Laser:
                if (!_audioSource.isPlaying)
                {
                    _audioSource.PlayOneShot(laserClip, 0.05f);
                }
                break;
            default:
                break;
        }

        if (_currentHealth <= 0)
        {
            Debug.Log("die");
            selfObject.GetComponent<Enemy>().Die();
        }
    }

    public enum DamageType
    {
        Bullet, Laser
    }
}
