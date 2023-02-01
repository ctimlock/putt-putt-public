using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ArrayExtensions
{
    /// <summary>
    /// Pick a random item from an array
    /// </summary>
    public static T GetRandom<T>(this T[] set)
    {
        var setLength = set.Length;
        if (setLength <= 0) return default(T);

        var randomIndex = UnityEngine.Random.Range(0, setLength);

        return set[randomIndex];
    }

    /// <summary>
    /// Get the next item from a list, wrapping on overflow
    /// </summary>
    public static T GetNext<T>(this T[] set, T current)
    {
        var currentItemIndex = Array.IndexOf(set, current);

        var nextItemIndex = (currentItemIndex + 1) % set.Length + 1;

        return set[nextItemIndex];
    }

    /// <summary>
    /// Shuffles a given array.
    /// </summary>
    public static void Shuffle<T>(this T[] set)
    {
        var setLength = set.Length;
        for (int i = 0; i < setLength; i++)
        {
            int randomValue = i + UnityEngine.Random.Range(0, setLength - i);
            T item = set[randomValue];
            set[randomValue] = set[i];
            set[i] = item;
        }
    }
}
