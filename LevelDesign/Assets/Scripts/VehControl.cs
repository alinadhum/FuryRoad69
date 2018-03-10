using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class VehControl : MonoBehaviour
{

    public int bhp;
    public float torque;
    public int brakeTorque;

    public float[] gearRatio;
    public int currentGear;

    public WheelCollider FL;
    public WheelCollider FR;
    public WheelCollider RL;
    public WheelCollider RR;

    public float currentSpeed;
    public int maxSpeed;
    public int maxRevSpeed;

    public float SteerAngle;

    public float engineRPM;
    public float gearUpRPM;
    public float gearDownRPM;

    private GameObject COM;
	private float mySidewayFriction;
	private float myForwardFriction;
	private float slipSidewayFriction;
	private float slipForwardFriction;

	public bool handBraked;
	public float soundRPM;
	public float[] MinRpmTable = {500, 750, 1120, 1669, 2224, 2783, 3335, 3882, 4355, 4833, 5384, 5943, 6436, 6928, 7419, 7900};
	public float[] NormalRpmTable = {720, 930, 1559, 2028, 2670, 3145, 3774, 4239, 4721, 5194, 5823, 6313, 6808, 7294, 7788, 8261};
	public float[] MaxRpmTable = {920, 1360, 1829, 2474, 2943, 3575, 4036, 4525, 4993, 5625, 6123, 6616, 7088, 7589, 8060, 10000};
	public float[] PitchingTable = {0.12f, 0.12f, 0.12f, 0.12f, 0.11f, 0.10f, 0.09f, 0.08f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f, 0.06f};
	public float RangeDivider = 4f;

	public Text SpeedText;
	public ParticleSystem exhaust;
	private Light BL_light;
	private Light BR_light;
	private float topSpeed = 120; 
	private float pitch = 0;
	public List<AudioSource> CarSound;

    void Start()
    {

        FL.GetComponent<WheelCollider>();
        FR.GetComponent<WheelCollider>();
        RL.GetComponent<WheelCollider>();
        RR.GetComponent<WheelCollider>();

		COM = GameObject.Find("CenterOfGravity");
        GetComponent<Rigidbody>().centerOfMass = new Vector3(COM.transform.localPosition.x * transform.localScale.x, COM.transform.localPosition.y * transform.localScale.y, COM.transform.localPosition.z * transform.localScale.z);		            
		BL_light = GameObject.Find ("LeftLight(back2)").GetComponent<Light> ();
		BR_light = GameObject.Find ("RightLight(back2)").GetComponent<Light> ();
		BL_light.intensity = 0.5f;
		BR_light.intensity = 0.5f;

		for(int i =1; i<=16; ++i) 
		{
			CarSound.Add(GameObject.Find(string.Format("CarSound ({0})",i)).GetComponent<AudioSource>());
			CarSound[i-1].Play();
		}

    }
	

    void Update()
    {
		//Functions to access.
        Steer();
		AutoGears();
		Accelerate();
		carSounds ();

		//Defenitions.
        currentSpeed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
		engineRPM = Mathf.Round((FL.rpm * gearRatio[currentGear]));
        torque = bhp * gearRatio[currentGear];

		if (Math.Round (currentSpeed) >= 0) {
			SpeedText.text = "Speed: " + Math.Round (currentSpeed).ToString () + " km/h";
		}
		if (Math.Round (currentSpeed) <= 0) {
			exhaust.emissionRate = 1;
		} else {
			exhaust.emissionRate = 200;
		}

		if (Input.GetButton ("Jump")) {
			handBraked = true;
			HandBrakes ();
		} else {
			handBraked = false;
		}
		
		if (Input.GetKey(KeyCode.R)) {
			transform.position.Set(transform.position.x, transform.position.y + 5f, transform.position.z);
			transform.rotation.Set(0,0,0,0);
		}
    }


	void ToggleBrakeLights(bool brake){
		if (brake) {
			BL_light.intensity = 1.5f;
			BR_light.intensity = 1.5f;
		} else {
			BL_light.intensity = 0.5f;
			BR_light.intensity = 0.5f;
		}
	}
	//Function
    void Accelerate()
    {

        if (currentSpeed < maxSpeed && currentSpeed > maxRevSpeed && engineRPM <= gearUpRPM)
        {
            RL.motorTorque = torque * Input.GetAxis("Vertical");
            RR.motorTorque = torque * Input.GetAxis("Vertical");
            RL.brakeTorque = 0;
            RR.brakeTorque = 0;
			ToggleBrakeLights (false);
        }
        else
        {
            RL.motorTorque = 0;
            RR.motorTorque = 0;
            RL.brakeTorque = brakeTorque;
            RR.brakeTorque = brakeTorque;
			ToggleBrakeLights (true);
        }

		if (engineRPM > 0 && Input.GetAxis("Vertical") < 0 && engineRPM <= gearUpRPM)
		{
            FL.brakeTorque = brakeTorque;
            FR.brakeTorque = brakeTorque;
			ToggleBrakeLights (true);
        }
        else
        {
            FL.brakeTorque = 0;
            FR.brakeTorque = 0;
			ToggleBrakeLights (false);
        }
    }

    void Steer()
    {
        if (currentSpeed < 100)
        {
            SteerAngle = 13 - (currentSpeed / 10);
        }
        else
        {
            SteerAngle = 2;
        }

        FL.steerAngle = SteerAngle * Input.GetAxis("Horizontal");
        FR.steerAngle = SteerAngle * Input.GetAxis("Horizontal");
    }


    void AutoGears()
    {

        int AppropriateGear = currentGear;

        if (engineRPM >= gearUpRPM)
        {
            for (var i = 0; i < gearRatio.Length; i++)
            {
                if (RL.rpm * gearRatio[i] < gearUpRPM)
                {
                    AppropriateGear = i;
                    break;
                }
            }
            currentGear = AppropriateGear;
        }

        if (engineRPM <= gearDownRPM)
        {
            AppropriateGear = currentGear;
            for (var j = gearRatio.Length - 1; j >= 0; j--)
            {
                if (RL.rpm * gearRatio[j] > gearDownRPM)
                {
                    AppropriateGear = j;
                    break;
                }
            }
            currentGear = AppropriateGear;
        }
    }

    void HandBrakes()
    {
        RL.brakeTorque = brakeTorque;
        RR.brakeTorque = brakeTorque;
        FL.brakeTorque = brakeTorque;
        FR.brakeTorque = brakeTorque;
    }

	void carSounds()
	{
		for (int i = 0; i < CarSound.Count; i++) {
			if (RL.rpm < MinRpmTable [i]) {
				CarSound[i].volume = 0.0f;
			} else if (RL.rpm >= MinRpmTable [i] && RL.rpm < NormalRpmTable [i]) {
				float Range = NormalRpmTable [i] - MinRpmTable [i];
				float ReducedRPM = RL.rpm - MinRpmTable [i];
				CarSound[i].volume = ReducedRPM / Range;
			} else if (RL.rpm >= NormalRpmTable [i] && RL.rpm <= MaxRpmTable [i]) {
				float Range = MaxRpmTable [i] - NormalRpmTable [i];
				float ReducedRPM = RL.rpm - NormalRpmTable [i];
				CarSound[i].volume = 1f;
				float PitchMath = (ReducedRPM * PitchingTable [i]) / Range;
				CarSound[i].pitch = 1f + PitchMath;
			} else if (RL.rpm > MaxRpmTable [i]) {
				float Range = (MaxRpmTable [i + 1] - MaxRpmTable [i]) / RangeDivider;
				float ReducedRPM = RL.rpm - MaxRpmTable [i];
				CarSound[i].volume = 1f - ReducedRPM / Range;
				float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
				CarSound[i].pitch = 1f + PitchingTable[i] + PitchMath;
			}
		} 
	}
}