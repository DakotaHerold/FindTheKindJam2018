using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DialogueManager : Singleton<DialogueManager> {
    public float letterPause = 0.2f;
    public Text uiText; 
    public DialogueData test;

    int lineIndex = 0;
    bool typingText = false;

    IEnumerator typeRoutine; 

    private void Update()
    {
        //if(InputHandler.Instance.Interact)
        //{
        //    Debug.Log("INTERACTING"); 
        //}

        Debug.Log(InputHandler.Instance.MoveAxes); 


        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    if(typingText)
        //    {
        //        StopCoroutine(typeRoutine);
        //    }
        //    if(lineIndex >= 0)
        //    {
        //        typeRoutine = TypeText(test.playerLines[lineIndex]);
        //        StartCoroutine(typeRoutine);
        //        IncrementLineIndex();
        //    }
            
        //}
    }

    void IncrementLineIndex()
    {
        lineIndex++;
        if (lineIndex > test.playerLines.Count - 1)
        {
            lineIndex = -1; 
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
        typingText = false; 
    }
}
