using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    float timer = 2; 
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            gameObject.SetActive(false);
    }

    public void Reset()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * 5;
        timer = 2; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false); 
    }
}
