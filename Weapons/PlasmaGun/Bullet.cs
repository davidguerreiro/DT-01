using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float damage;                                             // Damage caused by this bullet. This is set from the weapon shooting the bullet.
    public float criticRate;                                         // Damage critic rate. This is set from the weapon shooting the bullet.
    public float maximunDistance = 150f;                             // Maximun distance the bullet can move towards before being restore to the bullet object pool.
    public enum Type {                                               // Bulle type enum.
        standard,                                                    // Standard shoot bullet.
        charged,                                                     // Charged shoot bullet.
    }
    public Type bulletType;                                          // Current bullet type.
    private Vector3 _originalPosition;                               // Original bullet position. Used to restore bullet to weapoin shooting origin after the bullet is destroyed.                  
    private Vector3 _destination;                                    // Where the bullet is shot towards.
    private float _speed;                                            // Movement speed. Defined by weapon.
    private bool _shooted = false;                                   // When true, bullet can move towards destination.
    private GameObject _parent;                                      // Shooting original position parent. This is the gameObject which holds the object pool. 


    // old scale: x: 0.05936146, y:0.02604678, z:0.08106801

    void Awake() {
        Init();
    }

    // Update is called once per frame
    void FixedUpdate() {
        
        if ( _shooted ) {
            SentBullet();
        }

    }

    /// <summary>
    /// Set up bullet parameters.
    /// </summary>
    /// <parma name="destination">Vector3 - bullet destination - it is the point where the player has shot.</param>
    /// <param name="speed">float - movement speed</param>
    /// <param name="player">FPSInput - player input class reference. Used to calculate variations on destination point based on player's speed and direction.</param>
    public void ShootBullet( Vector3 destination, float speed, FPSInput player ) {

        this._destination = destination;
        this._speed = speed;

        transform.parent = null;
        transform.LookAt( _destination );

        _originalPosition = transform.position;

        _shooted = true;
    }


    /// <summary>
    /// Move bullet from the 
    /// weapon from where is shot
    /// to target.
    /// </summary>
    private void SentBullet() {
        float distance;

        transform.position = Vector3.MoveTowards( transform.position, _destination, _speed * Time.deltaTime );

        // check distance to disable bullet if it never collides to any object.
        distance = Vector3.Distance( _originalPosition, transform.position );

        if ( distance >= maximunDistance ) {
            RestoreBullet();
        }
        
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    /*
    void OnTriggerEnter(Collider other) {
        // Debug.Log( LayerMask.LayerToName( other.gameObject.layer) );

        Instantiate( impactParticles, transform.position, Quaternion.identity );
        
        RestoreBullet();
        
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay( Collider other ) {
        RestoreBullet();
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other) {
        RestoreBullet();
    }
    */

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other) {

        // display bullet impact particle effect if neccesary.
        ShootingImpact impact = other.gameObject.GetComponent<ShootingImpact>();

        if ( impact != null ) {
            impact.DisplayImpact( transform.position );
        }
        // old effect.
        /// Instantiate( impactParticles, transform.position, Quaternion.identity );
        
        RestoreBullet();
    }

    /// <summary>
    /// OnCollisionStay is called once per frame for every collider/rigidbody
    /// that is touching rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionStay(Collision other) {
        RestoreBullet();
    }

    /// <summary>
    /// OnCollisionExit is called when this collider/rigidbody has
    /// stopped touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionExit(Collision other) {
        RestoreBullet();
    }

    /// <summary>
    /// Restore bullet to obejct pool
    /// original state.
    /// </sumamry>
    public void RestoreBullet() {
        _shooted = false;

        // reasing to object pool.
        transform.parent = _parent.transform;

        transform.localPosition = Vector3.zero;
        gameObject.SetActive( false );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public virtual void Init() {

        // set original position attribute.
        // _originalPosition = transform.position;

        // get parent gameObject
        _parent = GameObject.FindGameObjectWithTag( "ShootingOrigin" );

    }
}
