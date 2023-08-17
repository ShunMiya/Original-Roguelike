using UnityEngine;

namespace Field
{
    public class CoordinateTransformation : MonoBehaviour
    {

        /**
        * グリッド座標(X)をワールド座標(X)に変換
        */
        public static float ToWorldX(int xgrid)
        {
            return xgrid * GameRule.GridSize;
        }

        /**
        * グリッド座標(Z)をワールド座標(Z)に変換
        */
        public static float ToWorldZ(int zgrid)
        {
            return zgrid * GameRule.GridSize;
        }

        /**
        * ワールド座標(X)をグリッド座標(X)に変換
        */
        public static int ToGridX(float xworld)
        {
            return Mathf.FloorToInt(xworld / GameRule.GridSize);
        }

        /**
        * ワールド座標(Z)をグリッド座標(Z)に変換
        */
        public static int ToGridZ(float zworld)
        {
            return Mathf.FloorToInt(zworld / GameRule.GridSize);

        }
    }
}