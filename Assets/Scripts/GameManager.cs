using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    private static GameManager instance;
    private static bool DataLoad()
    {
        instance = new GameManager();
        instance.ballSpeed = 7f;
        instance.barWidth = 2f;
        instance.divadeChance = 50f;
        instance.attack = 1;
        instance.limitBallCount = 1;
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
                DataLoad();
            return instance.ballSpeed;
        }
        set
        {
            if (instance == null)
                DataLoad();
            instance.ballSpeed = value;
            DataSave();
        }
    }
    public static float BarWidth
    {
        get
        {
            if (instance == null)
                DataLoad();
            return instance.barWidth;
        }
        set
        {
            if (instance == null)
                DataLoad();
            instance.barWidth = value;
            DataSave();
        }
    }
    public static float DivideChance
    {
        get
        {
            if (instance == null)
                DataLoad();
            return instance.divadeChance;
        }
        set
        {
            if (instance == null)
                DataLoad();
            instance.divadeChance = value;
            DataSave();
        }
    }
    public static int LimitBallCount
    {
        get
        {
            if (instance == null)
                DataLoad();
            return instance.limitBallCount;
        }
        set
        {
            if (instance == null)
                DataLoad();
            instance.limitBallCount = value;
            DataSave();
        }
    }
    public static int Attack
    {
        get
        {
            if (instance == null)
                DataLoad();
            return instance.attack;
        }
        set
        {
            if (instance == null)
                DataLoad();
            instance.attack = value;
            DataSave();
        }
    }
    private GameManager() { }
   
}
