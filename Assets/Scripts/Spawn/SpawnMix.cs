using UnityEngine;

public abstract class SpawnMix<T> : ScriptableObject where T : System.Enum
{
    public int[] odds;
    public T[] names;
}