using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingImpact : MonoBehaviour {

    [System.Serializable]
	public enum MaterialTypeEnum {
        Plaster,
	    Metall,
        Folliage,
        Rock,
        Wood,
        Brick,
        Concrete,
        Dirt,
        Glass,
        Water
	}

    public MaterialTypeEnum typeOfMaterial = MaterialTypeEnum.Plaster;              // Tipe of material used to display impact animation.

    // Start is called before the first frame update
    void Start() {
        
    }

    /// <summary>
    /// Display impact effect
    /// in this gameObject surface
    /// collider.
    /// </summary>
    /// <param name="position">Vector3 - where to instantiate impact effect</param>
    public void DisplayImpact( Vector3 position ) {
        if ( SceneImpactMaterials.instance != null ) {
            var materialType = GetImpactEffects();
        }
    }

    /// <summary>
    /// Get impact effect
    /// from scene impact
    /// effects manager.
    /// </summary>
    private GameObject GetImpactEffects() {
        return null;
    }

}
