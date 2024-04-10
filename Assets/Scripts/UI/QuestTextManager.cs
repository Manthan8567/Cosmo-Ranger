using UnityEngine;
using TMPro;

public class QuestTextManager : MonoBehaviour
{
    [SerializeField] QuestManager questManager;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] TextMeshProUGUI questProgressionText;
    [SerializeField] string questTextBase = "- what to do";


    private void OnEnable()
    {
        questManager.OnUpdateProgression += UpdateQuestText;
        questManager.OnQuestDone += UpdateQuestText;

        dialogueManager.OnFinishDialogue += ToggleQuestProgressionText;
    }

    private void OnDisable()
    {
        questManager.OnUpdateProgression -= UpdateQuestText;
        questManager.OnQuestDone -= UpdateQuestText;

        dialogueManager.OnFinishDialogue -= ToggleQuestProgressionText;
    }

    public void ToggleQuestProgressionText(bool isQuestDone)
    {
        questProgressionText.enabled = !isQuestDone;
    }

    public void UpdateQuestText(int currProgression)
    {
        questProgressionText.text = questTextBase + $"({currProgression}/{questManager.goalNum})";
    }
}
