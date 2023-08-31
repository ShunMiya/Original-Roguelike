using UnityEngine;

namespace Field
{
    public class ObjectPosition : MonoBehaviour
    {
        public Pos2D grid = new Pos2D();

        // �C���X�y�N�^�[�̒l���ς�������ɌĂяo�����
        void OnValidate()
        {
            if (grid.x != CoordinateTransformation.ToGridX(transform.position.x) || grid.z != CoordinateTransformation.ToGridZ(transform.position.z))
            {
                transform.position = new Vector3(CoordinateTransformation.ToWorldX(grid.x), 0, CoordinateTransformation.ToWorldZ(grid.z));
            }
        }

        /*
         * * * �w�肵���O���b�h���W�ɍ��킹�Ĉʒu��ύX����
         * */
        public void SetPosition(int xgrid, int zgrid)
        {
            grid.x = xgrid;
            grid.z = zgrid;
            transform.position = new Vector3(CoordinateTransformation.ToWorldX(xgrid), (float)-0.5, CoordinateTransformation.ToWorldZ(zgrid));
        }
    }
}