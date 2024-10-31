using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using Vector2 = UnityEngine.Vector2;

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
    
    private string sOutputText;
    private string sQueuedText = "";
    private int iAlternate = 0;
    private char[] cPossibleLetters =
    {
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
    };

    private bool m_bPlayerIsTalking = false;


    void Start()
    {
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
        if (sQueuedText.Length == 0)
        {
            DisplayNextLine();
        }

        if (ChatController.CurrentMessage != null && m_bPlayerIsTalking && ChatController.NPCMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()))
        {
            ChatController.NPCMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()).GetComponent<RectTransform>().sizeDelta = new Vector2(ChatController.NPCMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()).GetComponent<RectTransform>().sizeDelta.x,ChatController.CurrentMessage.rectTransform.sizeDelta.y);
        }
        else if(ChatController.CurrentMessage != null && ChatController.PlayerMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()))
        {
            ChatController.PlayerMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()).GetComponent<RectTransform>().sizeDelta = new Vector2(ChatController.PlayerMessageContainer.transform.GetChild(ChatController.CurrentMessage.transform.GetSiblingIndex()).GetComponent<RectTransform>().sizeDelta.x,ChatController.CurrentMessage.rectTransform.sizeDelta.y);
        }
    }

    private void StartStory()
    {
        _story = new Story(_inkJsonAsset.text);
        DisplayNextLine();
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
}
