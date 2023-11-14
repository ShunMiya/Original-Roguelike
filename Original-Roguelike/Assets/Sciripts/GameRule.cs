public static class GameRule
{
    private static float gridSize = 1.0f;
    private static float moveSpeed = 40.0f;
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
        moveSpeed = 0.04f;
    }

    public static void WalkMove()
    {
        moveSpeed = 0.2f;
    }

    public static void ThrowMove()
    {
        moveSpeed = 0.08f;
    }

    public static int CharaThrowHitDamage
    {
        get { return charaThrowHitDamage; }
    }
}
