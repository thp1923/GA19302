using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [SerializeField] float start, end, speed, speed2;
    public Transform FirePoint;
    public float FireRate = 0.5f;
    public GameObject bulletPrefabs;
    private float nextFireTime;
    public int BossLive = 100;
    Animator aim;
    bool isAlive = true;
    Rigidbody2D rig;
    int isRight = 1;
    GameObject player;
    CapsuleCollider2D cp;
    public Slider liveSlider;
    public GameObject BloodEffect;
    AudioManager audioManager;
    public Transform Portal;
    public GameObject Win;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        aim = GetComponent<Animator>();
        cp = GetComponent<CapsuleCollider2D>();
        
    }
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Run()
    {
        if (isAlive == false)
        {
            return;
        }
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
        var x_player = player.transform.position.x;
        if (x_player > start && x_player < end)
        {
            if (x_enemy < x_player)
            {
                isRight = 1;
            }
            if (x_enemy > x_player)
            {
                isRight = -1;
            }
        }
    }

    void Flip()
    {
        if (isAlive == false)
        {
            return;
        }
        transform.localScale = new Vector2(isRight, 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAlive == false)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player") && Time.time >= nextFireTime)
        {
            aim.SetBool("IsAttackBoss", true);
            Shoot();
            nextFireTime = Time.time + FireRate;
        }
        else if (collision.gameObject.CompareTag("Arrows"))
        {
            audioManager.PlaySFX(audioManager.DevilDamage);
            BossLive = BossLive - 5;
            liveSlider.value = BossLive;
            GameObject Effect = Instantiate(BloodEffect, transform.position, transform.localRotation);

            Destroy(Effect, 2);
            aim.SetBool("IsHitBoss", true);
            speed = 0;
            StartCoroutine(EndAnimation());
            if (BossLive <= 0)
            {
                BossDie();
            }
        }


    }

    void BossDie()
    {
        aim.SetBool("IsDeathBoss", true);
        isAlive = false;
        Destroy(cp);
        FindObjectOfType<GameSession>().AddScore(1000);
        Instantiate(Win, Portal.position, Portal.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        if (isAlive == false)
        {
            return;
        }
        Run();
        Flip();
    }
    void Shoot()
    {
        if (isAlive == false)
        {
            return;
        }
        speed = 0;
        Instantiate(bulletPrefabs, FirePoint.position, FirePoint.rotation);
        //GameObject arrow = Instantiate(bulletPrefabs, FirePoint.position, Quaternion.identity);
        StartCoroutine(EndAnimation());
    }
    IEnumerator EndAnimation()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        aim.SetBool("IsAttackBoss", false);
        aim.SetBool("IsHitBoss", false);
        speed = speed2;
    }
}
