using UnityEngine;
using System.Collections;
[RequireComponent(typeof(WheelCollider))]
public class Wheel : MonoBehaviour
{
    [SerializeField]Transform tire;
    private WheelCollider wheelcollider;

	// Use this for initialization
    void Awake()
    {

        wheelcollider = GetComponent<WheelCollider>();
    
    }

    public void Move(float power)
    {
        wheelcollider.motorTorque = power;
    }

    public void Turn(float turning)
    {
       wheelcollider.steerAngle = turning;
        tire.localEulerAngles = new Vector3(0f, wheelcollider.steerAngle, 0);
    }
}
