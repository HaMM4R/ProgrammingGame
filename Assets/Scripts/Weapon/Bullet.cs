using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosion; 

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * 5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "dTile")
        {
            collision.gameObject.GetComponent<TileDestroy>().Destroy();
        }

        Instantiate(explosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
