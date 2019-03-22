using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Test");
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerShoot>().Ammo += 5; 
        }
    }
}
