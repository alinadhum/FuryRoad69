using UnityEngine;
using System.Collections;

public class fireWeapon : MonoBehaviour 
{
    public GameObject metalBulletHole;
    public ParticleSystem bulletImpact;
    public float bulletDamage = 30f;

    private bool primaryFire = false;
    private float fireRate = 1f;
    private bool hitRigidbody;
    private Camera mainCamera;
    private RaycastHit hit;

	void Start ()
    {
        mainCamera = GetComponent<Camera>();
	}
	
	void Update () 
    {
        primaryFire = Input.GetMouseButton(0);

        if(primaryFire && fireRate <= 0)
        {
            fireRate = 1;
			Ray ray = GameObject.Find("Camera").GetComponent<Camera> ().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out hit, 3000f))
            {
                Instantiate(bulletImpact, (hit.point + (hit.normal * 0.0001f)), Quaternion.LookRotation(hit.normal));

                if (hit.rigidbody)
                {
                    hitRigidbody = true;
                }

                if (hit.collider.gameObject.tag != "gasValve" && hit.collider.gameObject.tag != "explosionDebris")
                {
                    GameObject bulletHoleInstance = Instantiate(metalBulletHole, (hit.point + (hit.normal * 0.0001f)), Quaternion.LookRotation(hit.normal)) as GameObject;
                    bulletHoleInstance.transform.parent = hit.transform;
                }

                if(hit.collider.gameObject.tag == "damageable" || hit.collider.gameObject.tag == "gasValve")
                {
                    IDamageable damageInterface = hit.collider.GetComponent(typeof(IDamageable)) as IDamageable;
                    IDamageableRay damageRay = hit.collider.GetComponent(typeof(IDamageableRay)) as IDamageableRay;
                    if(damageInterface != null)
                    {
                        damageInterface.hitDamage(bulletDamage);
                        damageInterface.isHit(true);
                    }
                    if(damageRay != null)
                    {
                        damageRay.rayHitInfo(hit);
                    }
                }
            }
        }

        if(fireRate > -1)
        {
            fireRate -= Time.deltaTime * 4;
        }
	}

    void FixedUpdate()
    {
        if(hitRigidbody)
        {
            hitRigidbody = false;
            hit.rigidbody.AddForceAtPosition(-hit.normal * 70, hit.point);
        }
    }
}
