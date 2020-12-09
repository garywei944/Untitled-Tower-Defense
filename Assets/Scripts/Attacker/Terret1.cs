using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terret1 : MonoBehaviour
{
    public float shootInterval = 10f;
    public float bulletInterval = 5f;
    public float bulletNum = 1;
    private float countDown;

    public Transform shootPoint;
    public Transform turretPoint;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        //countDown = shootInterval;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if(countDown <= 0)
        {
            Shoot();
            countDown = shootInterval;
        }
    }

    void Shoot()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for(int i = 0; i < bulletNum; i++)
        {
            GameObject newBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            Vector3 shootDirection = shootPoint.position - turretPoint.position; //TODO: shoot a bullet
            shootDirection.y = 0.0f;
            shootDirection.Normalize();
            newBullet.GetComponent<Bullet1>().direction = shootDirection;
            newBullet.GetComponent<Bullet1>().birthTime = Time.time;
            yield return new WaitForSeconds(bulletInterval);
        }
    }
}
