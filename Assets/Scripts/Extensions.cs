using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Extensions
{
    public static Vector3 Multiply(this Vector3 vector1, Vector3 vector2)
    {
        vector1.x *= vector2.x;
        vector1.y *= vector2.y;
        vector1.z *= vector2.z;

        return vector1;
    }

    public static void SafeInvoke(this Action action)
    {
        if (action is { Target: { } })
        {
            action.Invoke();
        }
    }

    public static void SafeInvoke<T>(this Action<T> action, T arg)
    {
        if (action is { Target: { } })
        {
            action.Invoke(arg);
        }
    }

    public static void SafeInvoke<T>(this Action<IEnumerator<T>> action, IEnumerator<T> arg)
    {
        if (action is { Target: { } })
        {
            action.Invoke(arg);
        }
    }

    public static void SafeInvoke<T>(this Action<T[]> action, T[] arg)
    {
        if (action is { Target: { } })
        {
            action.Invoke(arg);
        }
    }

    public static T GetRandomElement<T>(this T[] array)
    {
        if (array.Length <= 0) return default;

        var randomIndex = Random.Range(0, array.Length);

        return array[randomIndex];
    }

    public static T GetRandomElement<T>(this List<T> array)
    {
        if (array.Count <= 0) return default;

        var randomIndex = Random.Range(0, array.Count);

        return array[randomIndex];
    }
}