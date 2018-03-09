using UnityEngine;
using System.Collections;

public class cylinderValveCover : MonoBehaviour, IDamageable, IDamageableRay
{
    private Collider coverCollider;
    private Rigidbody coverRigid;
    private RaycastHit hitInfo;
    private bool hasBeenHit = false;
    private bool applyForceOnce = true;

	void Start ()
    {
        coverCollider = GetComponent<Collider>();
        coverRigid = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
	    if(hasBeenHit)
        {
            hasBeenHit = false;
            coverCollider.isTrigger = false;
            coverRigid.isKinematic = false;
            if(applyForceOnce)
            {
                applyForceOnce = false;
                coverRigid.AddForce((hitInfo.normal * 2f), ForceMode.Impulse);
                transform.parent = null;
            }
        }
	}

    public void rayHitInfo(RaycastHit hit)
    {
        hitInfo = hit;
    }

    public void isHit(bool hit)
    {
        hasBeenHit = hit;
    }

    public void hitDamage(float damageAmount)
    {
       //not used
    }

}
