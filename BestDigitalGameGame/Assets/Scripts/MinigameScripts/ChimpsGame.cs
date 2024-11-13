using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// class for running the 'CHIMPS' number pattern task
// Author: Charli Jones @CharliSIO + minor alterations by Nick
public class ChimpsGame : MonoBehaviour
{
    // Need 7 squares, one with each number
    // squares have 2 states, showing number or not showing number
    // squares can be clicked on and must be clicked in correct order
    // once all squares have been clicked the game resets with squares in different places
    // make array of positions available for squares to be placed in

    // array for squares
    public GameObject[] arrChimpSquaresToLoad;
    public GameObject[] arrChimpSquares;

    public GameObject GameWindowContent;

    public GameObject ownWindow;
    public GameManager GameManager;

    private int totalWins = 0;

    public int[,] possiblePositions = new int[5, 4];
    
    public int m_currentScore = 0; //TODO Make private
    private int m_mistakesMade = 0;
    private bool m_numbersVisible = true;

    public InkManager ownedManager; 


    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        setupGame(5);
    }

    // Update is called once per frame
    void Update()
    {
        // check whether the player has made too many mistakes
        if (m_mistakesMade >= 3)
        {
            Debug.Log("Task Failed.");
            ownedManager.GameFailed();
            GameManager.WindowInFocus = null;
            Destroy(ownWindow);
            // insert other consequence here
        }

        // if score is equal to total amount of squares (game is compelte) then destroy game and reset
        if (m_currentScore == arrChimpSquares.Length)
        {
            if (totalWins >= 2)
            {
                Debug.Log("Task Complete.");
                ownedManager.GameWon();
                GameManager.WindowInFocus = null;
                Destroy(ownWindow);
                Destroy(this);
                // hooray task complete reflect this in dialogue etc
            }
            else
            {
                deleteGame();
                totalWins++;
                setupGame(arrChimpSquares.Length + 2);
            }
        }
    }

    // function to set up the game
    void setupGame( int _totalSquares)
    {
        // reset the array so all positons are empty
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                possiblePositions[i, j] = 0;
            }
        }

        Random.InitState((int)System.DateTime.Now.Ticks);

        // set the length of the array to be the same as the total amount of squares needed
        arrChimpSquares = new GameObject[_totalSquares];

        // iterate through and instantiate each square
        for (int i = 0; i < _totalSquares; i++)
        {
            // choose a position
            int x = (int)Mathf.Floor(Random.Range(1.0f, 4.99f));
            int y = (int)Mathf.Floor(Random.Range(1.0f, 3.99f));

            // if position is already taken choose another position
            while (possiblePositions[x, y] != 0)
            {
                x = (int)Mathf.Floor(Random.Range(1.0f, 4.99f));
                y = (int)Mathf.Floor(Random.Range(1.0f, 3.99f));
            }

            // instantiate the sqaures based on position taken from array and adjusted to be within bounds of the window
            arrChimpSquares[i] = Instantiate(arrChimpSquaresToLoad[i],
                    new Vector3(GameWindowContent.transform.position.x + x - GameWindowContent.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                        GameWindowContent.transform.position.y + y - GameWindowContent.GetComponent<SpriteRenderer>().bounds.size.y / 2, -2),
                    Quaternion.identity, GameWindowContent.transform) as GameObject;

            // increments number that is displayed on the squares and sets the position as taken
            arrChimpSquares[i].GetComponent<ChimpsSquaresScript>().setNumber(i + 1);
            possiblePositions[x, y] = 1;
        }

        // make sure the numbers are visible
        m_numbersVisible = true;
    }

    // delete the squares ready for new set of squares to be instantiated
    void deleteGame()
    {
        // reset static valuyes score and mistake counter
        m_currentScore = 0;
        m_mistakesMade = 0;

        // iterate through array and destroy each object
        for (int i = 0; i < arrChimpSquares.Length; i++)
        {
            Destroy(arrChimpSquares[i]);
        }
    }
    
    //Getters and Setters\\
    
    public void setScore(int _score)
    {
        m_currentScore = _score;
    }
    public int getScore()
    {
        return m_currentScore;
    }

    public void setMistakes(int _mistakes)
    {
        m_mistakesMade = _mistakes;
    }
    public int getMistakes()
    {
        return m_mistakesMade;
    }

    public void setNumbersVisible(bool _setting)
    {
        m_numbersVisible = _setting;
    }
    public bool getNumbersVisible()
    {
        return m_numbersVisible;
    }
}
