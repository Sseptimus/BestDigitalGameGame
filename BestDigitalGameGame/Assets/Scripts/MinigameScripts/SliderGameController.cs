using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

// Author: Nick @Sseptimus
// With changes by Charli @CharliSIO
public class SliderGameController : MonoBehaviour
{
    public int m_iGridSize;
    private GridLayout m_GridLayout;
    public GameObject m_SlideObjPrefab;
    private List<int> m_iAvailableNums = new List<int>();
    public SliderObjController m_EmptySpace;
    public InkManager ownedManager;
    public GameObject ownWindow;

    private float gameTimer;
    private float dialogueTimer = 10.0f;

    void Start()
    {
        gameTimer = 120.0f;
        for (int i = 0; i < 8; i++)
        {
            m_iAvailableNums.Add(i + 1);
        }
        m_GridLayout = GetComponent<GridLayout>();
        for (int i = 0; i < m_iGridSize-1; i++)
        {
            GameObject tempSlideObj = Instantiate(m_SlideObjPrefab, transform);
            int iSlideNum = m_iAvailableNums[Random.Range(0, m_iAvailableNums.Count)];
            tempSlideObj.GetComponent<SliderObjController>().m_SliderNum = iSlideNum;
            m_iAvailableNums.Remove(iSlideNum);
            tempSlideObj.GetComponent<TextMeshProUGUI>().text = tempSlideObj.GetComponent<SliderObjController>().m_SliderNum.ToString();
            tempSlideObj.GetComponent<SliderObjController>().m_OwnedGame = this;
        }
        GameObject tempEmptySlideObj = Instantiate(m_SlideObjPrefab, transform);
        tempEmptySlideObj.GetComponent<SpriteRenderer>().enabled = false;
        m_EmptySpace = tempEmptySlideObj.GetComponent<SliderObjController>();
        m_EmptySpace.m_OwnedGame = this;
    }

    private void Update()
    {
        gameTimer -= Time.deltaTime;
        dialogueTimer -= Time.deltaTime;

        if (gameTimer <= 0)
        {
            // player has taken 2 minutes to complete puzzle, player loses game
            ownedManager.GameFailed();
            Destroy(ownWindow);
        }
        if (dialogueTimer <= 0)
        {
            ownedManager.GetStory().ChoosePathString("TaskDialogueNumberPuzzle.random_comment");
            ownedManager.DisplayNextLine();
            dialogueTimer = Random.Range(15.0f, 35.0f);
        }
    }

    public void WinCheck()
    {
        int iCorrectSquares = 0;
        for (int i = 0; i < 8; i++)
        {
            if (transform.GetChild(i).GetComponent<SliderObjController>().m_SliderNum == i + 1)
            {
                iCorrectSquares++;
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        Debug.Log(iCorrectSquares);

        if (iCorrectSquares == 8)
        {
            Win();
        }
    }

    public void Win()
    {
        ownedManager.GameWon();
        Destroy(ownWindow);
    }
}
