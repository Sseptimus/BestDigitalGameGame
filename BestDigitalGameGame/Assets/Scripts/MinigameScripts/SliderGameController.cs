using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SliderGameController : MonoBehaviour
{
    public int m_iGridSize;
    private GridLayout m_GridLayout;
    public GameObject m_SlideObjPrefab;
    private List<int> m_iAvailableNums = new List<int>();
    public SliderObjController m_EmptySpace;

    void Start()
    {
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

    public void WinCheck()
    {
        int iCorrectSquares = 0;
        for (int i = 0; i < 8; i++)
        {
            if (transform.GetChild(i).GetComponent<SliderObjController>().m_SliderNum == i + 1)
            {
                iCorrectSquares++;
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
        gameObject.SetActive(false);
    }
}
