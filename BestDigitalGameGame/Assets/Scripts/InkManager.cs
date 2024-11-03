using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using Ink.UnityIntegration;
using Vector2 = UnityEngine.Vector2;

// Main author: Nick @Sseptimus
// Secondary author: Charli @CharliSIO
// Manages the INK files and stories running
public class InkManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset _inkJsonAsset;
    private Story _story;
    
    [SerializeField]
    private HorizontalLayoutGroup _choiceButtonContainer;

    [SerializeField]
    private Button _choiceButtonPrefab;
    [SerializeField]
    private ChatController ChatController;

    // variable for the load_globals.ink JSON
    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    private string sOutputText;
    private string sQueuedText = "";
    private int iAlternate = 0;
    private bool m_bPlayerIsTalking = false;
    private char[] cPossibleLetters =
    {
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
    };

    [Header("Game Windows")]
    // game windows to be instantiated
    public GameObject chimpsGameWindow;

    [Header("Popup Windows")]
    public GameObject TaskFailed1;
    public GameObject TaskFailed2;
    public GameObject TaskFailed3;
    private List<GameObject> TaskFailPopups = new List<GameObject>();

    // dialogue observer
    private DialogueObserver dialogueVariablesObserver;
    private bool m_bPlayingGame = false;

    private void Awake()
    {
        dialogueVariablesObserver = new DialogueObserver(loadGlobalsJSON);
    }

    void Start()
    {
        StartStory();

        // add the popups to list
        TaskFailPopups.Add(TaskFailed1);
        TaskFailPopups.Add(TaskFailed2);
        TaskFailPopups.Add(TaskFailed3);
    }

    private void FixedUpdate()
    {
        if (sQueuedText.Length == 0)
        {
            ChatController.CurrentMessage = null;
        }
        if (iAlternate % 3 == 0 && sQueuedText.Length != 0)
        {
            sOutputText += sQueuedText[0];
            sQueuedText = sQueuedText.Remove(0, 1);
        }
        if (ChatController.CurrentMessage)
        {
            ChatController.CurrentMessage.text = sOutputText;
        }
        
        iAlternate++;
        if (sQueuedText.Length == 0 && !m_bPlayingGame)
        {
            DisplayNextLine();
        }

        //Making sure padding messages are the same height as their corresponding message
        if (ChatController.CurrentMessage != null && m_bPlayerIsTalking && ChatController.NPCMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()))
        {
            ChatController.NPCMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()).GetComponent<RectTransform>().sizeDelta = new Vector2(ChatController.NPCMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()).GetComponent<RectTransform>().sizeDelta.x,ChatController.CurrentMessage.rectTransform.sizeDelta.y);
        }
        else if(ChatController.CurrentMessage != null && ChatController.PlayerMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()))
        {
            ChatController.PlayerMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()).GetComponent<RectTransform>().sizeDelta = new Vector2(ChatController.PlayerMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()).GetComponent<RectTransform>().sizeDelta.x,ChatController.CurrentMessage.rectTransform.sizeDelta.y);
        }

        if (ChatController.PlayerMessageContainer.transform.childCount > 5 || ChatController.NPCMessageContainer.transform.childCount > 5)
        {
            //When window is full removes the top message
            Destroy(ChatController.PlayerMessageContainer.transform.GetChild(0).gameObject);
            Destroy(ChatController.NPCMessageContainer.transform.GetChild(0).gameObject);
        }
    }

    private void StartStory()
    {
        _story = new Story(_inkJsonAsset.text);

        dialogueVariablesObserver.StartListening(_story);

        _story.BindExternalFunction("runTask", (string taskName) => {
            if (taskName == "chimps")
            {
                m_bPlayingGame = true;
                GameObject newgame = Instantiate(chimpsGameWindow);
                newgame.GetComponentInChildren<ChimpsGame>().ownedManager = this;

            }
            if (taskName == "numberPuzzle")
            {
                // instatiate Game #3 number puzzle
            }
        });

        DisplayNextLine();
    }

    public void GameEnded()
    {
        m_bPlayingGame = false;
        if (_story.canContinue)
        {
            _story.Continue();
        }
    }

    public void GameFailed()
    {
        int popUpNum = UnityEngine.Random.Range(0, TaskFailPopups.Count() - 1);
        GameObject newPopup = Instantiate(TaskFailPopups[popUpNum]);
        _story.ChoosePathString("TaskFailed");
        DisplayNextLine();
        m_bPlayingGame = false;
    }
    public void DisplayNextLine()
    {
        if (_story.canContinue)
        {
            string text = _story.Continue(); // gets next line

            text = text?.Trim(); // removes white space from text
            PrintToScreen(text);
        }
        else if(_story.currentChoices.Count>0)
        {
            DisplayChoices();
        }
    }

    private void PrintToScreen(string newText,bool _bIsPlayer = false)
    {
        sQueuedText = newText;
        sOutputText = "";
        if (_bIsPlayer)
        {
            ChatController.CurrentMessage = Instantiate(ChatController.MessagePrefab, ChatController.PlayerMessageContainer.transform, false);
            ChatController.PlayerMessages.Append(ChatController.CurrentMessage);
            ChatController.CurrentMessage.text = newText;
            ChatController.NPCMessages.Append(Instantiate(ChatController.MessagePrefab, ChatController.NPCMessageContainer.transform, false));
            ChatController.CurrentMessage.alignment = TextAlignmentOptions.Right;
            m_bPlayerIsTalking = true;
        }
        else
        {
            ChatController.CurrentMessage = Instantiate(ChatController.MessagePrefab, ChatController.NPCMessageContainer.transform, false);
            ChatController.NPCMessages.Append(ChatController.CurrentMessage);
            ChatController.PlayerMessages.Append(Instantiate(ChatController.MessagePrefab, ChatController.PlayerMessageContainer.transform, false));
            ChatController.CurrentMessage.alignment = TextAlignmentOptions.Left;
            m_bPlayerIsTalking = false;
        }

    }
    
    private void DisplayChoices()
    {
        // checks if choices are already being displayed
        if (_choiceButtonContainer.GetComponentsInChildren<Button>().Length > 0) return;

        for (int i = 0; i < _story.currentChoices.Count; i++) // iterates through all choices
        {
            var choice = _story.currentChoices[i];
            var button = CreateChoiceButton(choice.text); // creates a choice button

            button.onClick.AddListener(() => OnClickChoiceButton(choice));
        }
    }
    Button CreateChoiceButton(string text)
    {
        // creates the button from a prefab
        var choiceButton = Instantiate(_choiceButtonPrefab);
        choiceButton.transform.SetParent(_choiceButtonContainer.transform, false);
  
        // sets text on the button
        var buttonText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = text;

        return choiceButton;
    }
    void OnClickChoiceButton(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index); // tells ink which choice was selected
        if (_story.canContinue)
        {
            string text = _story.Continue(); // gets next line

            text = text?.Trim(); // removes white space from text
            PrintToScreen(text,true);
        }
        RefreshChoiceView(); // removes choices from the screen
        DisplayNextLine();
    }
    void RefreshChoiceView()
    {
        if (_choiceButtonContainer != null)
        {
            foreach (var button in _choiceButtonContainer.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }
        }
    }

    void ExitDialogue()
    {
        dialogueVariablesObserver.StopListening(_story);
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variablevalue = null;
        dialogueVariablesObserver.variables.TryGetValue(variableName, out variablevalue);
        if (variablevalue == null)
        {
            Debug.LogWarning("Ink variable was found to be null: " + variablevalue);
        }

        return variablevalue;
    }
}
