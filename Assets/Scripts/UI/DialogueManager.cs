using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueWindow;
    [SerializeField] QuestManager questManager;

    [SerializeField] Text nameText; // Name whose the one talking
    [SerializeField] Text dialogueText;

    [SerializeField] DialogueData questDialogue;
    [SerializeField] DialogueData questInProcessDialogue;
    [SerializeField] DialogueData questDoneDialogue;

    public event Action<bool> OnFinishDialogue;

    private DialogueData currDialogue;
    private int textIndex = 0;
    private bool isTalking = false;


    private void OnEnable()
    {
        questManager.OnQuestDone += SetQuestDoneDialogue;
        currDialogue = questDialogue;
    }

    private void OnDisable()
    {
        questManager.OnQuestDone -= SetQuestDoneDialogue;
    }

    private void Update()
    {
        if (isTalking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                NextDialogue();
            }
        }      
    }

    public void StartDialogue()
    {
        GameManager.Singleton.StopGame();
        isTalking = true;

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

    public void SetQuestDoneDialogue(int currProgression)
    {
        currDialogue = questDoneDialogue;
    }

    public void FinishDialogue()
    {
        OnFinishDialogue?.Invoke(questManager.isQuestDone);

        dialogueWindow.SetActive(false);

        if (!questManager.isQuestDone)
        {
            // From now, NPC will read questInProcessDialogue.
            currDialogue = questInProcessDialogue;
        }

        GameManager.Singleton.ResumeGame();

        isTalking = false;
    }
}
