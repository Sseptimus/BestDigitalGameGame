using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFocusCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<SpriteRenderer>())
        {
            GetComponent<SpriteRenderer>().sortingLayerName = transform.root.GetComponent<WindowController>().m_CurrentLayer;
        }

        if (GetComponent<Canvas>())
        {   
            GetComponent<Canvas>().sortingLayerName = transform.root.GetComponent<WindowController>().m_CurrentLayer;
        }
    }
}
