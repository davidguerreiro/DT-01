using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedShardEnviroment : MonoBehaviour {

    private Rigidbody _rigi;                            // Rigibody component reference.
    private RotateItself _rotateItself;                 // Rotate itsefl class component reference.
    private RotateAround _rotateAround;                 // Rotate around class component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Drop shard and stop gravitating
    /// or rotating around.
    /// </summary>
    public void Drop() {
        _rigi.useGravity = true;
        _rotateItself.speed = 0f;
        _rotateAround.speed = 0f;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get rigibody component reference.
        _rigi = GetComponent<Rigidbody>();

        // get rotate itself class component reference.
        _rotateItself = GetComponent<RotateItself>();

        // get rotate around class component reference.
        _rotateAround = GetComponent<RotateAround>();
    }
}
