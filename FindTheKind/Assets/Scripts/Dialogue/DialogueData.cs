using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum DialogueSource
{
    Business_Man,
    Soccer_Mom,
    Young_Woman, 
    Young_Worker,
    Little_Boy, 
    Little_Girl, 
    Merchant, 
    Grunge_Girl, 
    City_Man, 
    Elderly_Woman, 
    Elderly_Man
}

[CreateAssetMenu(fileName = "Data", menuName = "Dialogue/Dialogue Data", order = 1)]
public class DialogueData : ScriptableObject {

    [System.Serializable]
    public struct DialoguePiece
    {
        [TextArea]
        public string line;
        public DialogueSource source;
        public Sprite characterPortrait;
        public int moneyCost;
        public bool positiveChoice; 
    }
    
    public List<DialoguePiece> dialogueLines;
    public string playerChoice; // To display choice to player
    public bool isRoot; 
    public List<DialogueData> choices;
    
    public int GetMoneyCost()
    {
        int result = 0;

        foreach(DialoguePiece dialogue in dialogueLines)
        {
            if (dialogue.moneyCost > result)
                result = dialogue.moneyCost; 
        }

        foreach(DialogueData data in choices)
        {
            foreach (DialoguePiece dialogue in data.dialogueLines)
            {
                if (dialogue.moneyCost > result)
                    result = dialogue.moneyCost;
            }
        }

        return result; 
    }

    public DialogueSource whoStartedConversation;
}
