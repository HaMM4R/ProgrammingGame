using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet; 


    void Start()
    {
        
    }
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject fireBullet = Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}
