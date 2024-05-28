using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKnight : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rig;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpspeed = 30f;
    [SerializeField] float climspeed = 15f;
    CapsuleCollider2D col;
    Animator aim;
    public BoxCollider2D feet;
    float stargravityscale;
    bool isAlive = true;
    public GameObject bulletPrefabs;

    public Transform FirePoint;
    public float FireRate = 0.5f;
    private float nextFireTime;
    bool isAttack = true;
    public float ArrowSpeed = 10f;
    [SerializeField] float speed2 = 10f;
    [SerializeField] float jumpspeed2 = 30f;
    [SerializeField] float climspeed2 = 15f;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        aim = GetComponent<Animator>();
        stargravityscale = rig.gravityScale;


    }



    void Die()
    {
        isAlive = false;

        aim.SetBool("IsDeath", true);
        Destroy(col);



    }

    void OnMove(InputValue value)
    {
        if (isAlive == false)
        {
            return;
        }
        moveInput = value.Get<Vector2>();

    }

    void OnJump(InputValue value)
    {
        if (isAlive == false)
        {
            return;
        }
        if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (value.isPressed)
        {
            rig.velocity += new Vector2(0f, jumpspeed);
        }
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            aim.SetBool("IsJumping", false);
        }
        else
        {
            aim.SetBool("IsJumping", true);
        }

    }

    void Update()
    {
        if (isAlive == false)
        {
            return;
        }
        Run();
        Flip();
        ClimbLander();
        if (Input.GetKey(KeyCode.J))
        {
            isAttack = true;
            speed = 1f;
            jumpspeed = 1f;
            climspeed = 1f;
        }
        else
        {
            isAttack = false;
            speed = speed2;
            jumpspeed = jumpspeed2;
            climspeed = climspeed2;
        }
        if (isAttack == true && Input.GetKey(KeyCode.J) && Time.time >= nextFireTime)
        {

            aim.SetBool("IsAttacking", true);

            if (Input.GetKey(KeyCode.J) && Time.time >= nextFireTime)
            {

                Shoot();
                nextFireTime = Time.time + FireRate;
            }

        }
        else
        {

            aim.SetBool("IsAttacking", false);
        }


    }

    void Run()
    {
        if (isAlive == false)
        {
            return;
        }
        if (isAttack == true)
        {
            aim.SetBool("IsAttacking", true);
        }
        rig.velocity = new Vector2(moveInput.x * speed, rig.velocity.y);

        bool havemove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;
        aim.SetBool("IsRunning", havemove);


        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            aim.SetBool("IsJumping", false);
        }
        else
        {
            aim.SetBool("IsJumping", true);
        }


    }

    void Flip()
    {
        if (isAlive == false)
        {
            return;
        }
        bool havemove = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;

        if (havemove)
        {

            transform.localScale = new Vector2(Mathf.Sign(rig.velocity.x), transform.localScale.y);

        }
        //RotatePlayer();
    }
    //void RotatePlayer()
    //{
    //    Vector2 firePointPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 look = firePointPosition - rig.position;
    //    float angle = Mathf.Atan2(look.x, look.y) * Mathf.Rad2Deg;
    //    rig.rotation = angle;
    //}

    void ClimbLander()
    {
        if (isAlive == false)
        {
            return;
        }
        if (!col.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rig.gravityScale = stargravityscale;
            if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                aim.SetBool("IsClimbing", false);
            }
            return;
        }

        rig.velocity = new Vector2(rig.velocity.x, moveInput.y * climspeed);


        if (col.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rig.gravityScale = 0;
            if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                aim.SetBool("IsJumping", false);
                aim.SetBool("IsClimbing", true);
            }

        }



    }

    void Shoot()
    {
        if (isAlive == false)
        {
            return;
        }
        if (isAttack == false && Time.time <= nextFireTime)
        {
            return;
        }
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            aim.SetBool("IsAttacking", true);
            GameObject arrow = Instantiate(bulletPrefabs, FirePoint.position, FirePoint.rotation);
            //GameObject arrow = Instantiate(bulletPrefabs, FirePoint.position, Quaternion.identity);
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            if (transform.localScale.x < 0)
            {

                rb.AddForce(transform.right * -ArrowSpeed, ForceMode2D.Impulse);
            }
            else if (transform.localScale.x > 0)
            {

                rb.AddForce(transform.right * ArrowSpeed, ForceMode2D.Impulse);
            }

        }
    }
}
