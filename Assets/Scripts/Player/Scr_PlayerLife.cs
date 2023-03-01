using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scr_PlayerLife : MonoBehaviour
{
    public SpriteRenderer spr;
    public SpriteRenderer playerSpr;
    public int life;
    public Sprite[] sprites = new Sprite[5];
    public GameObject text;

    public GameObject player;

    public ParticleSystem dust;
    public ParticleSystem dust1;
    public ParticleSystem dust2;
    public ParticleSystem dust3;
    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        LessLife();
        
        switch (life)
        {
            case (6):
                spr.sprite = sprites[5];
                break;
            case (5):
                spr.sprite = sprites[4];
                break;
            case (4):
                spr.sprite = sprites[3];
                break;
            case (3):
                spr.sprite = sprites[2];
                break;
            case (2):
                spr.sprite = sprites[1];
                break;
            case (1):
                spr.sprite = sprites[0];
                break;
            case (0):
                spr.sprite = null;
                playerSpr.enabled = false;
                text.SetActive(true);
                FindObjectOfType<Scr_PlayerMovement>().rb.bodyType = RigidbodyType2D.Static;
                StartCoroutine(Dust());
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Limiter")
            life = 0;
        if (col.gameObject.tag == "Life")
        {
            life = 5;
        }
    }

    void LessLife()
    {
        float actualLife = life;
        if (life < actualLife)
            StartCoroutine(Blinding());
    }

    IEnumerator Blinding()
    {
        for (int i = 0; i < 5; i++)
        {
            spr.sprite = null;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Dust()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                dust.Play();
                dust1.Play();
                dust2.Play();
                dust3.Play();
                yield return new WaitForSeconds(0.05f);
            }
            Destroy(player);
        }
    }
}
