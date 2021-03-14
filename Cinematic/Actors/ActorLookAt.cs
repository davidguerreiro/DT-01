using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorLookAt : MonoBehaviour {

    public GameObject toLookAt;                             // To look at gameObject.
    public bool lookingAt;                                  // Flag to control wheter to look at other gameObject.


    private Quaternion _initialRotation;                      // Head initial rotation.
    private Coroutine _restoreRotation;                      // Restore rotation coroutine.

    
    // Update is called once per frame
    void Update() {
        // look at someone if possible.
        if (lookingAt && toLookAt) {
            LookAtAnother();
        }
    }

    /// <summary>
    /// Start looking at another
    /// gameObject.
    /// </summary>
    /// <param name="someone">gameObject - someone to look at.If null, the default one will be used instead</param>
    public void StartLookingAt(GameObject someone = null) {
        if (someone) {
            toLookAt = someone;
        }

        if (toLookAt) {
            lookingAt = true;
        }
    }

    /// <summary>
    /// Stop looking at someone.
    /// </summary>
    /// <param name="restoreRotation">bool - restore to initial rotation. True by default.</param>
    public void StopLookingAt(bool restoreRotation = true) {
        lookingAt = false;

        if (restoreRotation && _restoreRotation == null) {
            _restoreRotation = StartCoroutine(RestoreHeadPosition());
        }
    }

    /// <summary>
    /// Look at another gameObject.
    /// </summary>
    private void LookAtAnother() {
        transform.LookAt(toLookAt.transform);
    }

    /// <summary>
    /// Restore head position.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator RestoreHeadPosition() {
        float speed = 10f;

        while(transform.rotation != _initialRotation) {
            transform.rotation = Quaternion.Slerp(transform.rotation, _initialRotation, speed * Time.deltaTime);
            yield return null;
        }

        _restoreRotation = null;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        _initialRotation = transform.rotation;
    }
}
