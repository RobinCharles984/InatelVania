using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Scr_DialogueRoom : MonoBehaviour
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
        dialoguePanel.SetActive(true);
        startDialogue = false;
        readyToSpeak = true;
    }

    private void Update()
    {
        if (!startDialogue && readyToSpeak)
        {
            float timer = 0;
            timer += Time.time;
            if(timer >= 2f) StartDialogue();
            FindObjectOfType<Scr_Room0>().enabled = false;
            director.SetActive(false);
            fade.enabled = false;
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
            FindObjectOfType<Scr_Room0>().enabled = true;
            director.SetActive(true);
            fade.enabled = true;
            GameController.music.Play();
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
}
