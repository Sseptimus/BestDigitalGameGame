using UnityEngine;

public class NextButtonScript : MonoBehaviour
{
    private InkManager _inkManager;

    void Start()
    {
        _inkManager = FindObjectOfType<InkManager>();

        if (_inkManager == null)
        {
            Debug.LogError("Ink Manager was not found!");
        }
    }

    public void OnClick()
    {
        _inkManager?.DisplayNextLine();
    }
}