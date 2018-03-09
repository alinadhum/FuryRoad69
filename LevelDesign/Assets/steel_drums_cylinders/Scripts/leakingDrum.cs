using UnityEngine;
using System.Collections;

public class leakingDrum : MonoBehaviour, IDamageable, IDamageableRay
{
    public ParticleSystem leakingLiquid;
    private bool hasBeenHit = false;
    private bool isEmpty = false;
    private bool isLeaking = false;
    private float leakTimer = 6f;
    private RaycastHit hitInfo;

	void Start () 
    {
	
	}
	
	void Update () 
    {
	    if(hasBeenHit)
        {
            isLeaking = true;
            hasBeenHit = false;

            if (!isEmpty)
            {
                ParticleSystem leakingLiquidInstance = Instantiate(leakingLiquid, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as ParticleSystem;
                leakingLiquidInstance.transform.parent = hitInfo.transform;
                leakingLiquidInstance.GetComponent<leakingLiquid>().leakTimeLeft(leakTimer);
            }
        }
        if(isLeaking && leakTimer > 0)
        {
            leakTimer -= Time.deltaTime;
        }
        else if(leakTimer < 0)
        {
            isEmpty = true;
            leakTimer = 0;
        }
        //Debug.Log(leakTimer);
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

    }
}
