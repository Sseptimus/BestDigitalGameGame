using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using UnityEngine;

public class SudokuGameController : MonoBehaviour
{
    [Header ("Game Settings")]
    public int m_iGridSize = 9;
    public int m_iDifficulty = 2;

    [Header("Refs")] public GameObject m_CellPrefab;

    private List<List<SudokuCellController>> m_arrCells = new List<List<SudokuCellController>>();
    private List<List<int>> m_arrCellValues = new List<List<int>>();

    // Start is called before the first frame update
    void Start()
    {
        //Because c# works in row major x and y will be reversed
        for (int y = 0; y < m_iGridSize; y++)
        {
            for (int x = 0; x < m_iGridSize; x++)
            {
                GameObject tempObj = Instantiate(m_CellPrefab, transform);
                m_arrCells[y].Add(tempObj.GetComponent<SudokuCellController>());
                int iCellValue;
                do
                {
                    iCellValue = Random.Range(1, m_iGridSize + 1);
                } while (m_arrCellValues[y].Contains(iCellValue) || CheckColumnContains(iCellValue, x));
                m_arrCells[y][x].SetValue(iCellValue);
                m_arrCellValues[y].Add(iCellValue);
            }
        }

        for (int i = 0; i < m_iDifficulty; i++)
        {
            int iRandX = Random.Range(1, m_iGridSize + 1);
            int iRandY = Random.Range(1, m_iGridSize + 1);

            if (!m_arrCells[iRandY][iRandX].GetEmpty())
            {
                m_arrCells[iRandY][iRandX].SetEmpty();
                m_arrCellValues[iRandY][iRandY] = 0;
            }
        }
    }

    private bool CheckColumnContains(int _iNumToCheck, int _iColumnToCheck)
    {
        for (int i = 0; i < m_iGridSize; i++)
        {
            if (m_arrCellValues[i][_iColumnToCheck] == _iNumToCheck)
            {
                return true;
            }
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
