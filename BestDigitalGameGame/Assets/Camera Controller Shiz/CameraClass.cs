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
    CameraPos CurrentCamPosition;

    bool m_bMoving;

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
        if (m_bMoving)
        {
            switch (CurrentCamPosition)
            {
                    case CameraPos.Centre:
                {
                    break;
                }
                case CameraPos.Left:
                {
                    break;
                }
                case CameraPos.Up: 
                {
                    break;
                }
                case CameraPos.Right:
                {
                    break;
                }
            }
        }
        var Step = m_fCameraMoveSpeed * Time.deltaTime; // calculate distance to move towards destination

        switch (CurrentCamPosition)
        {
            case CameraPos.Centre:
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))//Look Left down the Row
                {
                    transform.position = Vector3.MoveTowards(transform.position, m_v3LeftPeekPos, Step); //move towards the destination
                    CurrentCamPosition = CameraPos.Left;
                }
                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))//Look Right At the Board
                {
                    transform.position = Vector3.MoveTowards(transform.position, m_v3RightBoardPos, Step); //move towards the destination
                    CurrentCamPosition = CameraPos.Right;
                }
                else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w"))//Look Up over the Top of the screen
                {
                    transform.position = Vector3.MoveTowards(transform.position, m_v3UpPeekPos, Step); //move towards the destination
                    CurrentCamPosition = CameraPos.Up;
                }
                break;
            }
            case CameraPos.Left:
            {
                if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey("a"))//Not holding any of the look buttons
                {
                    transform.position = Vector3.MoveTowards(transform.position, m_v3CentrePos, Step); //move towards the destination
                    CurrentCamPosition = CameraPos.Centre;
                }
                break;
            }
            case CameraPos.Up: 
            {
                if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey("d"))//Not Holding any of the directional buttons
                {
                    transform.position = Vector3.MoveTowards(transform.position, m_v3CentrePos, Step); //move towards the destination
                    CurrentCamPosition = CameraPos.Centre;
                }
                break;
            }
            case CameraPos.Right:
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))//Hit the button to bo back to the centre
                {
                    transform.position = Vector3.MoveTowards(transform.position, m_v3CentrePos, Step); //move towards the destination
                    CurrentCamPosition = CameraPos.Centre;
                }
                break;
            }
        }
    }
}
