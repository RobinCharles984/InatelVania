using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Scr_GroundEnemy_Movement : MonoBehaviour
{
    //Components
    [Header("Components")] 
    [SerializeField] Rigidbody2D rb;
    
    //Variables
    [Header("Movement Variables")] 
    public float speed;
    public float h;

    [Header("Player Variables")] 
    public bool foundPlayer;
    public Vector2 foundSize;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        h = transform.localScale.x;
        rb.velocity = new Vector2(h * speed, rb.velocity.y);
        
        //Found Player
        if (foundPlayer)
        {
            Debug.Log("Founded");
        }
        
    }

    void FixedUpdate()
    {
        foundPlayer = Physics2D.OverlapBox(gameObject.transform.position, foundSize, 0f, 3);
    }
    
    void Flip()
    {
        float x = this.transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Wall"))
        {
            Flip();
        }
    }
}
