using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2 : MonoBehaviour
{
    public float range;
    public float rotSpeed;
    private const string EnemyTag = "Enemy";
    [HideInInspector] public Transform target;

    public Transform _bulletPoint;
    private float _countDown;

    public LineRenderer lineRender;
    public float overTimeDmg = 30;
    public float slowPct = 0.3f;
    private EnemyHealth _enemyHp;
    public ParticleSystem impactEffect;
    public Light pointLight;

    // Start is called before the first frame update
    void Start()
    {
        _bulletPoint = gameObject.transform.Find("ShootPoint");
        InvokeRepeating(nameof(UpdateTarget), 0, 0.5f);
        //_countDown = 1 / bulletRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            lineRender.enabled = false;
            impactEffect.Stop();
            pointLight.enabled = false;
            return;
        }

        LockTarget();

        Laser();
    }

    private void UpdateTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
        var minDistance = Mathf.Infinity;
        Transform nearestEnemy = null;
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (!(distance < minDistance)) continue;
            minDistance = distance;
            nearestEnemy = enemy.transform;
        }

        if (minDistance < range)
        {
            target = nearestEnemy;
            _enemyHp = target.GetComponent<EnemyHealth>();
            //_enemyMove = target.GetComponent<EnemyAI>();
        }
        else
        {
            target = null;
        }
    }

    private void Laser()
    {
        if (!lineRender.enabled)
        {
            lineRender.enabled = true;
            impactEffect.Play();
            pointLight.enabled = true;
        }

        // Damage enemy
        _enemyHp.Damage(overTimeDmg * Time.deltaTime);

        // Slow Enemy
        //_enemyMove.Slow(slowPct);

        // Draw the laser line
        var bulletPosition = _bulletPoint.position;
        var targetPosition = target.position;
        targetPosition.y += 1;
        lineRender.SetPosition(0, bulletPosition);
        lineRender.SetPosition(1, targetPosition);

        // Change Effect position and rotation 
        var dir = bulletPosition - targetPosition;
        impactEffect.transform.transform.position = targetPosition + dir.normalized * 1;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir.normalized);
    }

    private void LockTarget()
    {
        var dir = target.position - transform.position;
        var rotation = Quaternion.LookRotation(dir);
        var lerpRot = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);
        transform.rotation = Quaternion.Euler(new Vector3(0, lerpRot.eulerAngles.y, 0));
    }
}
