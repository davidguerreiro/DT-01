using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSection : MonoBehaviour {

    public float toWaitTillNext;                                  // How long to wait till next damaged element is displayed.

    private ObjectPool _pool;                                     // Damage text object pool.
    private List<DamageItem> _stack;                              // Stack list of items. Used to avoid overlap of text.
    private Coroutine _displayRoutine;                            // Display elements coroutine reference.
    private struct DamageItem {                                   // Damage item struct data. Used to keep a record of data to show with damage text.
        public float enemyDamage;                                       // Damage caused.
        public bool isCritic;                                           // Whether the damage is critic or not.

        // public construct.
        public DamageItem( float enemyDamage, bool isCritic ) {
            this.enemyDamage = enemyDamage;
            this.isCritic = isCritic;
        }
    };

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        if ( _stack.Count > 0 && _displayRoutine == null ) {
            _displayRoutine = StartCoroutine( DisplayRoutine() );
        }
    }

    /// <summary>
    /// Update damage value.
    /// </summary>
    /// <parma name="enemyDamage">float - enemy damage got to display</param>
    /// <param name="isCritic">bool - wheter the damage got is critic.False by default.</param>
    public void DisplayDamage( float enemyDamage, bool isCritic = false ) {
        DamageItem data = new DamageItem( enemyDamage, isCritic );
        _stack.Add( data );
    }

    /// <summary>
    /// Coroutine to avoid displaying too
    /// much text elements at the same time and overlaping
    /// each other.
    /// </summary>
    /// <param name="damageTextPrefab">GameObject - damage text gameobject instantiated from the object pool.</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator DisplayRoutine() {
        int i = 0;
        while ( i < _stack.Count ) {
            DamageItem data = _stack[i];
            _stack.RemoveAt( i );
            GameObject damageTextPrefab = _pool.SpawnPrefab();

            if ( damageTextPrefab != null ) {
                damageTextPrefab.GetComponent<DamageText>().Display( data.enemyDamage, data.isCritic );
            }

            yield return new WaitForSeconds( toWaitTillNext );
            Debug.Log( "completed" );
        }

        _displayRoutine = null;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get object pool reference to instantiate damage text elements.
        _pool = GetComponent<ObjectPool>();

        // set up _stack list.
        _stack = new List<DamageItem>();
    }
}
