using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderGameController : MonoBehaviour
{
    public int m_iGridSize = 3;
    private GridLayout m_GridLayout;
    public GameObject m_SlideObjPrefab;
    void Start()
    {
        m_GridLayout = GetComponent<GridLayout>();
        for (int i = 0; i < m_iGridSize; i++)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
