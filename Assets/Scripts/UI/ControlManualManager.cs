using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManualManager : MonoBehaviour
{
    [SerializeField] GameObject controlManual;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Health enemyHealth;


    private void OnEnable()
    {
        dialogueManager.OnFinishDialogue += TurnOnManual;
        enemyHealth.OnDie += TurnOffManual;
    }

    private void OnDisable()
    {
        dialogueManager.OnFinishDialogue -= TurnOnManual;
        enemyHealth.OnDie -= TurnOffManual;
    }

    public void TurnOnManual(bool isQuestDone)
    {
        // If the quest is already done, do not turn on controlManual again.
        controlManual.SetActive(!isQuestDone);
    }

    public void TurnOffManual()
    {
        controlManual.SetActive(false);
    }
}
