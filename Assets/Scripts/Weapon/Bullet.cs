using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float timer; 
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * 5;
        timer = 2; 
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "dTile")
        {
            collision.gameObject.GetComponent<TileDestroy>().Destroy();
        }
        
        Destroy(gameObject);
    }
}
