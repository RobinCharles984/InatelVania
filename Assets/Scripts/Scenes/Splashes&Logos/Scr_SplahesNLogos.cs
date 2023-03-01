using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_SplahesNLogos : MonoBehaviour
{
    public float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(1);
    }
}
