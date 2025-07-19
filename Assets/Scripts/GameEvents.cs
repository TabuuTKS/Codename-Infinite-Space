using UnityEngine;

public static class GameEvents
{
    public static int score = 0;
    public static int coins = 0;

    public static float SpeedMultiplier = 1f;
    public static int initScore = 0;
    public static float WaitTimeDivider = 1f;

    public static Vector2 MeteorDir = Vector2.down;
    public static void addScore() { score = score + 5; }

    public static void ResetDefaults()
    {
        score = 0;
        coins = 0;
        SpeedMultiplier = 1f;
        initScore = 0;
        WaitTimeDivider = 1f;
    }
}
