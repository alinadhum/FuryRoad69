using UnityEngine;
using System.Collections;

public class gasParticleTimer : MonoBehaviour
{
    private ParticleSystem gasParticle;
    private float duration;

	void Start () 
    {
        gasParticle = GetComponent<ParticleSystem>();
        gasParticle.emissionRate = 50f;
	}

    void Update()
    {
        duration -= Time.deltaTime;

        if(duration < 0)
        {
            gasParticle.emissionRate = 0;
        }
    }

    public void setDuration(float timer)
    {
        duration = timer;
    }
	
}
