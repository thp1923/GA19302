using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsShoot : MonoBehaviour
{
    
    public float lifeTime = 2f;
    readonly BoxCollider2D col;
    
    void Start()
    {
        
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
