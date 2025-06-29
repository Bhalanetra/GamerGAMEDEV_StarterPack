using UnityEngine;

public static class RaycastUtility
{
    /// <summary>
    /// Casts a ray from the given origin in the forward direction and returns the point it hits.
    /// Returns Vector3.zero if no hit occurred.
    /// </summary>
    /// <param name="origin">The origin of the raycast</param>
    /// <param name="direction">Direction of the ray</param>
    /// <param name="maxDistance">Maximum distance to check (default 100f)</param>
    /// <param name="layerMask">Optional layer mask to filter hits</param>
    public static Vector3 RaycastHitPoint(Vector3 origin, Vector3 direction, float maxDistance = 100f, LayerMask? layerMask = null)
    {
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if (layerMask.HasValue)
        {
            if (Physics.Raycast(ray, out hit, maxDistance, layerMask.Value))
                return hit.point;
        }
        else
        {
            if (Physics.Raycast(ray, out hit, maxDistance))
                return hit.point;
        }

        return Vector3.zero;
    }
}
