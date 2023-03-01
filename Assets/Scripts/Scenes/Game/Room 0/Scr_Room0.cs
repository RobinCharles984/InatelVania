using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class Scr_Room0 : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject director;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        director.SetActive(false);
        timer = Time.time + 18;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= 3)
            director.SetActive(true);

        if (Time.time >= timer  - 3)
        {
            anim.SetBool("toFade", true);
        }
        
        if (Time.time >= timer)
            SceneManager.LoadScene(3);
    }
}
