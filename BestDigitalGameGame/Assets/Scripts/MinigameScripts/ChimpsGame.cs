using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public BaseWindowClass GameWindow;
    public SpriteRenderer GameWindowContent;


    // Start is called before the first frame update
    void Start()
    {
        arrChimpSquares = new GameObject[arrChimpSquaresToLoad.Length];
       for (int i = 0; i < arrChimpSquaresToLoad.Length; i++)
        {
            arrChimpSquaresToLoad[i] = Instantiate(arrChimpSquaresToLoad[i], new Vector3((int)Random.Range(GameWindow.transform.position.x, GameWindowContent.size.x + GameWindow.transform.position.x), (int)Random.Range(GameWindow.transform.position.y, GameWindowContent.size.y + GameWindow.transform.position.y), -2), Quaternion.identity) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaceSquares()
    {

    }
}
