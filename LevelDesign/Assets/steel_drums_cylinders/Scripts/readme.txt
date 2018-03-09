If you have any questions or issues with this pack, please email me: conorlang810@gmail.com
www.conorlang3dart.com


****************
Script Overview
****************

cylinderTank - goes on the main gas cylinder, has addforce and instantiation of particle effects when hit.

cylinderValveCover - sets the cover as non kinematic and the collider trigger to false when hit

damageInterface - Implement these interfaces on the objects you want to be damaged by the different damage types. 
 
To use the interface, use GetComponent with the non generic interface: IDamageable damageInterface = hit.collider.GetComponent(typeof(IDamageable)) as IDamageable
then use the corresponding function calls to set the damage amount and when the specific collider is hit with corresponding hit bool

	IDamageable - implement this to take damage my raycast -send hitpoint damage with "hitDamage(float damageAmount)" and set hit as true with "isHit(bool hit)"

	IDamageableRay - implement this to send raycastHit - send raycastHit with "rayHitInfo(RaycastHit hit)"
	
	IDamageableByExplosion - implement this for explosive damage - send hitpoint damage with "explosionDamageAmount(float damageAmount)" and set hit as true with "isHitByExplosion(bool hit)"

	IDamageableByPosion - implement htis for posion damage - send hitpoint damage with "posionDamageAmount(float damageAmount)" and set hit as true with with "isHitByPosion(bool hit)"

darkSmokeTimer - turn off dark smoke emission rate after set time

destroyObjectTimer - destroy objects after set time

explodingDrum - Implements IDamageable, IDamageableRay, IDamageableByExplosion, destroys main object on explosion and instantiates the destroyed drum prefabs, along with particle effect instantiations

fireWeapon - uses functions in IDamageable and IDamageableRay interfaces to damage any gameObject that implements those interfaces, also Instantiates bullet holes and bullet impact particle effects

gasParticleTimer - turns of gas particle after set time

leakingDrum - implements IDamageable and IDamageableRay, instantiates leaking_liquid particle prefab with the amount of time left on the leakTimer which starts to countdown on the first hit

leakingLiquid - turns of leaking_liquid prefab once timer reaches zero

playerHealth - implements IDamageable, IDamageableByExplosion and IDamageableByPosion.  If hitpoints reach zero it sets timeScale to zero and enables the player death canvas to restart the demo scene.  Also controls the player health UI

posionParticleDamage - uses functions in IDamageableByPosion, sets damage and hit bool on each OnParticleCollision()


