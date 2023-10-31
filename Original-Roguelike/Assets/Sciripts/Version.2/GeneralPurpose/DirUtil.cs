using UnityEngine;

//方向を表す定数
public enum Dir
{
    Pause,
    LeftUp,   // 左上
    Up,       // 上
    RightUp,  // 右上
    Left,     // 左
    Right,    // 右
    LeftDown, // 左下
    Down,     // 下
    RightDown,// 右下
};

public static class DirUtil
{
    //ランダムな向きを返す
    public static Dir RandomDirection()
    {
        int dirnum = Random.Range(0, 8);
        switch (dirnum)
        {
            case 0:
                return Dir.LeftUp;
            case 1:
                return Dir.Up;
            case 2:
                return Dir.RightUp;
            case 3:
                return Dir.Left;
            case 4:
                return Dir.Right;
            case 5:
                return Dir.LeftDown;
            case 6:
                return Dir.Down;
            case 7:
                return Dir.RightDown;
        }
        return Dir.Down;
    }

    //向きを元に(X軸移動,移動角度,Z軸移動)を返す
    public static Vector3 SetNewPosRotation(Dir d)
    {
        switch(d)
        {
            case Dir.LeftUp:
                return new Vector3(-1, -45, 1);
            case Dir.Up:
                return new Vector3(0, 0, 1);
            case Dir.RightUp:
                return new Vector3(1, 45, 1);
            case Dir.Left:
                return new Vector3(-1, -90, 0);
            case Dir.Right:
                return new Vector3(1, 90, 0);
            case Dir.LeftDown:
                return new Vector3(-1, -135, -1);
            case Dir.Down:
                return new Vector3(0, 180, -1);
            case Dir.RightDown:
                return new Vector3(1, 135, -1);
        }
        return new Vector3(0, 0, 0);
    }

    public static Dir GetNewPosRotation(Pos2D pos, Pos2D targetPos)
    {
        if (pos.x == targetPos.x)
        {
            if (pos.z < targetPos.z) return Dir.Up;
            if (pos.z > targetPos.z) return Dir.Down;
        }
        if (pos.z == targetPos.z)
        {
            if (pos.x < targetPos.x) return Dir.Right;
            if (pos.x > targetPos.x) return Dir.Left;
        }
        if(pos.x < targetPos.x)
        {
            if (pos.z < targetPos.z) return Dir.RightUp;
            if (pos.z > targetPos.z) return Dir.RightDown;
        }
        if (pos.x > targetPos.x)
        {
            if (pos.z < targetPos.z) return Dir.LeftUp;
            if (pos.z > targetPos.z) return Dir.LeftDown;
        }
        return Dir.Pause;
    }

    //角度を元に(X軸移動,Z軸移動)を返す
    public static Pos2D SetAttackPoint(int R)
    {
        switch (R)
        {
            case -45:
                return new Pos2D { x = -1, z = 1 };
            case 0:
                return new Pos2D { x = 0, z = 1 };
            case 45:
                return new Pos2D { x = 1, z = 1 };
            case -90:
                return new Pos2D { x = -1, z = 0 };
            case 90:
                return new Pos2D { x = 1, z = 0 };
            case -135:
                return new Pos2D { x = -1, z = -1 };
            case 180:
                return new Pos2D { x = 0, z = -1 };
            case 135:
                return new Pos2D { x = 1, z = -1 };
        }
        return new Pos2D {x = 0, z = 0};
    }

    //位置と向きを元に正面の位置を返す
    public static Pos2D GetNewGrid(Pos2D position, Dir d)
    {
        Pos2D newP = new Pos2D();
        newP.x = position.x;
        newP.z = position.z;
        switch (d)
        {
            case Dir.LeftUp:
                newP.x -= 1;  newP.z += 1; break;
            case Dir.Up:
                newP.z += 1; break;
            case Dir.RightUp:
                newP.x += 1; newP.z += 1; break;
            case Dir.Left:
                newP.x -= 1; break;
            case Dir.Right:
                newP.x += 1; break;
            case Dir.LeftDown:
                newP.x -= 1; newP.z -= 1; break;
            case Dir.Down:
                newP.z -= 1; break;
            case Dir.RightDown:
                newP.x += 1; newP.z -= 1; break;
        }
        return newP;
    }

    //角度を元に逆向きの角度を返す
    public static int ReverseDirection(int R)
    {
        switch (R)
        {
            case -45:
                return 135;
            case 0:
                return 180;
            case 45:
                return -135;
            case -90:
                return 90;
            case 90:
                return -90;
            case -135:
                return 45;
            case 180:
                return 0;
            case 135:
                return -45;
        }
        return 0;
    }

    //角度を元に向きを返す
    public static Dir GetDirection(int R)
    {
        switch (R)
        {
            case -45:
                return Dir.LeftUp;
            case 0:
                return Dir.Up;
            case 45:
                return Dir.RightUp;
            case -90:
                return Dir.Left;
            case 90:
                return Dir.Right;
            case -135:
                return Dir.LeftDown;
            case 180:
                return Dir.Down;
            case 135:
                return Dir.RightDown;
        }
        return Dir.Pause;
    }

    //向きを元に角度を返す
    public static int GetRotation(Dir D)
    {
        switch (D)
        {
            case Dir.LeftUp:
                return -45;
            case Dir.Up:
                return 0;
            case Dir.RightUp:
                return 45;
            case Dir.Left:
                return -90;
            case Dir.Right:
                return 90;
            case Dir.LeftDown:
                return -135;
            case Dir.Down:
                return 180;
            case Dir.RightDown:
                return 135;
        }
        return 0;
    }

    //向きを元に逆の向きを返す
    public static Dir ReverseDir(Dir D)
    {
        switch (D)
        {
            case Dir.LeftUp:
                return Dir.RightDown;
            case Dir.Up:
                return Dir.Down;
            case Dir.RightUp:
                return Dir.LeftDown;
            case Dir.Left:
                return Dir.Right;
            case Dir.Right:
                return Dir.Left;
            case Dir.LeftDown:
                return Dir.RightUp;
            case Dir.Down:
                return Dir.Up;
            case Dir.RightDown:
                return Dir.LeftUp;
        }
        return Dir.Pause;
    }
}