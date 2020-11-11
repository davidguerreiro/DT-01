using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneImpactMaterials : MonoBehaviour {
    public static SceneImpactMaterials instance;                    // Public static class instance.

    [Serializable]
    public class ImpactInfo {
        public ShootingImpact.MaterialTypeEnum materialType;          // Material type used to detect type of impact.
        public GameObject impactEffect;                             // Impact effect gameObject reference.
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    public ImpactInfo[] impactEffects;                              // Impact effects reference.

}
