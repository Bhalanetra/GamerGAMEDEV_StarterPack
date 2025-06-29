using System;
using UnityEngine;

public static class LerpUtility
{
    /// <summary>
    /// Smoothly lerps current toward target and invokes callback only once when within threshold.
    /// </summary>
    public static Vector3 DoLerp(Vector3 current, Vector3 target, float speed, float threshold, Action onComplete, ref bool isComplete)
    {
        if (isComplete)
            return target;

        Vector3 newPos = Vector3.Lerp(current, target, Time.deltaTime * speed);

        if (Vector3.Distance(newPos, target) <= threshold)
        {
            isComplete = true;
            onComplete?.Invoke();
            return target;
        }

        return newPos;
    }

    /// <summary>
    /// Lerps from one point to another with an upward arc and invokes onComplete when done.
    /// </summary>
    /// <param name="from">Current position</param>
    /// <param name="to">Target position</param>
    /// <param name="speed">Lerp speed</param>
    /// <param name="arcCurve">AnimationCurve that controls arc shape (0 to 1)</param>
    /// <param name="arcHeightMultiplier">How high the arc goes relative to distance</param>
    /// <param name="onComplete">Callback when done</param>
    /// <param name="ref isComplete">Tracks whether the callback already fired</param>
    public static Vector3 DoArcLerp(
        Vector3 from,
        Vector3 to,
        float speed,
        AnimationCurve arcCurve,
        float arcHeightMultiplier,
        Action onComplete,
        ref bool isComplete
    )
    {
        if (isComplete)
            return to;

        float t = Time.deltaTime * speed;

        Vector3 flatLerp = Vector3.Lerp(from, to, t);
        float progress = Vector3.Distance(from, flatLerp) / Vector3.Distance(from, to);
        float arcHeight = arcCurve.Evaluate(progress) * Vector3.Distance(from, to) * arcHeightMultiplier;

        flatLerp.y += arcHeight;

        if (Vector3.Distance(flatLerp, to) <= 0.01f)
        {
            isComplete = true;
            onComplete?.Invoke();
            return to; // Snap to exact target
        }

        return flatLerp;
    }

    /// <summary>
    /// Linearly lerps from one point to another with an upward arc and invokes onComplete when done.
    /// </summary>
    /// <param name="from">Starting position</param>
    /// <param name="to">Target position</param>
    /// <param name="speed">Units per second</param>
    /// <param name="arcCurve">AnimationCurve for arc shaping (0–1)</param>
    /// <param name="arcHeightMultiplier">Height multiplier for arc</param>
    /// <param name="onComplete">Callback when done</param>
    /// <param name="ref progress">Progress value between 0 and 1</param>
    /// <param name="ref isComplete">Ensures callback fires once</param>
    public static Vector3 DoArcLerpLinear(
        Vector3 from,
        Vector3 to,
        float speed,
        AnimationCurve arcCurve,
        float arcHeightMultiplier,
        Action onComplete,
        ref float progress,
        ref bool isComplete
    )
    {
        if (isComplete)
            return to;

        float totalDistance = Vector3.Distance(from, to);
        if (totalDistance == 0f)
        {
            isComplete = true;
            onComplete?.Invoke();
            return to;
        }

        // Advance progress linearly based on speed and distance
        progress += (Time.deltaTime * speed) / totalDistance;
        progress = Mathf.Clamp01(progress);

        Vector3 flatLerp = Vector3.Lerp(from, to, progress);
        float arcHeight = arcCurve.Evaluate(progress) * totalDistance * arcHeightMultiplier;

        flatLerp.y += arcHeight;

        if (progress >= 1f)
        {
            isComplete = true;
            onComplete?.Invoke();
            return to;
        }

        return flatLerp;
    }
}
