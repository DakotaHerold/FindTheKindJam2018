using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DialogueManager : MonoBehaviour {
    public float letterPause = 0.2f;
    public GameObject NamePanel;
    public GameObject DialoguePanel;
    public GameObject PC_PortraitPanel; 
    public GameObject NPC_PortraitPanel; 

    private Text boxText;
    private Image NPC_Image; 

    private Button[] optionButtons; 
    
    private DialogueSource activeSpeaker; 

    private int dialogueLineIndex = 0; 
    private bool typingText = false;

    private IEnumerator typeRoutine;

    DialogueData activeDialogue;

    // Temp
    public DialogueData testDialogue; 

    private void Start()
    {
        boxText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        NPC_Image = NPC_PortraitPanel.transform.GetChild(0).GetComponent<Image>(); 
        optionButtons = transform.GetChild(0).GetComponentsInChildren<Button>();
        OffsetButtonsInitial();
        NamePanel.SetActive(false);
        DialoguePanel.SetActive(false);
        NPC_PortraitPanel.SetActive(false);
        PC_PortraitPanel.SetActive(false); 
        //Temp 
        //StartConversation(testDialogue); 
    }

    private void Update()
    {
        if(activeDialogue != null)
        {
            if (InputHandler.Instance.Interact)
            {
                CycleDialogue();
            }
        }
        
    }

    void OffsetButtonsInitial()
    {
        int numButtons = optionButtons.Length; 
        Vector3 position = boxText.transform.position;
        RectTransform buttonTransform = optionButtons[0].gameObject.transform as RectTransform;
        float offset = buttonTransform.sizeDelta.x/2; 
        position.x -= (offset * (numButtons / 2) ); 
        position.y -= buttonTransform.sizeDelta.y - 15; 
        offset += 10; 
        foreach (Button b in optionButtons)
        {
            b.transform.position = position;
            position.x += offset;
            b.gameObject.SetActive(false); 
        }
    }

    void OffsetButtonsOnChoice()
    {
        int numButtons = activeDialogue.choices.Count;
        Vector3 position = boxText.transform.position;
        RectTransform buttonTransform = optionButtons[0].gameObject.transform as RectTransform;
        float offset = (buttonTransform.sizeDelta.x / numButtons);
        position.x -= (offset * (numButtons / 2));
        position.y -= buttonTransform.sizeDelta.y - 15;
        offset *= 2;
        foreach (Button b in optionButtons)
        {
            b.transform.position = position;
            position.x += offset;
            b.gameObject.SetActive(false);
        }
    }

    public void StartConversation(DialogueData data)
    {
        NamePanel.SetActive(true);
        DialoguePanel.SetActive(true); 
        SetActiveDialogue(data);
        CycleDialogue(); 
    }

    public void EndConversation()
    {
        NamePanel.SetActive(false);
        DialoguePanel.SetActive(false);
        NPC_PortraitPanel.SetActive(false);
        PC_PortraitPanel.SetActive(false); 
        activeDialogue = null;
        GameManager.Instance.TriggerDialogueEnd(); 
    }

    void SetActiveDialogue (DialogueData data)
    {
        dialogueLineIndex = 0;
        activeDialogue = data; 
    }

    void CycleDialogue()
    {
        if(dialogueLineIndex < activeDialogue.dialogueLines.Count)
        {
            activeSpeaker = activeDialogue.dialogueLines[dialogueLineIndex].source;
            if(activeDialogue.dialogueLines[dialogueLineIndex].source != DialogueSource.Business_Man)
            {
                PC_PortraitPanel.SetActive(false); 
                NPC_PortraitPanel.SetActive(true); 
                NPC_Image.sprite = activeDialogue.dialogueLines[dialogueLineIndex].characterPortrait;
            }
            else
            {
                NPC_PortraitPanel.SetActive(false);
                PC_PortraitPanel.SetActive(true); 
            }
            string nameString = activeDialogue.dialogueLines[dialogueLineIndex].source.ToString();
            nameString = nameString.Replace('_', ' '); 
            NamePanel.GetComponentInChildren<Text>().text = nameString;
            string textToType  = activeDialogue.dialogueLines[dialogueLineIndex].line; 

            if (typingText)
            {
                StopCoroutine(typeRoutine);
                boxText.text = textToType;
                typingText = false;
                dialogueLineIndex++; 
            }
            else
            {
                typeRoutine = TypeText(textToType);
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
                boxText.text = activeDialogue.playerChoice; 
                typingText = false;
            }
            else
            {
                if(activeDialogue.isRoot)
                {
                    // TODO, set dialogue source images 
                    
                    typeRoutine = TypeText(activeDialogue.playerChoice);
                    StartCoroutine(typeRoutine);
                    // TODO, make choice buttons 
                    OffsetButtonsOnChoice(); 
                    for (int iChoice = 0; iChoice < activeDialogue.choices.Count; ++iChoice)
                    {
                        optionButtons[iChoice].GetComponentInChildren<Text>().text = activeDialogue.choices[iChoice].playerChoice;
                        DialogueData choiceData = activeDialogue.choices[iChoice];
                        optionButtons[iChoice].onClick.AddListener(delegate {
                            ChoiceAction(choiceData); 
                        });
                        optionButtons[iChoice].gameObject.SetActive(true); 
                    }

                }
                else 
                {
                    // Check if NPC asked for coins 
                    if (activeDialogue.dialogueLines[dialogueLineIndex-1].moneyCost > 0)
                        GameManager.Instance.NumCoins -= activeDialogue.dialogueLines[dialogueLineIndex-1].moneyCost; 
                    // No choices, end dialogue!
                    EndConversation(); 
                    
                }
                
            }

        }
    }

    void ChoiceAction(DialogueData choiceData)
    {
        // Disable currently enabled buttons
        foreach(Button b in optionButtons)
        {
            b.gameObject.SetActive(false); 
        }
        SetActiveDialogue(choiceData);
        CycleDialogue();
    }

    IEnumerator TypeText(string message)
    {
        boxText.text = ""; 
        typingText = true; 
        foreach (char letter in message.ToCharArray())
        {
            boxText.text += letter;
            //if (sound)
            //    audio.PlayOneShot(sound);
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
        dialogueLineIndex++; 
        typingText = false; 
    }
}
