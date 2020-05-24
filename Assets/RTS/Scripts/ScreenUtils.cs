using UnityEngine;
using System.Collections;

namespace rts
{
    public static class ScreenUtils
    {
        public static Rect PointsToRect(Vector2 pointA, Vector2 pointB)
        {
            var beginYPos = Mathf.Min(pointA.y, pointB.y);
            var endYPos = Mathf.Max(pointA.y, pointB.y);

            var beginXPos = Mathf.Min(pointA.x, pointB.x);
            var endXPos = Mathf.Max(pointA.x, pointB.x);

            return Rect.MinMaxRect(beginXPos, beginYPos, endXPos, endYPos);
        }
    }
}

