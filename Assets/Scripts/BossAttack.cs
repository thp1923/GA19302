using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float lifeTime = 2f;
    BoxCollider2D col;
    Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        col = rig.GetComponent<BoxCollider2D>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
