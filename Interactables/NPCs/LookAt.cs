using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    public GameObject toLookAt;                         // GameObject to look at.

    [Header("Settings")]
    public float minimunVert = -45f;                        // Minimum angles to look down.
    public float maximunVert = 45f;                         // Maximium angles to look up.

    public float minimunHor = -45f;                        // Minimum angles to look down.
    public float maximunHor = 45f;                         // Maximium angles to look up.

    private Vector3 initialRotation;                        // Initial neck / head rotation.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        LookAtTarget();
    }

    /// <summary>
    /// Look at target.
    /// </summary>
    private void LookAtTarget() {
        if (toLookAt != null) {
            Vector3 relativePos = toLookAt.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            float rotationY = Mathf.Clamp(rotation.eulerAngles.y, minimunHor, maximunHor);
            float rotationX = Mathf.Clamp(rotation.eulerAngles.x, minimunVert, maximunVert);

            transform.eulerAngles = new Vector3(rotationX, rotationY, 0f);
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {
        initialRotation = transform.rotation.eulerAngles;
    }
}
