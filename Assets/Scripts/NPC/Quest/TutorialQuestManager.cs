using UnityEngine;


public class TutorialQuestManager : QuestManager
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Health enemyHealth;
    [SerializeField] GameObject portal;

    private bool isExpGained = false;


    private void OnEnable()
    {
        enemyHealth.OnDie += UpdateProgression;
        dialogueManager.OnFinishDialogue += CheckQuestProgression;

        GoalNum = 1;
        QuestExp = 10;
    }

    private void OnDisable()
    {
        enemyHealth.OnDie -= UpdateProgression;
        dialogueManager.OnFinishDialogue -= CheckQuestProgression;
    }

    private void CheckQuestProgression(bool isQuestDone)
    {
        if (isQuestDone && !isExpGained)
        {
            ExperienceManager.Singleton.AddExperience(QuestExp);
            isExpGained = true;
            portal.SetActive(true);
        }
    }
}
