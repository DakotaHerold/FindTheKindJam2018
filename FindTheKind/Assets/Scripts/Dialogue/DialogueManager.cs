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

    [Space(20)]
    // Top HUD
    public GameObject CoinMeter;
    public GameObject LevelTitle;
    public GameObject Timer;

    [Space(30)]

    // End Game Refs 
    [SerializeField]
    [TextArea]
    public string businessManEndLine;

    [SerializeField]
    public Sprite soccermom_characterPortrait;
    [TextArea]

    [SerializeField]
    public string soccermom_endLine;


    [SerializeField]
    public Sprite youngwoman_characterPortrait;

    [SerializeField]
    [TextArea]
    public string youngwoman_endLine;


    [SerializeField]
    public Sprite youngworker_characterPortrait;

    [SerializeField]
    [TextArea]
    public string youngworker_endLine;

    [SerializeField]
    public Sprite littleboy_characterPortrait;

    [SerializeField]
    [TextArea]
    public string littleboy_endLine;

    [SerializeField]
    public Sprite littlegirl_characterPortrait;

    [SerializeField]
    [TextArea]
    public string littlegirl_endLine;

    [SerializeField]
    public Sprite merchant_characterPortrait;

    [SerializeField]
    [TextArea]
    public string merchant_endLine;

    [SerializeField]
    public Sprite grunge_characterPortrait;

    [SerializeField]
    [TextArea]
    public string grunge_endLine;

    [SerializeField]
    public Sprite cityman_characterPortrait;

    [SerializeField]
    [TextArea]
    public string cityman_endLine;


    [SerializeField]
    public Sprite elderlyman_characterPortrait;

    [SerializeField]
    [TextArea]
    public string elderlyman_endLine;


    [SerializeField]
    public Sprite elderlywoman_characterPortrait;

    [SerializeField]
    [TextArea]
    public string elderlywoman_endLine;





    private Text boxText;
    private Image NPC_Image; 

    private Button[] optionButtons; 
    
    private DialogueSource activeSpeaker; 

    private int dialogueLineIndex = 0; 
    private bool typingText = false;

    private IEnumerator typeRoutine;

    private DialogueData activeDialogue;

    // End Game
    private List<DialogueData.DialoguePiece> peopleSpokenTo = new List<DialogueData.DialoguePiece>();
    private bool inFinalDialogue = false; 

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

        EnableStartGameHud(); 
    }

    private void Update()
    {
        if (inFinalDialogue)
        {
            if (InputHandler.Instance.Interact)
            {
                CycleEndDialogue();
            }
        }
        else
        {
            if (activeDialogue != null)
            {
                if (InputHandler.Instance.Interact)
                {
                    CycleDialogue();
                }
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
        
        GameManager.Instance.TriggerDialogueEnd(); 
    }

    public void DisableStartGameHud()
    {
        CoinMeter.SetActive(false);
        LevelTitle.SetActive(false);
        Timer.SetActive(false);
    }

    public void EnableStartGameHud()
    {
        CoinMeter.SetActive(true);
        LevelTitle.SetActive(true);
        Timer.SetActive(true);
    }

    void SetActiveDialogue (DialogueData data)
    {
        dialogueLineIndex = 0;
        activeDialogue = data;
    }

    void SetEndData()
    {
        DialogueData.DialoguePiece newPiece = new DialogueData.DialoguePiece();
        newPiece.source = activeDialogue.whoStartedConversation;

        switch (newPiece.source)
        {
            case DialogueSource.City_Man:
                newPiece.line = cityman_endLine;
                newPiece.characterPortrait = cityman_characterPortrait;
                break;
            case DialogueSource.Elderly_Man:
                newPiece.line = elderlyman_endLine;
                newPiece.characterPortrait = elderlyman_characterPortrait;
                break;
            case DialogueSource.Elderly_Woman:
                newPiece.line = elderlywoman_endLine;
                newPiece.characterPortrait = elderlywoman_characterPortrait;
                break;
            case DialogueSource.Grunge_Girl:
                newPiece.line = grunge_endLine;
                newPiece.characterPortrait = grunge_characterPortrait;
                break;
            case DialogueSource.Little_Boy:
                newPiece.line = littleboy_endLine;
                newPiece.characterPortrait = littleboy_characterPortrait;
                break;
            case DialogueSource.Little_Girl:
                newPiece.line = littlegirl_endLine;
                newPiece.characterPortrait = littlegirl_characterPortrait;
                break;
            case DialogueSource.Merchant:
                newPiece.line = merchant_endLine;
                newPiece.characterPortrait = merchant_characterPortrait;
                break;
            case DialogueSource.Soccer_Mom:
                newPiece.line = soccermom_endLine;
                newPiece.characterPortrait = soccermom_characterPortrait;
                break;
            case DialogueSource.Young_Woman:
                newPiece.line = youngwoman_endLine;
                newPiece.characterPortrait = youngwoman_characterPortrait;
                break;
            case DialogueSource.Young_Worker:
                newPiece.line = youngworker_endLine;
                newPiece.characterPortrait = youngworker_characterPortrait;
                break;
        }

        bool containsChar = false;

        foreach (DialogueData.DialoguePiece p in peopleSpokenTo)
        {
            if (p.source == newPiece.source)
            {
                containsChar = true;
                break;
            }
        }

        if (containsChar == false)
        {
            peopleSpokenTo.Add(newPiece);
        }
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

                    int index = activeDialogue.dialogueLines.Count - 1;
                    if (index < 1)
                        index = 0; 

                    if (activeDialogue.dialogueLines[index].positiveChoice)
                    {
                        SetEndData();
                    }

                    activeDialogue = null;

                }
                
            }

        }
    }

    public void FinalDialogue()
    {
        dialogueLineIndex = 0;
        inFinalDialogue = true;
        StartEndDialogue(); 
    }

    void StartEndDialogue()
    {
        // Open panels 
        NamePanel.SetActive(true);
        DialoguePanel.SetActive(true);

        activeSpeaker = DialogueSource.Business_Man;
        NPC_PortraitPanel.SetActive(false);
        PC_PortraitPanel.SetActive(true);

        string nameString = DialogueSource.Business_Man.ToString();
        nameString = nameString.Replace('_', ' ');
        NamePanel.GetComponentInChildren<Text>().text = nameString;

        string textToType = businessManEndLine;

        if (typingText)
        {
            StopCoroutine(typeRoutine);
            boxText.text = textToType;
            typingText = false;
        }
        else
        {
            typeRoutine = TypeText(textToType);
            StartCoroutine(typeRoutine);
        }
    }

    void CycleEndDialogue()
    {
        if (dialogueLineIndex < peopleSpokenTo.Count)
        {
            activeSpeaker = peopleSpokenTo[dialogueLineIndex].source;
            NPC_PortraitPanel.SetActive(true);
            NPC_Image.sprite = peopleSpokenTo[dialogueLineIndex].characterPortrait; 
            PC_PortraitPanel.SetActive(false);

            string nameString = peopleSpokenTo[dialogueLineIndex].source.ToString();
            nameString = nameString.Replace('_', ' ');
            NamePanel.GetComponentInChildren<Text>().text = nameString;

            string textToType = peopleSpokenTo[dialogueLineIndex].line;

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
            inFinalDialogue = false;
            
            GameManager.Instance.RestartGame();
            peopleSpokenTo = new List<DialogueData.DialoguePiece>();
        }
    }

    public void DisableConvoHud()
    {
        NamePanel.SetActive(false);
        DialoguePanel.SetActive(false);
        NPC_PortraitPanel.SetActive(false);
        PC_PortraitPanel.SetActive(false);
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
