using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float speed2 = 5f;
    public Transform start;
    public Transform end;
    Rigidbody2D rig;
    int isRight = 1;
    GameObject player;
    private Vector2 now;
    public float health = 10f;
    CapsuleCollider2D col;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        now = start.position;
    }

    void Run()
    {

        var x_player = player.transform.position;
        if (Vector2.Distance(transform.position, start.position) < 0.1f)
        {
            now = end.position;
            isRight = -1;
        }
        if(Vector2.Distance(transform.position, end.position) < 0.1f)
        {
            now = start.position;
            isRight = 1;
        }

        transform.position = Vector2.MoveTowards(transform.position, now, speed * Time.deltaTime);

        
    }

    void Flip()
    {
        transform.localScale = new Vector2(isRight, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrows"))
        {
            health = health - 3f;
            
            if(health <= 0)
            {
                Die();
            }
        }

    }

    
    void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Flip();
        
    }
}

