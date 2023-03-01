using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Scr_DialogueBoss : MonoBehaviour
{
    private Camera cam;
    public Animator camAnim;
    public Collider2D trigger;
    public GameObject spawner;
    public GameObject portal;

    public GameObject sceneCollider;
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
    private void Start()
    {
        dialoguePanel.SetActive(false);
        startDialogue = false;
        cam = Camera.main;
        camAnim.enabled = false;
        sceneCollider.SetActive(false);
        spawner.SetActive(false);
        portal.SetActive(false);
        GameController.music.Play();
    }

    private void Update()
    {
        if (!startDialogue && readyToSpeak)
        {
            GameController.music.Stop();
            float timer = 0;
            timer += Time.time;
            if (timer >= 2f)
            {
                StartDialogue();
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
            GameController.game1bad.Play();
            spawner.SetActive(true);
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
        yield return new WaitForSeconds(1f);
        GameController.game1bad.Play();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //StartCoroutine(PreguicaDialogue());
        trigger.enabled = false;
        sceneCollider.SetActive(true);
        camAnim.enabled = true;
        readyToSpeak = true;
    }
}
