using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float timeScale = 5;
    public PlayerPickup player;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
