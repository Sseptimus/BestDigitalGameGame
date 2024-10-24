using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using Random = UnityEngine.Random;

public class InkManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset _inkJsonAsset;
    private Story _story;
    
    [SerializeField]
    private VerticalLayoutGroup _choiceButtonContainer;

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
            sQueuedText = sQueuedText.TrimStart(sQueuedText[0]);
        }
        if (ChatController.CurrentMessage)
        {
            ChatController.CurrentMessage.text = sOutputText;
        }
        
        iAlternate++;
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

    private void PrintToScreen(string newText)
    {
        sQueuedText = newText;
        sOutputText = "";
        if (ChatController.NPCMessages.Count == 0||ChatController.NPCMessages[ChatController.NPCMessages.Count - 1] ==
            ChatController.CurrentMessage)
        {
            ChatController.CurrentMessage = Instantiate(ChatController.MessagePrefab, ChatController.PlayerMessageContainer.transform, false);
            ChatController.PlayerMessages.Append(ChatController.CurrentMessage);
        }
        else
        {
            ChatController.CurrentMessage = Instantiate(ChatController.MessagePrefab, ChatController.NPCMessageContainer.transform, false);
            ChatController.NPCMessages.Append(ChatController.CurrentMessage);
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
