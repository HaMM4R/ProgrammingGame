using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject fireLocation;
    PlayerController playerController; 

    int ammo;
    float timer; 

    public int Ammo { get { return ammo; } set { ammo = value; }  }

    void Start()
    {
        ammo = 0;
        playerController = GetComponent<PlayerController>(); 
    }
    
    void Update()
    {
        FireBuffer(); 
    }

    public void Fire()
    {
        if(ammo > 0)
        {
            Instantiate(bullet, fireLocation.transform.position, fireLocation.transform.rotation);
            ammo--;
            timer = 2; 
        }
    }
    
    void FireBuffer()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
    }
}
