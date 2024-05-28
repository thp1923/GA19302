using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsShoot : MonoBehaviour
{
    
    public float lifeTime = 2f;
    BoxCollider2D col;
    public float damge = 5f;
    
    void Start()
    {
        damge = 5f;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("EnemySlime"))
        {
            Destroy(gameObject);
        }
    }
    
    

    void Update()
    {
        
    }
}
