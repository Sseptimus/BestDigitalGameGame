using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SudokuCellController : MonoBehaviour
{
    public int m_iCellValue;
    private bool m_bEmptyCell = false;

    public void SetValue(int _iValue)
    {
        m_iCellValue = _iValue;
        transform.GetComponent<TextMeshProUGUI>().text = m_iCellValue.ToString();
    }

    public void SetEmpty()
    {
        transform.GetComponent<TextMeshProUGUI>().text = "";
        m_bEmptyCell = true;
    }

    public bool GetEmpty()
    {
        return m_bEmptyCell;
    }
}
