using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    /// <summary>
    /// Pick a random item from a list
    /// </summary>
    public static T GetRandom<T>(this List<T> set)
    {
        var setLength = set.Count;
        if (setLength < 1) return default(T);

        var randomIndex = Random.Range(0, setLength);

        return set[randomIndex];
    }

    /// <summary>
    /// Get the next item from a list, wrapping on overflow
    /// </summary>
    public static T GetNext<T>(this List<T> set, T current)
    {
        var currentItemIndex = set.IndexOf(current);

        var nextItemIndex = (currentItemIndex + 1) % set.Count;

        return set[nextItemIndex];
    }

    /// <summary>
    /// Shuffles a given list
    /// </summary>
    public static void Shuffle<T>(this List<T> set)
    {
        var setLength = set.Count;
        if (setLength < 1) return;

        for (int i = 0; i < setLength; i++)
        {
            int randomValue = i + Random.Range(0, setLength - i);
            T item = set[randomValue];
            set[randomValue] = set[i];
            set[i] = item;
        }
    }
}
