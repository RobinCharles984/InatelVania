using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Scr_DialogueHowToPlay : MonoBehaviour
{
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
        readyToSpeak = true;
    }

    private void Update()
    {
        if (!startDialogue && readyToSpeak)
        {
            float timer = 0;
            timer += Time.time;
            if(timer >= 2f) 
                StartDialogue();
            FindObjectOfType<Scr_PlayerMovement>().enabled = false;
            FindObjectOfType<Scr_PlayerAnimation>().enabled = false;
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
            dialogueIndex = 0;
            FindObjectOfType<Scr_PlayerMovement>().enabled = true;
            FindObjectOfType<Scr_PlayerAnimation>().enabled = true;
            FindObjectOfType<Scr_DialogueHowToPlay>().enabled = false;
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
        yield return new WaitForSeconds(0.3f);
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
}
