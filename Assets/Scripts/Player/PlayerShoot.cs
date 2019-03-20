using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    List<GameObject> bullets = new List<GameObject>();

    void Start()
    {
        SpawnBullets(20);
    }
    
    void Update()
    {
        Fire(); 
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    bullets[i].SetActive(true);
                    bullets[i].transform.position = transform.position;
                    bullets[i].transform.rotation = transform.rotation;
                    bullets[i].GetComponent<Bullet>().Reset();
                    break;
                }
            }
        }
    }

    void SpawnBullets(int numToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            GameObject bulletHolder = Instantiate(bullet, transform.position, transform.rotation);
            bulletHolder.SetActive(false);
            bullets.Add(bulletHolder);
        }
    }
}
