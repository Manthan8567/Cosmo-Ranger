using System;
using UnityEngine;

public abstract class QuestManager : MonoBehaviour
{
    public bool isQuestDone { get; private set; } = false;
    public int goalNum { get; set; }
    private int currProgression = 0;

    public event Action<int> OnUpdateProgression;
    public event Action<int> OnQuestDone;


    public void UpdateProgression()
    {
        currProgression++;

        OnUpdateProgression?.Invoke(currProgression);

        if (currProgression >= goalNum)
        {
            isQuestDone = true;

            OnQuestDone?.Invoke(currProgression);
        }
    }
}
