using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_GameController : MonoBehaviour
{
    //Components
    public Transform playerTransform;
    private Camera cam;
    
    //Camera Controller
    public Transform camLeft, camRight, camTop, camDown;
    public float speedCam;
    
    //Audios
    public AudioSource music;
    public AudioSource sfx;
    
    //Musics
    [Header("Musics")]
    public AudioSource menu;
    public AudioSource room;
    public AudioSource game1;
    public AudioSource game1bad;
    public AudioSource game2;
    public AudioSource game3;
    public AudioSource boss;
    public AudioSource ending;
    
    //SFX
    [Header("SFX")]
    public AudioSource blip;
    public AudioSource click;
    public AudioSource jump;
    public AudioSource coin;
    public AudioSource attack;
    public AudioSource hit;
    public AudioSource storm;
    [HideInInspector]
    public AudioClip blipClip;
    public AudioClip clickClip;
    public AudioClip jumpClip;
    public AudioClip coinClip;
    public AudioClip attackClip;
    public AudioClip hitClip;
    public AudioClip stormClip;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        blipClip = blip.clip;
        clickClip = click.clip;
        jumpClip = jump.clip;
        coinClip = coin.clip;
        attackClip = attack.clip;
        hitClip = hit.clip;
        stormClip = storm.clip;
    }

    // Update is called once per frame
    void Update()
    { 
        Scene scene = SceneManager.GetActiveScene();
        if (Input.GetButtonDown("Reset"))
        {
            SceneManager.LoadScene(scene.name);
        }
    }

    private void LateUpdate()
    {
        float posX = playerTransform.position.x;
        float posY = playerTransform.position.y;

        if (cam.transform.position.x < camLeft.position.x && playerTransform.position.x < camLeft.position.x)
        {
            posX = camLeft.position.x;
        }
        else if (cam.transform.position.x > camRight.position.x && playerTransform.position.x > camRight.position.x)
        {
            posX = camRight.position.x;
        }
        
        if (cam.transform.position.y < camDown.position.y && playerTransform.position.y < camDown.position.y)
        {
            posY = camDown.position.y;
        }
        else if (cam.transform.position.y > camTop.position.y && playerTransform.position.y > camTop.position.y)
        {
            posY = camTop.position.y;
        }
        
        Vector3 posCam = new Vector3(posX, posY, cam.transform.position.z);
        cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, speedCam * Time.deltaTime);
    }

    public void playSFX(AudioClip sfxClip, float volume)
    {
        sfx.PlayOneShot(sfxClip, volume);
    }
}
