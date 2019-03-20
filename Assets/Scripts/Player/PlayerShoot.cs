using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject fireLocation; 


    void Start()
    {
    }
    
    void Update()
    {

    }

    public void Fire()
    {
        Instantiate(bullet, fireLocation.transform.position, fireLocation.transform.rotation);
    }
}
