using System;
using UnityEngine;


public abstract class QuestManager : MonoBehaviour
{
    public bool IsQuestDone { get; private set; } = false;
    public int QuestExp { get; set; }
    public int GoalNum { get; set; }

    private int currProgression = 0;

    public event Action<int> OnUpdateProgression;
    public event Action<int> OnQuestDone;


    public void UpdateProgression()
    {
        currProgression++;

        OnUpdateProgression?.Invoke(currProgression);

        if (currProgression >= GoalNum)
        {
            IsQuestDone = true;

            OnQuestDone?.Invoke(currProgression);
        }
    }
}
