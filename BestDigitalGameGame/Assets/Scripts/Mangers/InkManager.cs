using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using Ink.Runtime;
using MinigameScripts;
using TMPro;
using Vector2 = UnityEngine.Vector2;

// Main author: Nick @Sseptimus
// Secondary author: Charli @CharliSIO
// Manages the INK files and stories running

public class InkManager : MonoBehaviour
{
    public GameManager mGameManager;

    [Header("Dialogue Assets")]
    [SerializeField]
    private TextAsset JaniceJsonAsset;
    [SerializeField]
    private TextAsset GunnerJsonAsset;
    [SerializeField]
    private TextAsset ChamomileJsonAsset;
    [SerializeField]
    private TextAsset CarlJsonAsset;

    private List<TextAsset> DialogueJsons = new List<TextAsset>();
    private TextAsset currentDialogue;
    private int dialogueListIndex = 0;

    private Story _story;
    
    public int m_CustomersLeft = 4; //TODO update with new conversations
    private HoldCounterController m_CounterInstance;
    
    [Header ("Buttons and Prefabs")]
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
    public GameObject numberPuzzleWindow;
    public GameObject minesweeperWindow;
    public float fTotalChatHeight = 0;
    [Header("Popup Windows")]
    public GameObject TaskFailed1;
    public GameObject TaskFailed2;
    public GameObject TaskFailed3;
    private List<GameObject> TaskFailPopups = new List<GameObject>();
    public GameObject BossWatching1;
    public GameObject BossWatching2;
    public GameObject BossWatching3;
    private List<GameObject> BossWatchingPopups = new List<GameObject>();

    // dialogue observer
    private DialogueObserver dialogueVariablesObserver;
    private bool m_bPlayingGame = false;
    private bool m_bWaitingBetweenPeople = false;

    private void Awake()
    {
        dialogueVariablesObserver = new DialogueObserver(loadGlobalsJSON);
        dialogueVariablesObserver.m_GameManager = mGameManager;
        dialogueVariablesObserver.m_InkManager = this;
        m_CounterInstance = FindObjectOfType<HoldCounterController>();
    }

    void Start()
    {
        DialogueJsons.Add(JaniceJsonAsset);
        DialogueJsons.Add(GunnerJsonAsset);
        DialogueJsons.Add(ChamomileJsonAsset);
        DialogueJsons.Add(CarlJsonAsset);

        currentDialogue = DialogueJsons[dialogueListIndex];
        StartStory();
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
            ChatController.CurrentMessage.rectTransform.sizeDelta =
                new Vector2(ChatController.CurrentMessage.rectTransform.sizeDelta.x, (sOutputText.Length / 42)*0.13f);
            ChatController.NPCMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()).GetComponent<RectTransform>().sizeDelta = new Vector2(ChatController.NPCMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()).GetComponent<RectTransform>().sizeDelta.x,ChatController.CurrentMessage.rectTransform.sizeDelta.y);
        }
        else if(ChatController.CurrentMessage != null && ChatController.PlayerMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()))
        {
            ChatController.CurrentMessage.rectTransform.sizeDelta =
                new Vector2(ChatController.CurrentMessage.rectTransform.sizeDelta.x, (sOutputText.Length / 42)*0.13f);
            ChatController.PlayerMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()).GetComponent<RectTransform>().sizeDelta = new Vector2(ChatController.PlayerMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()).GetComponent<RectTransform>().sizeDelta.x,ChatController.CurrentMessage.rectTransform.sizeDelta.y);
        }

        fTotalChatHeight = 0;
        for (int i = 0; i < ChatController.NPCMessageContainer.transform.childCount; i++)
        {
            fTotalChatHeight += ChatController.NPCMessageContainer.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y;
        }
        if (fTotalChatHeight > 0.65f)
        {
            //When window is full removes the top message
            Destroy(ChatController.PlayerMessageContainer.transform.GetChild(0).gameObject);
            Destroy(ChatController.NPCMessageContainer.transform.GetChild(0).gameObject);
        }
    }

    private void StartStory()
    {
        m_bWaitingBetweenPeople = false;
        _story = new Story(currentDialogue.text);
        m_CustomersLeft--;
        m_CounterInstance.UpdateNumber(m_CustomersLeft);
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
                m_bPlayingGame = true;
                GameObject newgame = Instantiate(numberPuzzleWindow);
                newgame.GetComponentInChildren<SliderGameController>().ownedManager = this;
            }
            if (taskName == "minesweeper")
            {
                // instatiate minesweeper puzzle
                m_bPlayingGame = true;
                GameObject newgame = Instantiate(minesweeperWindow);
                newgame.GetComponentInChildren<MineSweeperGameController>().ownedManager = this;
            }
        });

        _story.BindExternalFunction("bossSpotted", (string taskName) =>
        {
            int popUpNum = UnityEngine.Random.Range(0, BossWatchingPopups.Count() - 1);
            GameObject newPopup = Instantiate(BossWatchingPopups[popUpNum], new UnityEngine.Vector3(-2, 0, 0), UnityEngine.Quaternion.identity);
        });

            DisplayNextLine();

        // add the popups to list
        TaskFailPopups.Add(TaskFailed1);
        TaskFailPopups.Add(TaskFailed2);
        TaskFailPopups.Add(TaskFailed3);

        // add boss watching popups to list
        BossWatchingPopups.Add(BossWatching1);
        BossWatchingPopups.Add(BossWatching2);
        BossWatchingPopups.Add(BossWatching3);
    }

    public void GameFailed()
    {
        int popUpNum = UnityEngine.Random.Range(0, TaskFailPopups.Count() - 1);
        GameObject newPopup = Instantiate(TaskFailPopups[popUpNum], new UnityEngine.Vector3(-2, 0, 0), UnityEngine.Quaternion.identity);
        _story.ChoosePathString("TaskFailed");
        DisplayNextLine();
        m_bPlayingGame = false;
    }
    public void GameWon()
    {
        _story.ChoosePathString("TaskSuccess");
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
        else if(_story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else // story cannot continue and no choices available
        {
            if (!m_bWaitingBetweenPeople)
            {
                // story has ended
                ExitDialogue();
                dialogueListIndex++;
                currentDialogue = DialogueJsons[dialogueListIndex];
                Invoke("ClearChat", 3);
                Invoke("StartStory", 3);
                m_bWaitingBetweenPeople = true;
            }
        }
    }

    private void PrintToScreen(string newText,bool _bIsPlayer = false)
    {
        sQueuedText = newText;
        sOutputText = "";
        if (_bIsPlayer)
        {
            ChatController.CurrentMessage = Instantiate(ChatController.MessagePrefab, ChatController.PlayerMessageContainer.transform, false);
            ChatController.PlayerMessages.Append(ChatController.CurrentMessage.GetComponent<TextMeshProUGUI>());
            ChatController.CurrentMessage.text = newText;
            ChatController.NPCMessages.Append(Instantiate(ChatController.MessagePrefab, ChatController.NPCMessageContainer.transform, false).GetComponent<TextMeshProUGUI>());
            ChatController.CurrentMessage.alignment = TextAlignmentOptions.Right;
            ChatController.CurrentMessage.color = Color.black;
            m_bPlayerIsTalking = true;
        }
        else
        {
            ChatController.CurrentMessage = Instantiate(ChatController.MessagePrefab, ChatController.NPCMessageContainer.transform, false);
            ChatController.NPCMessages.Append(ChatController.CurrentMessage.GetComponent<TextMeshProUGUI>());
            ChatController.PlayerMessages.Append(Instantiate(ChatController.MessagePrefab, ChatController.PlayerMessageContainer.transform, false).GetComponent<TextMeshProUGUI>());
            ChatController.CurrentMessage.alignment = TextAlignmentOptions.Left;
            ChatController.CurrentMessage.color = Color.white;
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

    void ClearChat()
    {
        foreach (Transform child in ChatController.PlayerMessageContainer.transform)
        {
            Destroy(child.gameObject);
        }
        ChatController.PlayerMessages.Clear();

        foreach (Transform child in ChatController.NPCMessageContainer.transform)
        {
            Destroy(child.gameObject);
        }

        ChatController.NPCMessages.Clear();
        
        sQueuedText = "";
        sOutputText = "";
        ChatController.CurrentMessage = null;
    }

    void ExitDialogue()
    {
        Destroy(ChatController.PlayerMessageContainer.transform.GetChild(0).gameObject);
        Destroy(ChatController.NPCMessageContainer.transform.GetChild(0).gameObject);

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

    public Story GetStory()
    {
        return _story;
    }
}
