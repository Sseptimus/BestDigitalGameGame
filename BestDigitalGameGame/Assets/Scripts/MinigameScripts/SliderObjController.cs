using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderObjController : MonoBehaviour
{
    public int m_SliderNum;
    public SliderGameController m_OwnedGame;
    public int m_GridPosition;

    private void Start()
    {
        m_GridPosition = transform.GetSiblingIndex();
        if (transform.GetSiblingIndex() == m_SliderNum)
        {
            m_OwnedGame.m_iCorrectSpaces++;
        }
    }

    private void OnMouseDown()
    {
        if (Mathf.Ceil(m_GridPosition / 3) == Mathf.Ceil(m_OwnedGame.m_EmptySpace.transform.GetSiblingIndex()/3) && (m_OwnedGame.m_EmptySpace.transform.GetSiblingIndex()+1 == m_GridPosition || m_OwnedGame.m_EmptySpace.transform.GetSiblingIndex()-1 == m_GridPosition))
        {
            int tempIndex = m_OwnedGame.m_EmptySpace.transform.GetSiblingIndex();
            m_OwnedGame.m_EmptySpace.transform.SetSiblingIndex(transform.GetSiblingIndex());
            transform.SetSiblingIndex(tempIndex);
            m_GridPosition = transform.GetSiblingIndex();
            if (transform.GetSiblingIndex() == m_SliderNum)
            {
                m_OwnedGame.m_iCorrectSpaces++;
            }
            else
            {
                m_OwnedGame.m_iCorrectSpaces--;
            }
        }
        else if (m_GridPosition / 3 - Mathf.Ceil(m_GridPosition / 3) == m_OwnedGame.m_EmptySpace.transform.GetSiblingIndex() / 3 - Mathf.Ceil(m_OwnedGame.m_EmptySpace.transform.GetSiblingIndex() / 3) && (m_OwnedGame.m_EmptySpace.transform.GetSiblingIndex()+3 == m_GridPosition || m_OwnedGame.m_EmptySpace.transform.GetSiblingIndex()-3 == m_GridPosition))
        {
            int tempIndex = m_OwnedGame.m_EmptySpace.transform.GetSiblingIndex();
            m_OwnedGame.m_EmptySpace.transform.SetSiblingIndex(transform.GetSiblingIndex());
            transform.SetSiblingIndex(tempIndex);
            m_GridPosition = transform.GetSiblingIndex();
            if (transform.GetSiblingIndex() == m_SliderNum)
            {
                m_OwnedGame.m_iCorrectSpaces++;
                if (m_OwnedGame.m_iCorrectSpaces == 8)
                {
                    m_OwnedGame.Win();
                }
            }
            else
            {
                m_OwnedGame.m_iCorrectSpaces--;
            }
        }
    }
}
