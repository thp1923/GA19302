using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boss : MonoBehaviour
{
    [SerializeField] float start, end, speed, speed2;
    public Transform FirePoint;
    public float FireRate = 0.5f;
    public GameObject bulletPrefabs;
    private float nextFireTime;
    
    Animator aim;
    bool isAlive = true;
    Rigidbody2D rig;
    int isRight = 1;
    GameObject player;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        aim = GetComponent<Animator>();

    }

    void Run()
    {

        var x_enemy = transform.position.x;
        var y_enemy = transform.position.y;

        
        
        if (x_enemy < start)
        {
            isRight = 1;
        }
        if (x_enemy > end)
        {
            isRight = -1;
        }
        transform.Translate(new Vector2(isRight * speed * Time.deltaTime, 0));

        
    }

    void Flip()
    {
        transform.localScale = new Vector2(isRight, 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= nextFireTime)
        {
            aim.SetBool("IsAttackBoss", true);
            Shoot();
            nextFireTime = Time.time + FireRate;
        }
        


    }
    // Update is called once per frame
    void Update()
    {
        Run();
        Flip();
        
    }
    void Shoot()
    {
        speed = 0;
        Instantiate(bulletPrefabs, FirePoint.position, FirePoint.rotation);
        //GameObject arrow = Instantiate(bulletPrefabs, FirePoint.position, Quaternion.identity);
        StartCoroutine(EndAnimation());
    }
    IEnumerator EndAnimation()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        aim.SetBool("IsAttackBoss", false);
        speed = speed2;
    }
}
