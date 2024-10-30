using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// class for chat window management
// Author: Nick Lees
public class ChatController : MonoBehaviour
{
    public TextMeshProUGUI MessagePrefab;
    public List<TextMeshProUGUI> PlayerMessages;
    public List<TextMeshProUGUI> NPCMessages;
    public VerticalLayoutGroup PlayerMessageContainer;
    public VerticalLayoutGroup NPCMessageContainer;
    public TextMeshProUGUI CurrentMessage ;

    
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
