using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Prefab_Shuriken : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float speed;

    public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject, destroyTime);
    }
}
