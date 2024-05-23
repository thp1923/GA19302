using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsShoot : MonoBehaviour
{
    
    public float lifeTime = 2f;
    
    void Start()
    {
        
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        
    }
}
