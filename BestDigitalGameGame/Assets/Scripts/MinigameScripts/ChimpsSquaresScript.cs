using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // namespace for text mesh pro, for changing text on squares

// class for the squares in the'CHIMPS' number pattern task
// Author: Charli Jones @CharliSIO + minor alterations by Nick
public class ChimpsSquaresScript : MonoBehaviour
{
    public GameObject self;
    private TMP_Text m_displayNumber;
    private ChimpsGame m_OwnedGame;
    private int m_ButtonNumber;
    private Animation anim;

    //when object is created
    private void Awake()
    {
        m_displayNumber = transform.GetChild(1).GetComponent<TMP_Text>();
        anim = transform.GetChild(0).GetComponent<Animation>();
        m_OwnedGame = GetComponentInParent<ChimpsGame>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "ChimpsNumberSquare";
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_OwnedGame.getNumbersVisible())
        {
            //Hides Numbers once game has begun (Could be changed in ChimpsGame.cs when the game starts to optimise?) TODO Remove extra comment
            m_displayNumber.enabled = false;
        }
    }

    // Called when moused over
    void OnMouseOver()
    {
        //If mouse is clicked while over the gameobject
        if(Input.GetMouseButtonDown(0))
        {
            // hide numbers from all squares
            if (m_ButtonNumber == 1)
            {
                m_OwnedGame.setNumbersVisible(false);
            }

            // set square as inactive if clicked, and add to 'score'
            if (m_ButtonNumber == m_OwnedGame.getScore() + 1)
            {
                self.SetActive(false);
                Debug.Log("Score added!");
                m_OwnedGame.setScore(m_OwnedGame.getScore()+1);
                
            }
            else
            {
                Debug.Log("Wrong number!");
                m_OwnedGame.setMistakes(m_OwnedGame.getMistakes()+1);
                anim.Play();
            }
        }
    }

    // function to set the number on the square, called by the 'chimpsgame' manager script
    public void setNumber(int _number)
    {
        m_ButtonNumber = _number;
        m_displayNumber.text = _number.ToString();
    }
}
