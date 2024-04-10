using TMPro;
using UnityEngine;

public class TutorialQuestManager : QuestManager
{
    [SerializeField] Health enemyHealth;
    [SerializeField] GameObject portal;


    private void OnEnable()
    {
        enemyHealth.OnDie += UpdateProgression;
        OnQuestDone += ActivatePortal;

        goalNum = 1;
    }

    private void OnDisable()
    {
        enemyHealth.OnDie -= UpdateProgression;
        OnQuestDone -= ActivatePortal;
    }

    public void ActivatePortal(int notUsedHere)
    {
        portal.SetActive(true);
    }
}
