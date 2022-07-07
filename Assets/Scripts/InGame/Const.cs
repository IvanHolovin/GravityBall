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
    public const float SPEED = 10f;
    public const int LEVEL_POOL_SIZE = 25;
    public const int ENEMIES_POOL_SIZE = 20;
    public const string LEVEL_PARTS_PATH = "LevelParts";
    public const string ENEMIES_PATH = "Enemy";
    public static readonly Vector3 WallStartPosition = new Vector3(-4, 0, 0);
    public static readonly Vector3 PlayerStartPosition = new Vector3(6, 0, 0);
}
