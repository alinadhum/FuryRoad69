using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour, IDamageable, IDamageableByExplosion, IDamageableByPosion
{
    public float hitPoints = 200f;
    private float maxHitPoints;
    public RectTransform healthBarRect;
    public Canvas playerDeath;

    private float healthBarPos = 0f;
    private bool hasBeenHit = false;
    private float hitDamageAmount;
    private bool hasBeenHitByExplosion = false;
    private float hitExplosionDamage;
    private float hitPosionDamage;
    private bool hasBeenHitByPosion = false;
    private float posiontimer = 0f;

    void Awake()
    {
        Time.timeScale = 1.0f;
        maxHitPoints = hitPoints;
        playerDeath.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

	void Update ()
    {
        if(hasBeenHit)
        {
            hasBeenHit = false;
            hitPoints -= hitDamageAmount;
        }

        if(hasBeenHitByExplosion)
        {
            hasBeenHitByExplosion = false;
            hitPoints -= hitExplosionDamage;
        }

        if(hasBeenHitByPosion)
        {
            posiontimer = 5;
            hasBeenHitByPosion = false;
        }

        
        if(posiontimer > 0)
        {
            hitPoints -= hitPosionDamage;
            posiontimer -= Time.deltaTime;
        }
	
        healthBarPos = (healthBarRect.rect.width * (hitPoints/maxHitPoints)) - healthBarRect.rect.width;
        healthBarRect.transform.localPosition = new Vector3(healthBarPos,0,0);

        if(hitPoints < 0)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerDeath.enabled = true;
            gameObject.SetActive(false);
        }
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

    public void explosionDamageAmount(float damageAmount)
    {
        hitExplosionDamage = damageAmount;
    }

    public void isHitByPosion(bool hit)
    {
        hasBeenHitByPosion = hit;
    }

    public void posionDamageAmount(float damageAmount)
    {
        hitPosionDamage = damageAmount;
    }

    public void restartOnDeath()
    {
        Application.LoadLevel(0);
    }
}
