using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private Vector3 m_v3TargetPosition;

    public float m_fCameraMoveSpeed;

    void Start()
    {
        gameObject.transform.position = m_v3CentrePos;//Set the camera to the centre position
        CurrentCamPosition = CameraPos.Centre;
    }

    // Update is called once per frame
    void Update()
    {
        var Step = m_fCameraMoveSpeed * Time.deltaTime; // calculate distance to move towards destination

        //if we are almost there, set position and stop moving
        if(Vector3.Distance(gameObject.transform.position, m_v3TargetPosition) <= Step)
        {
            gameObject.transform.position = m_v3TargetPosition;
            m_bMoving = false; //reset moving so can move again
        }

        if (m_bMoving)//if moving, then loop the movement, else take an input and update
        {
            transform.position = Vector3.MoveTowards(transform.position, m_v3TargetPosition, Step); //move towards the destination
        }
        else
        {
            //detect Player Input and set target positions
            switch (CurrentCamPosition)
            {
                case CameraPos.Centre: //if the camera is currently at the centre position
                {
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))//Look Left down the Row
                    {
                        CurrentCamPosition = CameraPos.Left;
                        m_v3TargetPosition = m_v3LeftPeekPos;
                        m_bMoving = true;
                    }
                    else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))//Look Right At the Board
                    {
                        CurrentCamPosition = CameraPos.Right;
                        m_v3TargetPosition = m_v3RightBoardPos;
                        m_bMoving = true;
                    }
                    else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w"))//Look Up over the Top of the screen
                    {
                        CurrentCamPosition = CameraPos.Up;
                        m_v3TargetPosition = m_v3UpPeekPos;
                        m_bMoving = true;
                    }
                    break;
                }
                case CameraPos.Left:
                {
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))//Press to go back to centre
                    {
                        CurrentCamPosition = CameraPos.Centre;
                        m_v3TargetPosition = m_v3CentrePos;
                        m_bMoving = true;
                    }
                    break;
                }
                case CameraPos.Up: 
                {
                    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s"))//Press to go back to the centre
                    {
                        CurrentCamPosition = CameraPos.Centre;
                        m_v3TargetPosition = m_v3CentrePos;
                        m_bMoving = true;
                    }
                    break;
                }
                case CameraPos.Right:
                {
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))//Press left to go back to centre
                    {
                        CurrentCamPosition = CameraPos.Centre;
                        m_v3TargetPosition = m_v3CentrePos;
                        m_bMoving = true;
                    }
                    break;
                }
            }
        }
    }
}
