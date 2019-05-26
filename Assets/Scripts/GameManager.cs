using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static float BallSpeed { get; set; }
    public static float BarWidth { get; set; }
    public static float DivideChance { get; set; }
    public static int LimitBallCount { get; set; }
    public static int Attack { get; set; }
    static GameManager()
    {
        BallSpeed = 5f;
        BarWidth = 3f;
        DivideChance = 0f;
        LimitBallCount = 1;
        Attack = 1;
    }
   
}
