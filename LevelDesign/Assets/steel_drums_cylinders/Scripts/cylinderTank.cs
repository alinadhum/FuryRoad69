using UnityEngine;
using System.Collections;

public class cylinderTank : MonoBehaviour, IDamageable, IDamageableRay
{
    public ParticleSystem gasParticle;
    public GameObject valveCover;
    private Rigidbody tankRigid;
    private Rigidbody coverRigid;
    private Collider coverCollider;
    private RaycastHit hitInfo;
    private bool hasBeenHit = false;
    private bool enableForce = false;
    private bool tankIsLeaking = false;
    private float leakTime = 12f;
    private bool tankEmpty = false;

    void Start()
    {
        tankRigid = GetComponent<Rigidbody>();
        coverCollider = valveCover.GetComponent<Collider>();
        coverRigid = valveCover.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (hasBeenHit && !tankEmpty)
        {
            coverRigid.isKinematic = false;
            coverCollider.isTrigger = false;
            valveCover.transform.parent = null;
            hasBeenHit = false;
            enableForce = true;
            tankIsLeaking = true;
            ParticleSystem gasParticleInstance = Instantiate(gasParticle, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as ParticleSystem;
            gasParticleInstance.transform.parent = transform;
            gasParticleInstance.GetComponent<gasParticleTimer>().setDuration(leakTime);
        }
        
        if(tankIsLeaking && !tankEmpty)
        {
            leakTime -= Time.deltaTime;
        }

        if(leakTime < 0)
        {
            tankEmpty = true;
            tankIsLeaking = false;
        }
        //Debug.Log("tank timer" + leakTime);
    }

    void FixedUpdate()
    {
        if (enableForce && tankIsLeaking)
        {
            //tankRigid.AddForceAtPosition((-hitInfo.normal) * 30, hitInfo.point);
            tankRigid.AddForce(-hitInfo.normal * 18);
            tankRigid.AddRelativeTorque(15,0,0);
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
        // not used
    }
    
}
