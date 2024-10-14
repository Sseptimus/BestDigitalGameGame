using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimpsSquaresScript : MonoBehaviour
{
    public GameObject self;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "ChimpsNumberSquare";
        Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called when clicked
    public void squareClicked()
    {
        Debug.Log("Clicked!");
        self.SetActive(!self.activeSelf);
    }
}
