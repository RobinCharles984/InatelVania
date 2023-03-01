using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_Portals : MonoBehaviour
{
    //Components
    [SerializeField] GameObject fade;
    [SerializeField] Animator anim;
    
    //Variables
    public float speed;
    public int sceneNumber;
    
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(Fade());
        FindObjectOfType<Scr_PlayerMovement>().enabled = false;
        FindObjectOfType<Scr_PlayerAnimation>().enabled = false;
    }

    IEnumerator Fade()
    {
        anim.SetBool("toFade", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneNumber);
    }
}
