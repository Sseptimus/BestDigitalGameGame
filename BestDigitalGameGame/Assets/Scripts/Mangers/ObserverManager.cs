using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverManager : MonoBehaviour
{
    public GameObject BossManager;
    public GameObject CrossFadeController;

    BossManager.BossLocation CurrentLocation;
    VingetteController.TargetScene CurrentPlayerLocation;

    public event Action MovePlayer;
    public event Action MoveBoss;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
