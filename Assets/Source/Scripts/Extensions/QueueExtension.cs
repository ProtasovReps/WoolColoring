using System.Collections.Generic;
using UnityEngine;

public static class QueueExtension
{
    public static void Shuffle<T>(this Queue<T> queue)
    {
        T[] array = queue.ToArray();

        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = Random.Range(0, array.Length);

            (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
        }

        queue = new Queue<T>(array);
    }
}
