using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Scr_DialoguePreguica : MonoBehaviour
{
    private Camera cam;
    public SpriteRenderer spr;
    public Color c1;
    public Color c2;
    public Animator fadeAnim;
    
    public Animator preguicaAnim;
    public string[] dialogueNpc;
    public string nameText;
    public int dialogueIndex;

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    public TMP_Text nameNpc;

    public bool readyToSpeak;
    public bool startDialogue;
    public Scr_GameController GameController;
    public GameObject director;
    public Animator fade;
    private void Start()
    {
        dialoguePanel.SetActive(false);
        startDialogue = false;
        cam = Camera.current;
        preguicaAnim.enabled = false;
    }

    private void Update()
    {
        if (!startDialogue && readyToSpeak)
        {
            float timer = 0;
            timer += Time.time;
            if (timer >= 2f)
            {
                StartDialogue();
                StartCoroutine(PreguicaDialogue());
            }
            preguicaAnim.enabled = true;
            FindObjectOfType<Scr_PlayerAnimation>().anim.SetBool("hMovement", false);
            FindObjectOfType<Scr_PlayerMovement>().rb.bodyType = RigidbodyType2D.Static;
            FindObjectOfType<Scr_PlayerMovement>().enabled = false;
            FindObjectOfType<Scr_PlayerAnimation>().enabled = false;
            FindObjectOfType<Scr_DialogueHowToPlay>().enabled = false;
        }
        else if (nameNpc.text == dialogueNpc[dialogueIndex])
        {
            if(Input.GetButtonDown("Interact"))
                NextDialogue();
        }
    }

    void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueNpc.Length)
        {
            StartCoroutine(ShowDialogue());
        }
        else
        {
            readyToSpeak = false;
            dialoguePanel.SetActive(false);
            startDialogue = false;
            preguicaAnim.SetBool("Away", true);
            FindObjectOfType<Scr_PlayerMovement>().enabled = true;
            FindObjectOfType<Scr_PlayerAnimation>().enabled = true;
            FindObjectOfType<Scr_DialoguePreguica>().enabled = false;
            FindObjectOfType<Scr_PlayerMovement>().rb.bodyType = RigidbodyType2D.Dynamic;
            Destroy(gameObject);
        }
    }
    
    void StartDialogue()
    {
        dialogueText.text = nameText;
        startDialogue = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(ShowDialogue());
    }

    IEnumerator ShowDialogue()
    {
        nameNpc.text = "";
        foreach (char letter in dialogueNpc[dialogueIndex])
        {
            nameNpc.text += letter;
            //GameController.playSFX(GameController.voice);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator PreguicaDialogue()
    {
        GameController.music.Stop();
        GameController.storm.Play();
        fadeAnim.enabled = false;
        for (int i = 0; i < 7; i++)
        {
            cam.backgroundColor = Color.white;
            spr.color = c1;
            yield return new WaitForSeconds(0.2f);
            spr.color = c2;
            cam.backgroundColor = Color.yellow;
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1f);
        GameController.storm.Pause();
        GameController.game1bad.Play();
        fadeAnim.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        readyToSpeak = true;
    }
}
