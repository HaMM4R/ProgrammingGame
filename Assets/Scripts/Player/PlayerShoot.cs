using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject fireLocation; 

    int ammo; 

    public int Ammo { get { return ammo; } set { ammo = value; }  }

    void Start()
    {
        ammo = 10; 
    }
    
    void Update()
    {

    }

    public void Fire()
    {
        if(ammo > 0)
        {
            Instantiate(bullet, fireLocation.transform.position, fireLocation.transform.rotation);
            ammo--; 
        }
    }
    
    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 4 + 15, 10, 100,22), "Ammo: " + ammo.ToString());
    }
}
