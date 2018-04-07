using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum DialogueSource
{
    Player,
    NPC
}

[CreateAssetMenu(fileName = "Data", menuName = "Dialogue/Dialogue Data", order = 1)]
public class DialogueData : ScriptableObject {

    

    [System.Serializable]
    public struct DialoguePiece
    {
        [TextArea]
        public string line;
        public DialogueSource source; // Make sprite image? 
    }
    
    public List<DialoguePiece> dialogueLines;
    public string playerChoice; // To display choice to player
    public List<DialogueData> choices;
    

}
