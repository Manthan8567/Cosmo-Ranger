using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class CinematicTrigger : MonoBehaviour
{
    [SerializeField] private CharacterController playerCharacterController;

    private PlayableDirector playableDirector;

    private bool isPlayed = false;
    private float cinematicPlayTime;


    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();

        cinematicPlayTime = (float)playableDirector.duration;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }

        if (isPlayed == false)
        {
            // Player cannot move while playing cinematics
            playerCharacterController.enabled = false;

            playableDirector.Play();
            isPlayed = true;

            StartCoroutine(WaitForCinematicEnd());
        }
    }

    IEnumerator WaitForCinematicEnd()
    {
        // Wait until the cinematic ends
        yield return new WaitForSeconds(cinematicPlayTime);

        // Player can move again after the cinematic ends
        playerCharacterController.enabled = true;
    }
}
