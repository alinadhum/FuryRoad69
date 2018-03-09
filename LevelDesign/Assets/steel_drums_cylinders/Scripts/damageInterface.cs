using UnityEngine;
using System.Collections;

public interface IDamageable
{
    void isHit(bool hit);
    void hitDamage(float damageAmount);
}

public interface IDamageableRay
{
    void rayHitInfo(RaycastHit hit);
}

public interface IDamageableByExplosion
{
    void isHitByExplosion(bool hit);
    void explosionDamageAmount(float damageAmount);
}

public interface IDamageableByPosion
{
    void isHitByPosion(bool hit);
    void posionDamageAmount(float damageAmount);
}