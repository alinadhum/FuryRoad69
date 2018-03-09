using UnityEngine;
using System.Collections;

public class leakingLiquid : MonoBehaviour 
{
    private float leakTimerStart;
    private ParticleSystem leakingLiquidParticle;
    private bool isLeaking;
    private bool isEmtpy;
    private bool leakStarted = true;

    void Start()
    {
        leakingLiquidParticle = GetComponent<ParticleSystem>();
        leakingLiquidParticle.startSpeed = 0f;
        isEmtpy = false;
        isLeaking = true;
    }
	void Update () 
    {
        if(isLeaking && !isEmtpy)
        {
            if(leakStarted)
            {
                leakingLiquidParticle.startSpeed = 1.06f;
                leakStarted = false;
            }

            leakTimerStart -= Time.deltaTime;
            if(leakTimerStart < 2)
            {
                leakingLiquidParticle.startSpeed -= Time.deltaTime / 2;
            }
            if(leakTimerStart <= 0)
            {
                isEmtpy = true;
            }

        }
        else if(isEmtpy)
        {
            leakingLiquidParticle.startSpeed = 0;
            leakingLiquidParticle.emissionRate = 0f;
            isLeaking = false;
        }

	}

    public void leakTimeLeft(float leakTime)
    {
        leakTimerStart = leakTime;
    }


}
