using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject dialogueWindow;
    [SerializeField] InputManager inputManager;

    [SerializeField] Text nameText; // Name whose the one talking
    [SerializeField] Text dialogueText;

    [SerializeField] DialogueData questDialogue;
    [SerializeField] DialogueData questInProcessDialogue;

    private DialogueData currDialogue;
    private int textIndex = 0;


    private void Start()
    {
        currDialogue = questDialogue;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextDialogue();
        }
    }

    public void StartDialogue()
    {
        GameManager.Singleton.StopGame();

        textIndex = 0;
        dialogueWindow.SetActive(true);

        NextDialogue();
    }

    public void NextDialogue()
    {
        // If it went through all the texts, fininsh the dialogue
        if (textIndex >= currDialogue.dialoguePhrase.Length)
        {
            FinishDialogue();
            return;
        }

        nameText.text = currDialogue.characterName;
        dialogueText.text = currDialogue.dialoguePhrase[textIndex];

        textIndex++;
    }

    public void FinishDialogue()
    {
        dialogueWindow.SetActive(false);

        // From now, NPC will read questInProcessDialogue.
        currDialogue = questInProcessDialogue;

        GameManager.Singleton.ResumeGame();
    }
}
