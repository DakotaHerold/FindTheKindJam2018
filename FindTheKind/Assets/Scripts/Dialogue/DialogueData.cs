using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Dialogue/Dialogue Data", order = 1)]
public class DialogueData : ScriptableObject {
    struct DialoguePiece
    {
    }

    public List<string> playerLines;
    
}
