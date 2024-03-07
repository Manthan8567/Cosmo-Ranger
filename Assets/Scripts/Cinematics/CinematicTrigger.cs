using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class CinematicTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;

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
        if (other.tag == "Player" && isPlayed == false)
        {
            // Player cannot move while the cinematic is playing
            player.GetComponent<newPlayerMovement>().enabled = false;
            // This prevents player sliding
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Animator>().SetFloat("Speed", 0);

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
        player.GetComponent<newPlayerMovement>().enabled = true;
    }
}
