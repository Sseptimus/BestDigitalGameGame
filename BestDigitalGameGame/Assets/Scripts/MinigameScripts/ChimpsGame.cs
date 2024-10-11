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
            arrChimpSquaresToLoad[i] = Instantiate(arrChimpSquaresToLoad[i], 
                new Vector3((int)Random.Range(GameWindowContent.transform.position.x, GameWindowContent.size.x + GameWindowContent.transform.position.x), 
                            (int)Random.Range(GameWindowContent.transform.position.y, GameWindowContent.size.y + GameWindowContent.transform.position.y), -2), 
                Quaternion.identity, GameWindowContent.transform) as GameObject;
            for (int j = 0; j < i; j++)
            {
                bool placedCorrectly = false;
                while (!placedCorrectly)
                {
                    if (arrChimpSquaresToLoad[i].GetComponent<BoxCollider2D>().transform.position.x == arrChimpSquaresToLoad[j].GetComponent<BoxCollider2D>().transform.position.x
                        && arrChimpSquaresToLoad[i].GetComponent<BoxCollider2D>().transform.position.y == arrChimpSquaresToLoad[j].GetComponent<BoxCollider2D>().transform.position.y)
                    {
                        arrChimpSquaresToLoad[i].transform.position = new Vector3((int)Random.Range(GameWindowContent.transform.position.x, GameWindowContent.size.x + GameWindowContent.transform.position.x),
                            (int)Random.Range(GameWindowContent.transform.position.y, GameWindowContent.size.y + GameWindowContent.transform.position.y), -2);
                    }
                    else {
                        placedCorrectly = true; break; }
                }
            }
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
