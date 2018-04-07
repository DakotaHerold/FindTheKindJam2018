using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum DialogueSource
{
    Business_Man,
    Soccer_Mom,
    Young_Woman, 
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
    }
    
    public List<DialoguePiece> dialogueLines;
    public string playerChoice; // To display choice to player
    public bool isRoot; 
    public List<DialogueData> choices;
    

}
