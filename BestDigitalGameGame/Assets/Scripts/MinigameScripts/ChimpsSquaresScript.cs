using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // namespace for text mesh pro, for changing text on squares

public class ChimpsSquaresScript : MonoBehaviour
{
    public GameObject self;
    private TMP_Text m_displayNumber;
    private int m_ButtonNumber;
    static int m_currentScore = 0;
    static bool m_numbersVisible = true;

    //when object is created
    private void Awake()
    {
        m_displayNumber = GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "ChimpsNumberSquare";
        Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (self.GetComponent<MeshRenderer>().enabled)
        {
            if (!m_numbersVisible)
            {
                self.GetComponent<MeshRenderer>().enabled = false;
            }
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
                m_numbersVisible = false;
            }

            // set square as inactive if clicked, and add to 'score'
            if (m_ButtonNumber == m_currentScore + 1)
            {
                self.SetActive(false);
                Debug.Log("Score added!");
                m_currentScore++;
            }
            else
            {
                Debug.Log("Wrong number!");
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
