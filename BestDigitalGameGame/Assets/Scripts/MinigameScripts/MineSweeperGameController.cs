using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MineSweeperGameController : MonoBehaviour
{
    public GameObject MineObjPrefab;
    public int m_iGridSize = 5;
    public int m_iDifficulty = 12;

    private void Start()
    {
        GetComponent<GridLayoutGroup>().constraintCount = m_iGridSize;
        for (int i = 0; i < m_iGridSize*m_iGridSize; i++)
        {
            //Creating m_iGridSize^2 cells
            GameObject newCell = Instantiate(MineObjPrefab, transform, false);
            newCell.GetComponent<MineCellController>().SetGame(this);
        }

        for (int i = 0; i < m_iGridSize*m_iGridSize; i++)
        {
            //Finding all neighbours of cells
            MineCellController tempChild = transform.GetChild(i).GetComponent<MineCellController>();
            if (i % m_iGridSize != 0)
            {
                tempChild.AddNeighbour(transform.GetChild(i-1).GetComponent<MineCellController>());
                
                if (i > m_iGridSize)
                {
                    tempChild.AddNeighbour(transform.GetChild(i-m_iGridSize-1).GetComponent<MineCellController>());
                }

                if (i < m_iGridSize * (m_iGridSize - 1))
                {
                    tempChild.AddNeighbour(transform.GetChild(i+m_iGridSize-1).GetComponent<MineCellController>());
                }
            }

            if ((i + 1) % m_iGridSize != 0)
            {
                tempChild.AddNeighbour(transform.GetChild(i+1).GetComponent<MineCellController>());
                
                if (i > m_iGridSize)
                {
                    tempChild.AddNeighbour(transform.GetChild(i-m_iGridSize+1).GetComponent<MineCellController>());
                }

                if (i < m_iGridSize * (m_iGridSize - 1))
                {
                    tempChild.AddNeighbour(transform.GetChild(i+m_iGridSize+1).GetComponent<MineCellController>());
                }
            }

            if (i > m_iGridSize)
            {
                tempChild.AddNeighbour(transform.GetChild(i-m_iGridSize).GetComponent<MineCellController>());
            }

            if (i < m_iGridSize * (m_iGridSize - 1))
            {
                tempChild.AddNeighbour(transform.GetChild(i+m_iGridSize).GetComponent<MineCellController>());
            }
        }

        for (int i = 0; i < m_iDifficulty; i++)
        {
            bool bSettingMine = true;
            while (bSettingMine)
            {
                if (!transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<MineCellController>()
                    .m_bIsMine)
                {
                    transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<MineCellController>()
                        .m_bIsMine = true;
                    bSettingMine = false;
                }
            }
        }

        for (int i = 0; i < m_iGridSize*m_iGridSize; i++)
        {
            //Setting non bomb cell values
            if (!transform.GetChild(i).GetComponent<MineCellController>().m_bIsMine)
            {
                transform.GetChild(i).GetComponent<MineCellController>().FindMineCount();
            }
        }
    }
}