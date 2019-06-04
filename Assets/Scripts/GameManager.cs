using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
public static class Extends
{
    public static IEnumerable<T> Meet<T>(this IEnumerable<T> collectoin, Predicate<T> condition)
    {
        foreach (var element in collectoin)
            if (condition(element))
                yield return element;
    }
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collectoin, Action<T> action)
    {
        foreach (var element in collectoin)
        {
            action(element);
            yield return element;
        }
    }
    public static int CountIf<T>(this IEnumerable<T> list, Predicate<T> condition)
    {
        int count = 0;
        foreach (var element in list)
            if (condition(element))
                ++count;
        return count;
    }
    public static List<T> Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }
}
