using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tags
{
    public static string WALL = "Wall";
    public static string TAIL = "Tail";
    public static string BOMB = "Bomb";
    public static string FRUIT = "Fruit";
}//tail

public class Metrics
{
    public static float NODE = 0.2f;
}

public enum PlayerDirection
{
    LEFT = 0,
    UP = 1,
    RIGHT = 2,
    DOWN = 3,
    COUNT = 4
};//Player Direction
