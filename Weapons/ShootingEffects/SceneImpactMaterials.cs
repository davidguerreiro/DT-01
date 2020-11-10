using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneImpactMaterials : MonoBehaviour {
    public static SceneImpactMaterials instance;                    // Public static class instance.

    [Serializable]
    public class ImpactInfo {
        public MaterialType.MaterialTypeEnum MaterialType;          // Material type used to detect type of impact.
        public GameObject ImpactEffect;                             // Impact effect gameObject reference.
    }

    public ImpactInfo[] impactEffects;                              // 

}
