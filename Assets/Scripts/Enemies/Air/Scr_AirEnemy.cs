using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AirEnemy : MonoBehaviour
{
    public GameObject life;
    public Transform lifeSpawn;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < player.position.x)
            transform.localScale = new Vector3( 1, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3( -1, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Attack")
        {
            Instantiate(life, lifeSpawn.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
