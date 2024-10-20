using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for running the 'CHIMPS' number pattern task
// Author: Charli Jones @CharliSIO
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

    public int[,] possiblePositions = new int[5, 4];

    // Start is called before the first frame update
    void Start()
    {
        setupGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // function to set up the game
    void setupGame()
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

        arrChimpSquares = new GameObject[arrChimpSquaresToLoad.Length];

        // iterate through and instantiate each square
        for (int i = 0; i < arrChimpSquaresToLoad.Length; i++)
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

            arrChimpSquaresToLoad[i] = Instantiate(arrChimpSquaresToLoad[i],
                    new Vector3(GameWindowContent.transform.position.x + x - GameWindowContent.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                        GameWindowContent.transform.position.y + y - GameWindowContent.GetComponent<SpriteRenderer>().bounds.size.y / 2, -2),
                    Quaternion.identity, GameWindowContent.transform) as GameObject;

            // increments number that is displayed on the squares and sets the position as taken
            arrChimpSquaresToLoad[i].GetComponent<ChimpsSquaresScript>().setNumber(i + 1);
            possiblePositions[x, y] = 1;
        }
    }

    void deleteGame()
    {
        for (int i = 0; i < arrChimpSquaresToLoad.Length; i++)
        {
            Destroy(arrChimpSquaresToLoad[i]);
        }
    }
}
