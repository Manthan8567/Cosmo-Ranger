using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrDoQuestManager : QuestManager
{
    [SerializeField] temp_PlayerDiamonds playerDiamonds;

    private void OnEnable()
    {
        playerDiamonds.OnDiamondCollected += UpdateProgression;

        goalNum = 4;
    }

    private void OnDisable()
    {
        playerDiamonds.OnDiamondCollected += UpdateProgression;
    }
}
