using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using System.Linq;

public static class NetworkListExtensions
{
    /// <summary>
    /// Returns NetworkList as an IEnumerable
    /// </summary>
    public static IEnumerable<T> ToEnumerable<T>(this NetworkList<T> set) where T : unmanaged, IEquatable<T>
    {
        foreach (var item in set) yield return item;
    }

    /// <summary>
    /// Return a NetworkList as a List
    /// </summary>
    public static List<T> ToList<T>(this NetworkList<T> set) where T : unmanaged, IEquatable<T>
    {
        return set.ToEnumerable().ToList();
    }
}
