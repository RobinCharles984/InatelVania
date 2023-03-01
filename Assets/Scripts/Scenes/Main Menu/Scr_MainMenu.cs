using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Scr_MainMenu : MonoBehaviour
{
    private Data data = new Data();
    
    //Variables
    [SerializeField] AudioSource menuMusic;
    [SerializeField] GameObject system;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    private Scr_GameController gameController;
    private int dropDownValue;
    
    //Components
    [SerializeField] TMP_Dropdown  dropDown;
    [SerializeField] Toggle fullscreen;
    [SerializeField] Animator anim;
    [SerializeField] Animator animCamera;
    [SerializeField] Slider fxSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] List<AudioSource> fxAudio;
    [SerializeField] List<AudioSource> musicAudio;
    private FullScreenMode fullScreenMode;

    private void Start()
    {
        StartCoroutine(MenuMusic());
    }

    private void Update()
    {
        dropDownValue = dropDown.value;
        if (fullscreen.isOn == false)
        {
            fullScreenMode = FullScreenMode.Windowed;
        }
        else
        {
            fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        
        Debug.Log(Screen.currentResolution.ToString());
        Debug.Log(fullScreenMode);
        
        //DropDown
        switch (dropDownValue)
        {
            case 0:
                Screen.SetResolution(800, 600, fullScreenMode);
                break;
            
            case 1:
                Screen.SetResolution(1024, 768, fullScreenMode);
                break;
            
            case 2: 
                Screen.SetResolution(1280, 600, fullScreenMode);
                break;
            
            case 3: 
                Screen.SetResolution(1280, 720, fullScreenMode);
                break;
            
            case 4: 
                Screen.SetResolution(1280, 768, fullScreenMode);
                break;
            
            case 5: 
                Screen.SetResolution(1280, 800, fullScreenMode);
                break;
            
            case 6: 
                Screen.SetResolution(1280, 960, fullScreenMode);
                break;
            
            case 7: 
                Screen.SetResolution(1280, 1024, fullScreenMode);
                break;
            
            case 8: 
                Screen.SetResolution(1360, 768, fullScreenMode);
                break;
            
            case 9: 
                Screen.SetResolution(1366, 768, fullScreenMode);
                break;
            
            case 10: 
                Screen.SetResolution(1400, 1050, fullScreenMode);
                break;
            
            case 11: 
                Screen.SetResolution(1440, 900, fullScreenMode);
                break;
            
            case 12: 
                Screen.SetResolution(1600, 900, fullScreenMode);
                break;
            
            case 13: 
                Screen.SetResolution(1680, 1050, fullScreenMode);
                break;
            
            case 14: 
                Screen.SetResolution(1920, 1080, fullScreenMode);
                break;
        }
        
        //Music Slider
        for (int i = 0; i < musicAudio.Count; i++)
        {
            musicAudio[i].volume = musicSlider.value;
        }

        //SoundFX Slider
        for (int i = 0; i < fxAudio.Count; i++)
        {
            fxAudio[i].volume = fxSlider.value;
        }
        
    }

    public void NewGame()
    {
        StartCoroutine(Play());
        anim.SetBool("toPlay", true);
        animCamera.SetBool("toPlay", true);
    }

    public void LoadGame()
    {
        StartCoroutine(Load());
        anim.SetBool("toPlay", true);
        animCamera.SetBool("toPlay", true);
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
        anim.SetBool("toOptions", true);
    }

    public void Back()
    {
        anim.SetBool("toOptions", false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator MenuMusic()
    {
        yield return new WaitForSeconds(2.4f);
        menuMusic.Play();
    }
    
    IEnumerator Play()//For new game
    {
        yield return new WaitForSeconds(4.2f);
        SceneManager.LoadScene(2);
    }

    IEnumerator Load()//For load game
    {
        yield return new WaitForSeconds(4.2f);
        data.Load();
    }
}
