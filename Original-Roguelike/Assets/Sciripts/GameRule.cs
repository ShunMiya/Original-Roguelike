using UnityEngine;
public static class GameRule
{
    private static float gridSize = 1.0f;
    private static float moveSpeed = 40.0f;
    private static float animeSpeed = 1.0f;
    private static float hitRate = 95;
    private static float evasionRate = 0;
    private static float damageIndexValue = 0.9375f;
    private static int charaThrowHitDamage = 10;

    public static float GridSize
    {
        get { return gridSize; }
    }

    public static float MoveSpeed
    { 
        get { return moveSpeed; } 
    }
    public static float AnimeSpeed
    {
        get { return animeSpeed; }
    }
    public static float HitRate
    {
        get { return hitRate; }
    }
    public static float EvasionRate
    {
        get { return evasionRate; }
    }
    public static float DamageIndexValue
    {
        get { return damageIndexValue; }
    }
    public static void DashMove()
    {
        animeSpeed = 1.5f;
        moveSpeed = 0.04f;
    }
    public static void WalkMove()
    {
        animeSpeed = 1.0f;
        moveSpeed = 0.2f;
    }
    public static void ThrowMove()
    {
        animeSpeed = 1.0f;
        moveSpeed = 0.06f;
    }
    public static int CharaThrowHitDamage
    {
        get { return charaThrowHitDamage; }
    }
}
