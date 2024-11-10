using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using UnityEngine;

public class MineCellController : MonoBehaviour
{
    private MineSweeperGameController m_OwnedGame;
    private MineCellController[] m_NeighbourCells =new MineCellController[8];
    private int m_iNeighbourCount = 0;
    private int m_iMineCount = 0;
    public bool m_bIsMine;
    private bool m_bFlagged = false;
    private bool m_bHidden = true;

    public void SetGame(MineSweeperGameController _OwnedGame)
    {
        m_OwnedGame = _OwnedGame;
    }

    public void AddNeighbour(MineCellController _Cell)
    {
        m_NeighbourCells[m_iNeighbourCount] = _Cell;
        m_iNeighbourCount++;
    }

    public void FindMineCount()
    {
        foreach (var CurrentCell in m_NeighbourCells)
        {
            if (CurrentCell && CurrentCell.m_bIsMine)
            {
                m_iMineCount++;
            }
        }
        GetComponent<TextMeshProUGUI>().text = m_iMineCount.ToString();
    }

    private void FlagCell()
    {
        if (!m_bFlagged)
        {
            m_bFlagged = true;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            if (m_bIsMine)
            {
                m_OwnedGame.IncrementMinesFound();
            }
        }
        else
        {
            m_bFlagged = false;
            GetComponent<SpriteRenderer>().color = Color.HSVToRGB(0, 0, 51);
            if (m_bIsMine)
            {
                m_OwnedGame.DecreaseMinesFound();
            }
        }
    }

    public void ClickCell()
    {
        if (!m_bFlagged)
        {
            if (m_bIsMine && m_OwnedGame.GetFirstClickOccured())
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                return;
            }
            else if (m_bIsMine && !m_OwnedGame.GetFirstClickOccured())
            {
                //If lose on first click move mine to first free space
               m_bIsMine = false;
               for (int i = 0; i < transform.childCount; i++)
               {
                   if (!transform.parent.GetChild(i).GetComponent<MineCellController>().m_bIsMine)
                   {
                       transform.parent.GetChild(i).GetComponent<MineCellController>().m_bIsMine = true;
                   }
               }
               m_OwnedGame.ResetMineCounts();
            }
            
            if (!m_OwnedGame.GetFirstClickOccured())
            {
                m_OwnedGame.SetFirstClickOccured();
            }
            m_bHidden = false;
            GetComponent<TextMeshProUGUI>().enabled = true;
            if (m_iMineCount == 0)
            {
                foreach (var CurrentCell in m_NeighbourCells)
                {
                    if (CurrentCell && CurrentCell.m_bHidden)
                    {
                        CurrentCell.ClickCell();
                    }
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            FlagCell();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            ClickCell();
        }
    }
}
