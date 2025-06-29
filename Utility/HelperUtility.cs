using UnityEngine;

namespace GamerGAMEDEV
{
    namespace Utility
    {
        public static class HelperUtility
        {
            /// <summary>
            /// Returns a point offset units away from PointA in the direction from PointB to PointA.
            /// </summary>
            /// <param name="pointA">Start point (origin)</param>
            /// <param name="pointB">Direction reference point</param>
            /// <param name="offset">Distance to offset</param>
            /// <returns>PointC offset from PointA in the direction from PointB to PointA</returns>
            public static Vector3 GetPointInOppositeDirection(Vector3 to, Vector3 from, float offset)
            {
                Vector3 direction = (to - from).normalized; // Direction from B to A
                return to + direction * offset;
            }

        }
    }
}
