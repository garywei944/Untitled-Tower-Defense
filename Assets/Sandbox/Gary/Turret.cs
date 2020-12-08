using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range = 5.0f;
    public string enemyTag = "Enemy";

    [HideInInspector] public Transform target;

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0, 0.5f);
    }


    // Update is called once per frame
    private void Update()
    {
        if (!target)
            return;
        LockTarget();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void UpdateTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        var minDistance = Mathf.Infinity;
        Transform nearestEnemy = null;
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        if (minDistance < range)
        {
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }
    }

    private void LockTarget()
    {
        var dir = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
    }
}
