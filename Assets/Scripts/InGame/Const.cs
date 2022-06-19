using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KillType
{
    Out,
    Wall,
    Enemy
}

public enum SceneID
{
    MainMenu,
    InGameScene
}

public class Const
{
    public const float Speed = 10f;
    public const int LevelPoolSize = 25;
    public const int EnemiesPoolSize = 20;
    public const string LevelPartsPath = "LevelParts";
    public const string EnemiesPath = "Enemy";
    public static readonly Vector3 WallStartPosition = new Vector3(-4, 0, 0);
    public static readonly Vector3 PlayerStartPosition = new Vector3(6, 0, 0);
}
