using UnityEngine;
using System.Collections;

public class explodingDrum : MonoBehaviour, IDamageable, IDamageableRay, IDamageableByExplosion
{
    public ParticleSystem smallFlame;
    public ParticleSystem explosion;
    public GameObject destroyedDrumTop;
    public GameObject destroyedDrumBottom;
    public float explodePower = 30f;
    public float explodeRadius = 10f;
    public float explodeDamage = 40f;

    private bool hasBeenHitByExplosion = false;
    private float explosionHitDamage;
    private bool explode = false;
    private bool hasBeenHit = false;
    private bool fireStarted = false;
    private bool explodeOnce = true;
    private float hitPoints = 80f;
    private float hitDamageAmount;
    private RaycastHit hitInfo;

    void Start()
    {

    }

    void Update()
    {
        if(hasBeenHit)
        {
            hasBeenHit = false;
            fireStarted = true;
            hitPoints -= hitDamageAmount;
            ParticleSystem smallFlameInstance = Instantiate(smallFlame, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as ParticleSystem;
            smallFlameInstance.transform.parent = hitInfo.transform;
        }

        if(hasBeenHitByExplosion)
        {
            hasBeenHitByExplosion = false;
            hitPoints -= explosionHitDamage;
        }

        if(fireStarted)
        {
            hitPoints -= Time.deltaTime * 4;
        }

        if(hitPoints <= 0)
        {
            explode = true;
        }
    }

    void FixedUpdate()
    {
        if(explode && explodeOnce)
        {
            explodeOnce = false;
            Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(destroyedDrumBottom, transform.position, Quaternion.identity);
            Instantiate(destroyedDrumTop, transform.position, Quaternion.identity);

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explodeRadius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                Collider col = hit.GetComponent<Collider>();

                if (rb != null)
                    rb.AddExplosionForce(explodePower, explosionPos, explodeRadius, 1.8f, ForceMode.Impulse);
                
                if(col.gameObject.tag == "damageable")
                {
                    IDamageableByExplosion explosionDamageInterface = rb.gameObject.GetComponent(typeof(IDamageableByExplosion)) as IDamageableByExplosion;
                    if(explosionDamageInterface != null)
                    {
                        explosionDamageInterface.explosionDamageAmount(explodeDamage);
                        explosionDamageInterface.isHitByExplosion(true);
                    }
                }
            }

            Destroy(gameObject, 0.01f);
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
        hitDamageAmount = damageAmount;
    }

    public void isHitByExplosion(bool hit)
    {
        hasBeenHitByExplosion = hit;
    }

    public void explosionDamageAmount(float damage)
    {
        explosionHitDamage = damage;
    }
}
