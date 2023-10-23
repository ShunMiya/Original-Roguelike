public class Array2D
{
    public int width;
    public int height;
    private int[,] data;

    public Array2D(int w, int h)
    {
        width = w; height = h;
        data = new int[width, height];
    }

    /**
    * X/Z���W�ɂ���l���擾����
    */
    public int Get(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            return data[x, z];
        }
        return -1;
    }

    /**
    * X/Z���W�ɒl(v)��ݒ肷��
    */
    public int Set(int x, int z, int v)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            data[x, z] = v;
            return v;
        }
        return -1;
    }

    public int AStarGet(int x, int z, Dir d)
    {
        switch (d)
        {
            case Dir.LeftUp:
                if (Get(x + 1, z) == 1) return 1;
                if (Get(x, z - 1) == 1) return 1;
                return Get(x, z);
            case Dir.Up:
                return Get(x, z);
            case Dir.RightUp:
                if (Get(x - 1, z) == 1) return 1;
                if (Get(x, z - 1) == 1) return 1;
                return Get(x, z);
            case Dir.Left:
                return Get(x, z);
            case Dir.Right:
                return Get(x, z);
            case Dir.LeftDown:
                if (Get(x + 1, z) == 1) return 1;
                if (Get(x, z + 1) == 1) return 1;
                return Get(x, z);
            case Dir.Down:
                return Get(x, z);
            case Dir.RightDown:
                if (Get(x - 1, z) == 1) return 1;
                if (Get(x, z + 1) == 1) return 1;
                return Get(x, z);
        }
        return Get(x, z);
    }
}