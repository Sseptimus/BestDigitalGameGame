using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderGameController : MonoBehaviour
{
    public int m_iGridSize;
    private GridLayout m_GridLayout;
    public GameObject m_SlideObjPrefab;
    private int[] m_iAvailableNums = {0,1,2,3,4,5,6,7,8};
    public SliderObjController m_EmptySpace;
    public int m_iCorrectSpaces = 0;

    void Start()
    {
        m_GridLayout = GetComponent<GridLayout>();
        for (int i = 0; i < m_iGridSize-1; i++)
        {
            GameObject tempSlideObj = Instantiate(m_SlideObjPrefab, transform);
            tempSlideObj.GetComponent<SliderObjController>().m_SliderNum = m_iAvailableNums[Random.Range(0, m_iAvailableNums.Length + 1)];
            tempSlideObj.GetComponent<SliderObjController>().m_OwnedGame = this;
        }
        GameObject tempEmptySlideObj = Instantiate(m_SlideObjPrefab, transform);
        tempEmptySlideObj.GetComponent<SpriteRenderer>().enabled = false;
        m_EmptySpace = tempEmptySlideObj.GetComponent<SliderObjController>();
        m_EmptySpace.m_OwnedGame = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
