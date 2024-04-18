using UnityEngine;


public class OreoQuestManager : QuestManager
{
    [SerializeField] DialogueManager dialogueManager;

    private bool isExpGained = false;


    private void OnEnable()
    {
        dialogueManager.OnFinishDialogue += CheckQuestProgression;

        GoalNum = 4;
        QuestExp = 100;
    }

    private void OnDisable()
    {
        dialogueManager.OnFinishDialogue -= CheckQuestProgression;
    }

    private void CheckQuestProgression(bool isQuestDone)
    {
        if (isQuestDone && !isExpGained)
        {
            ExperienceManager.Singleton.AddExperience(QuestExp);
            isExpGained = true;
        }
    }
}
