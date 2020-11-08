using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {
    public ItemData data;                           // Item data source.
    public Coroutine useCoroutine;                 // Use coroutine.

    /// <summary>
    /// Use item action.
    /// </summary>
    /// <parma name="audioSource">AudioComponent - Audio source to play sounds. Passed by reference.</param>
    public abstract void Use();

    /// <summary>
    /// Use item action coroutine.
    /// Item use logic goes here.
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected abstract IEnumerator UseRoutine();
}
