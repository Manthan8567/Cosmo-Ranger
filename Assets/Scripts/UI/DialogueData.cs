using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "rename this asset", menuName = "Diaglogue/New Dialogue Data", order = 0)]
public class DialogueData : ScriptableObject
{
    // Dialogue data structure
    public string characterName;
    public string[] dialoguePhrase;
}
