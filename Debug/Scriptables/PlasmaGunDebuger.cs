using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGunDebuger : MonoBehaviour {
    public bool restoreToDefault;                               // Restore basic changing values to default.
    public bool restoreCompletely = false;                      // Restore all changing values to default, including those affected by skills and items.
    public PlasmaGun plasmaGunData;

    // Start is called before the first frame update
    void Start() {
        
        // check if the choosen component needs to be restore to default values.
        if ( restoreToDefault ) {
            RestorePlasmaToDefault( restoreCompletely );
        }
    }

    
    /// <summary>
    /// Restore to default values.
    /// </summary>
    /// <parma name="restoreCompletely">bool - wheter to restore the component to real default values ( values before applying skills and items )</param>
    private void RestorePlasmaToDefault( bool restoreCompletely ) {
        plasmaGunData.RestartDefaultValues( restoreCompletely );
    }
}
