using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoToAdd; 

    public int AddAmmo()
    {
        return ammoToAdd;
    }

    public void DestroyAmmo()
    {
        Destroy(gameObject);
    }
}
