using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    enum BossLocation
    {
        In_Office,
        Up_Pos1,
        Up_Pos2,
        Left_Pos1,
        Left_Pos2,
        Left_Pos3,
        Right_Behind_YOU
    }

    [Header ("Move Variables")]
    //Moving Variables
    public float MaxMoveTimer;
    public float MinMoveTimer;
    float CurrentMoveTimer;
    BossLocation CurrentLocation = BossLocation.In_Office;
    public bool BOSS_IS_WATCHING = false;


    [Header ("Rendering and Sprites")]     
    //Sprites and Renderers
    public SpriteRenderer UpPeek;
    public SpriteRenderer LeftPeek;
    public Sprite _sprIn_Office;
    public Sprite _sprUpPeek1;
    public Sprite _sprUpPeek2;
    public Sprite _sprUpPeekNoBoss;
    public Sprite _sprLeftPeek1;
    public Sprite _sprLeftPeek2;
    public Sprite _sprLeftPeek3;
    public Sprite _sprLeftPeekNoBoss;
    

    // Update is called once per frame
    void Update()
    {
        //if boss timer is zero, move the boss and reset the timer
        if(CurrentMoveTimer <= 0)
        {
            MoveBoss();

            CurrentMoveTimer = Random.Range(MinMoveTimer, MaxMoveTimer);
        }
        else // Else, tick down the boss move timer
        {
            CurrentMoveTimer--;
        }
    }

    void MoveBoss()//Move the boss 'woo'
    {
        //Random the new location
        int NewPosition = Random.Range((int)BossLocation.In_Office,(int)BossLocation.Right_Behind_YOU);
        CurrentLocation = (BossLocation)NewPosition;

        //Set the old sprite back and Set the new sprite
        switch(CurrentLocation)
        {
            case BossLocation.In_Office:
            {
                UpPeek.sprite = _sprIn_Office;
                LeftPeek.sprite = _sprLeftPeekNoBoss;
                BOSS_IS_WATCHING = false;
                break;
            }
            case BossLocation.Up_Pos1:
            {
                UpPeek.sprite = _sprUpPeek1;
                LeftPeek.sprite = _sprLeftPeekNoBoss;
                BOSS_IS_WATCHING = false;
                break;
            }
            case BossLocation.Up_Pos2:
            {
                UpPeek.sprite = _sprUpPeek2;
                LeftPeek.sprite = _sprLeftPeekNoBoss;
                BOSS_IS_WATCHING = false;
                break;
            }
            case BossLocation.Left_Pos1:
            {
                LeftPeek.sprite = _sprLeftPeek1;
                UpPeek.sprite = _sprUpPeekNoBoss;
                BOSS_IS_WATCHING = false;
                break;
            }
            case BossLocation.Left_Pos2:
            {
                LeftPeek.sprite = _sprLeftPeek2;
                UpPeek.sprite = _sprUpPeekNoBoss;
                BOSS_IS_WATCHING = false;
                break;
            }
            case BossLocation.Left_Pos3:
            {
                LeftPeek.sprite = _sprLeftPeek3;
                UpPeek.sprite = _sprUpPeekNoBoss;
                BOSS_IS_WATCHING = false;
                break;
            }
            case BossLocation.Right_Behind_YOU:
            {
                LeftPeek.sprite = _sprLeftPeekNoBoss;
                UpPeek.sprite = _sprUpPeekNoBoss;
                BOSS_IS_WATCHING = true;
                break;
            }
        }

        Debug.Log("Boss Moved! woo!!");
        Debug.Log(CurrentLocation);
    }
}
