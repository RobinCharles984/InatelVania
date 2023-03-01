using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Counter : MonoBehaviour
{
    public float count;

    public Animator camAnim;

    public GameObject portal;

    public Scr_GameController GameController;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (count >= 86)
        {
            GameController.game1bad.Stop();
            camAnim.SetBool("Zoom", true);
            portal.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Damage")
            count++;
    }
}
