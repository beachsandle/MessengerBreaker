using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    private static GameManager instance;
    private static bool DataLoad()
    {
        return true;
    }
    private static bool DataSave()
    {
        return true;
    }
    private float ballSpeed;
    private float barWidth;
    private float divadeChance;
    private int limitBallCount;
    private int attack;

    public static float BallSpeed
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance.ballSpeed;
        }
        set
        {
            if (instance == null)
                instance = new GameManager();
            instance.ballSpeed = value;
        }
    }
    public static float BarWidth
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance.barWidth;
        }
        set
        {
            if (instance == null)
                instance = new GameManager();
            instance.barWidth = value;
        }
    }
    public static float DivideChance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance.divadeChance;
        }
        set
        {
            if (instance == null)
                instance = new GameManager();
            instance.divadeChance = value;
        }
    }
    public static int LimitBallCount
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance.limitBallCount;
        }
        set
        {
            if (instance == null)
                instance = new GameManager();
            instance.limitBallCount = value;
        }
    }
    public static int Attack
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance.attack;
        }
        set
        {
            if (instance == null)
                instance = new GameManager();
            instance.attack = value;
        }
    }
    private GameManager() { }
   
}
