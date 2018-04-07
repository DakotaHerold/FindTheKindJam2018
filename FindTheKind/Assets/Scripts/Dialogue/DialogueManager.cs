using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DialogueManager : Singleton<DialogueManager> {
    public float letterPause = 0.2f;
    public Text uiText;
    //public DialogueData test;

    private DialogueSource
    private int dialogueLineIndex = 0; 
    private bool typingText = false;

    private IEnumerator typeRoutine;

    DialogueData activeDialogue;

    public DialogueData testDialogue; 

    private void Start()
    {
        StartConversation(testDialogue); 
    }

    private void Update()
    {

        //if (InputHandler.Instance.Interact)
        //{
        //    if (typingText)
        //    {
        //        StopCoroutine(typeRoutine);
        //        if (lineIndex != -1)
        //            uiText.text = test.playerLines[lineIndex - 1];
        //        else
        //            uiText.text = test.playerLines[test.playerLines.Count - 1]; 
        //        typingText = false; 
        //    }
        //    else if (lineIndex >= 0)
        //    {
        //        typeRoutine = TypeText(test.playerLines[lineIndex]);
        //        StartCoroutine(typeRoutine);
        //        IncrementLineIndex();
        //    }
        //}

        if(InputHandler.Instance.Interact)
        {
            CycleDialogue(); 
        }
    }

    void StartConversation(DialogueData data)
    {
        SetActiveDialogue(data);
        CycleDialogue(); 
    }

    void SetActiveDialogue (DialogueData data)
    {
        dialogueLineIndex = 0;
        activeDialogue = data; 
    }

    void CycleDialogue()
    {
        if(dialogueLineIndex < activeDialogue.dialogueLines.Count - 1)
        {
            if(typingText)
            {
                StopCoroutine(typeRoutine);
                uiText.text = activeDialogue.dialogueLines[dialogueLineIndex].line;
                typingText = false;
                dialogueLineIndex++; 
            }
            else
            {
                // TODO, set dialogue source images 
                typeRoutine = TypeText(activeDialogue.dialogueLines[dialogueLineIndex].line);
                StartCoroutine(typeRoutine);
            }
        }
        else
        {
            // Out of dialogue in sequence, check if there are choices to be made 
            // Asserts that there is text for player choice 
            if(typingText)
            {
                StopCoroutine(typeRoutine); 
            }
            else
            {
                if(activeDialogue.playerChoice != "")
                {
                    // TODO, set dialogue source images 
                    
                    typeRoutine = TypeText(activeDialogue.playerChoice);
                    StartCoroutine(typeRoutine);
                    // TODO, make choice buttons 
                }
                else
                {
                    // No choices, end dialogue!
                    Debug.Log("End Dialogue!"); 
                }
                
            }

        }
    }

    IEnumerator TypeText(string message)
    {
        uiText.text = ""; 
        typingText = true; 
        foreach (char letter in message.ToCharArray())
        {
            uiText.text += letter;
            //if (sound)
            //    audio.PlayOneShot(sound);
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
        dialogueLineIndex++; 
        typingText = false; 
    }
}
