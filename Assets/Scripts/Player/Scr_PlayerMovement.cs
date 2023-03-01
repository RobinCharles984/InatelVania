using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scr_PlayerMovement : MonoBehaviour
{
    //Global Variables
    //Components variables
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] TrailRenderer tr;
    [SerializeField] ParticleSystem pt;
    [SerializeField] Animator anim;
    [SerializeField] Scr_GameController gameController;
    
    //Movement Variables
    [Header("Movement Variables")]
    public float horizontalSpeed;
    public float verticalSpeed;

    //Jump variavles
    [Header("Jumping variables")]
    public bool isGrounded;
    public bool inGround;
    public Transform groundCheck;
    public int jumpCount = 0;
    public float jumpColl = 0;
    
    //Transforming variables
    [Header("Transform Variables")]
    public bool lookRight = true;
    
    //Dashing variables
    [Header("Dashing Variables")] 
    public bool canDash;
    public bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingColldown;
    
    //Attacking Variables
    [Header("Attacking Variables")]
    public float attackSpeed;
    public float attackDurantion;
    public float attackMovement;
    public bool canAttack;
    public GameObject attackTrigger;
    
    //Damage Variables
    [Header("Damage Variables")]
    private SpriteRenderer sr;
    public Color hited;
    public Color noHited;
    public bool canTakeDamage;

    //Inputs
    [HideInInspector]public float h;
    [HideInInspector]public bool v;
    [HideInInspector]public float stomp;
    [HideInInspector]public bool dash;
    [HideInInspector]public float speedY;
    [HideInInspector]public bool attack;
    [HideInInspector]public bool thrown; 
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        //Configurating inputs
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetButtonDown("Jump");
        stomp = Input.GetAxis("Vertical");
        dash = Input.GetButtonDown("Dash");
        attack = Input.GetButtonDown("Fire1");
        thrown = Input.GetButtonDown("Fire2");
        speedY = rb.velocity.y;

        //Flipping by horizontal movement
        if (h < 0 && lookRight && canAttack)
            Flip();
        else if (h > 0 && !lookRight && canAttack)
            Flip();
        
        //Attacking Action
        if (attack && canAttack)
        {
            Attack();
        }

        //Stomp Action
        if (!isGrounded && stomp != 0)
        {
            rb.gravityScale += 0.2f;
            jumpCount = 1;
        }
        else if (isGrounded)
            rb.gravityScale = 1;

        if (Input.GetKey(KeyCode.R)) SceneManager.LoadScene(0);

        //Dashing action
        if (dash && canDash)
        {
            StartCoroutine(Dash());
        }

        //Jumping if collides ground and double jump
        if (v)
        {
            Jump();
        }
        CheckingGround();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(h * horizontalSpeed, rb.velocity.y);//Horizontal movement
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Damage" && canTakeDamage)
        {
            StartCoroutine(Damage());
        }
    }

    //My Functions//
    //Attack
    void Attack()
    {
        StartCoroutine(Attacking());
        Dust();
    }
    
    //Checking ground collision
    void CheckingGround()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.03f))
        {
            isGrounded = true;
            jumpCount = 0;
            jumpColl = Time.time + 0.2f;
            
        }
        else if (Time.time < jumpColl)
            isGrounded = true;
        else
            isGrounded = false;
    }
    
    //Jumping function
    void Jump()
    {
        if (isGrounded || jumpCount < 1)
        {
            Dust();
            rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
            jumpCount++;
        }
    }
    
    //Flip x scale
    void Flip()
    {
        if(isGrounded)
            Dust();    
        lookRight = !lookRight;
        float x = this.transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    //Dust FX
    void Dust()
    {
        pt.Play();
    }

    //Taking Damage
    IEnumerator Damage()
    {
        FindObjectOfType<Scr_PlayerLife>().life--;
        this.gameObject.layer = LayerMask.NameToLayer("Invincible");
        canTakeDamage = false;
        sr.color = hited;
        rb.AddForce(new Vector2(-10, 6), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        sr.color = noHited;
        for (int i = 0; i < 5; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        sr.color = Color.white;
        canTakeDamage = true;
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    
    //Attack
    IEnumerator Attacking()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDurantion);
        attackTrigger.SetActive(true);
        isGrounded = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
        attackTrigger.SetActive(false);
    }
    
    //Dash
    IEnumerator Dash()
    {
        Dust();
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        tr.emitting = true;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingColldown);
        canDash = true;
    }
}

