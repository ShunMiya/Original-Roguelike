using UnityEngine;

//������\���萔
public enum Dir
{
    LeftUp,   // ����
    Up,       // ��
    RightUp,  // �E��
    Left,     // ��
    Right,    // �E
    LeftDown, // ����
    Down,     // ��
    RightDown,// �E��
    NoMove,
};

public static class DirUtil
{
    //�����_���Ȍ�����Ԃ�
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

    //����������(X���ړ�,�ړ��p�x,Z���ړ�)��Ԃ�
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

    //�p�x������(X���ړ�,Z���ړ�)��Ԃ�
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
}