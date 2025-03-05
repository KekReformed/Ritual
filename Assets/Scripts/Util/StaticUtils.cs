using UnityEngine;

public static class StaticUtils
{
    public static Color fadeColor(Color color, float time, float maxTime)
    {
        return new Color(color.r,color.g,color.b, time / maxTime);
    }
}
