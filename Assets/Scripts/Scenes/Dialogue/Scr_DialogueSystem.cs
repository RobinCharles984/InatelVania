using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class Scr_DialogueSystem : MonoBehaviour
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

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && readyToSpeak)
        {
            if (!startDialogue)
            {
                FindObjectOfType<Scr_PlayerMovement>().enabled = false;
                FindObjectOfType<Scr_PlayerAnimation>().enabled = false;
                StartDialogue();
            }
            else if (nameNpc.text == dialogueNpc[dialogueIndex])
            {
                NextDialogue();
            }
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
            dialoguePanel.SetActive(false);
            startDialogue = false;
            dialogueIndex = 0;
            FindObjectOfType<Scr_PlayerMovement>().enabled = true;
            FindObjectOfType<Scr_PlayerAnimation>().enabled = true;
        }
    }
    
    void StartDialogue()
    {
        dialogueText.text = nameText;
        startDialogue = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
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
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            readyToSpeak = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            readyToSpeak = false;
        }
    }
}
