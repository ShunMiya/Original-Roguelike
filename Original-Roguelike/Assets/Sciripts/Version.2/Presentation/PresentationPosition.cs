using UnityEngine;

namespace Field
{
    public class PresentationPosition : MonoBehaviour
    {
        public float z = 5f;

        public void SetPosition(int xgrid, int zgrid)
        {

            Vector3 DamageCharPos = new (CoordinateTransformation.ToWorldX(xgrid), 0, CoordinateTransformation.ToWorldZ(zgrid));

            Vector3 screenPos = Camera.main.WorldToScreenPoint(DamageCharPos);

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, z));

            transform.position = worldPos;
        }
    }
}