using UnityEngine;


public class MrDoQuestManager : QuestManager
{
    [SerializeField] temp_PlayerDiamonds playerDiamonds;
    [SerializeField] DialogueManager dialogueManager;

    private bool isExpGained = false;


    private void OnEnable()
    {
        playerDiamonds.OnDiamondCollected += UpdateProgression;
        dialogueManager.OnFinishDialogue += CheckQuestProgression;

        QuestExp = 30;
        GoalNum = 4;
    }

    private void OnDisable()
    {
        playerDiamonds.OnDiamondCollected -= UpdateProgression;
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
