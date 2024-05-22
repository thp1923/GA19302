using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rig;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpspeed = 20f;
    [SerializeField] float climspeed = 10f;
    CapsuleCollider2D col;
    Animator aim;
    public BoxCollider2D feet;
    float stargravityscale;
    bool isAlive = true;
    




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
    }


    void Run()
    {
        if (isAlive == false)
        {
            return;
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
    }

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
}
