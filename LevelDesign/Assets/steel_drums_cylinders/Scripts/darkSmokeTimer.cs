using UnityEngine;
using System.Collections;

public class darkSmokeTimer : MonoBehaviour 
{
    private ParticleSystem darkSmoke;
    private float smokeTimer = 16f;

	void Start () 
    {
        darkSmoke = GetComponent<ParticleSystem>();
        darkSmoke.gravityModifier = 0f;
        darkSmoke.emissionRate = 0f;
        darkSmoke.startSpeed = 0f;
        darkSmoke.startSize = 0f;
	}
	
	void Update () 
    {
        smokeTimer -= Time.deltaTime;

        if(smokeTimer > 14f)
        {
            darkSmoke.gravityModifier -= Time.deltaTime / 15;
            darkSmoke.emissionRate += Time.deltaTime*30;
            darkSmoke.startSpeed += Time.deltaTime;
            darkSmoke.startSize += Time.deltaTime*2;
        }
        else if(smokeTimer <= 14f && smokeTimer > 5)
        {
            darkSmoke.emissionRate -= Time.deltaTime * 3;
            darkSmoke.startSize += Time.deltaTime / 6;
        }
        else
        {
            darkSmoke.emissionRate = 0f;
        }
        //Debug.Log(smokeTimer);
	}
}
