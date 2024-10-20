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
    public GameObject GameWindowContent;

    public int[,] possiblePositions = new int[4, 4];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                possiblePositions[i, j] = 0;
            }
        }

        Random.InitState((int)System.DateTime.Now.Ticks);

        arrChimpSquares = new GameObject[arrChimpSquaresToLoad.Length];
       for (int i = 0; i < arrChimpSquaresToLoad.Length; i++)
        {
            int x = (int)Mathf.Floor(Random.Range(1.0f, 5.0f));
            int y = (int)Mathf.Floor(Random.Range(1.0f, 5.0f));

            while (possiblePositions[x, y] != 0)
            {
                x = (int)Mathf.Floor(Random.Range(1.0f, 5.0f));
                y = (int)Mathf.Floor(Random.Range(1.0f, 5.0f));
            }

            arrChimpSquaresToLoad[i] = Instantiate(arrChimpSquaresToLoad[i], new Vector3(x, y, -2), Quaternion.identity, GameWindowContent.transform) as GameObject;
            arrChimpSquaresToLoad[i].GetComponent<ChimpsSquaresScript>().setNumber(i + 1);
            possiblePositions[x, y] = 1;


            //    arrChimpSquaresToLoad[i] = Instantiate(arrChimpSquaresToLoad[i], 
            //        new Vector3(GameWindowContent.transform.position.x+(int)Random.Range(1,GameWindowContent.GetComponent<SpriteRenderer>().bounds.size.x)-GameWindowContent.GetComponent<SpriteRenderer>().bounds.size.x/2, 
            //            GameWindowContent.transform.position.y+(int)Random.Range(1, GameWindowContent.GetComponent<SpriteRenderer>().bounds.size.y)-GameWindowContent.GetComponent<SpriteRenderer>().bounds.size.y/2, -2),   
            //        Quaternion.identity, GameWindowContent.transform) as GameObject;
            //    arrChimpSquaresToLoad[i].GetComponent<ChimpsSquaresScript>().setNumber(i+1);

            //    for (int j = 0; j < i; j++)
            //    {
            //        bool placedCorrectly = false;
            //        while (!placedCorrectly)
            //        {
            //            // replace with correct trigger overlap
            //            Debug.Log("Collision Detected" + i);

            //            if (arrChimpSquaresToLoad[i].GetComponent<BoxCollider2D>().transform.position.x == arrChimpSquaresToLoad[j].GetComponent<BoxCollider2D>().transform.position.x
            //                && arrChimpSquaresToLoad[i].GetComponent<BoxCollider2D>().transform.position.y == arrChimpSquaresToLoad[j].GetComponent<BoxCollider2D>().transform.position.y)
            //            {
            //                arrChimpSquaresToLoad[i].transform.position = new Vector3(GameWindowContent.transform.position.x+(int)Random.Range(1,GameWindowContent.GetComponent<SpriteRenderer>().bounds.size.x)-GameWindowContent.GetComponent<SpriteRenderer>().bounds.size.x/2, 
            //                    GameWindowContent.transform.position.y+(int)Random.Range(1, GameWindowContent.GetComponent<SpriteRenderer>().bounds.size.y)-GameWindowContent.GetComponent<SpriteRenderer>().bounds.size.y/2, -2);
            //                Debug.Log(arrChimpSquaresToLoad[i].transform.position);
            //            }
            //            else {
            //                placedCorrectly = true; break; }
            //        }
            //    }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
