using System.Collections;
using UnityEngine;


public class ControlManualManager : MonoBehaviour
{
    [SerializeField] GameObject combatManual;
    [SerializeField] GameObject generalManual;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Health enemyHealth;

    private float generalManualShownTime = 20f;


    private void OnEnable()
    {
        dialogueManager.OnFinishDialogue += TurnOnManual;
        enemyHealth.OnDie += TurnOffCombatManual;
    }

    private void OnDisable()
    {
        dialogueManager.OnFinishDialogue -= TurnOnManual;
        enemyHealth.OnDie -= TurnOffCombatManual;
    }

    public void TurnOnManual(bool isQuestDone)
    {
        // If the quest is already done, do not turn on controlManual again.
        combatManual.SetActive(!isQuestDone);
    }

    public void TurnOffCombatManual()
    {
        combatManual.SetActive(false);

        StartCoroutine(ShowGeneralManual());
    }

    IEnumerator ShowGeneralManual()
    {
        generalManual.SetActive(true);

        yield return new WaitForSeconds(generalManualShownTime);

        generalManual.SetActive(false);
    }
}
