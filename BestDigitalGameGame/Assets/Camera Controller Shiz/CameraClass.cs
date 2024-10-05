using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CameraPos
{
    Centre,
    Left,
    Up,
    Right
}

public class CameraClass : MonoBehaviour
{
    private CameraPos CurrentCamPosition;

    public Vector3 m_v3CentrePos;
    public Vector3 m_v3LeftPeekPos;
    public Vector3 m_v3RightBoardPos;
    public Vector3 m_v3UpPeekPos;

    public float m_fCameraMoveSpeed;

    void Start()
    {
        gameObject.transform.Translate(m_v3CentrePos, Space.World);//move the camera to the centre position
        CurrentCamPosition = CameraPos.Centre;
    }

    // Update is called once per frame
    void Update()
    {
        var Step = m_fCameraMoveSpeed * Time.deltaTime; // calculate distance to move towards destination
        transform.position = Vector3.MoveTowards(transform.position, m_v3RightBoardPos, Step); //move towards the destination

        switch (CurrentCamPosition)
        {
            case CameraPos.Centre:
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))//Look Left down the Row
                {
                    gameObject.transform.Translate(m_v3LeftPeekPos, Space.World);
                    CurrentCamPosition = CameraPos.Left;
                }
                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))//Look Right At the Board
                {
                    gameObject.transform.Translate(m_v3RightBoardPos, Space.World);
                    CurrentCamPosition = CameraPos.Right;
                }
                else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w"))//Look Up over the Top of the screen
                {
                    gameObject.transform.Translate(m_v3LeftPeekPos, Space.World);
                    CurrentCamPosition = CameraPos.Up;
                }
                break;
            }
            case CameraPos.Left:
            {
                if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey("a"))//Not holding any of the look buttons
                {
                    gameObject.transform.Translate(m_v3CentrePos, Space.World);//Reset to the centre
                    CurrentCamPosition = CameraPos.Centre;
                }
                break;
            }
            case CameraPos.Up: 
            {
                if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey("d"))//Not Holding any of the directional buttons
                {
                    gameObject.transform.Translate(m_v3CentrePos, Space.World);//Reset to the centre
                    CurrentCamPosition = CameraPos.Centre;
                }
                break;
            }
            case CameraPos.Right:
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))//Hit the button to bo back to the centre
                {
                    gameObject.transform.Translate(V3m_CentrePos, Space.World);//move to the centre
                    gameObject.transform.moveTowards()
                    CurrentCamPosition = CameraPos.Centre;
                }
                break;
            }
        }
    }
}
