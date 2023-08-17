using UnityEngine;

namespace Field
{
    public class CoordinateTransformation : MonoBehaviour
    {

        /**
        * �O���b�h���W(X)�����[���h���W(X)�ɕϊ�
        */
        public static float ToWorldX(int xgrid)
        {
            return xgrid * GameRule.GridSize;
        }

        /**
        * �O���b�h���W(Z)�����[���h���W(Z)�ɕϊ�
        */
        public static float ToWorldZ(int zgrid)
        {
            return zgrid * GameRule.GridSize;
        }

        /**
        * ���[���h���W(X)���O���b�h���W(X)�ɕϊ�
        */
        public static int ToGridX(float xworld)
        {
            return Mathf.FloorToInt(xworld / GameRule.GridSize);
        }

        /**
        * ���[���h���W(Z)���O���b�h���W(Z)�ɕϊ�
        */
        public static int ToGridZ(float zworld)
        {
            return Mathf.FloorToInt(zworld / GameRule.GridSize);

        }
    }
}