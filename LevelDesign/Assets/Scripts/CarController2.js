var rearWheel1 : WheelCollider;
var rearWheel2 : WheelCollider;
var frontWheel1 : WheelCollider;
var frontWheel2 : WheelCollider;
var wheelFL : Transform;
var wheelFR : Transform;
var rightBrakeLight : Light;
var leftBrakeLight : Light;
var steer_max = 30;
var motor_max = 2000;
var brake_max = 15000;
var steerSpeed = 150;
private var steer = 0;
private var forward = 0;
private var back = 0;
private var brakeRelease = false;
private var motor = 0;
private var brake = 0;
private var reverse = false;
private var speed = 0;
var topSpeed = 150;
private var mySidewayFriction : float;
private var myForwardFriction : float;
private var slipSidewayFriction : float;
private var slipForwardFriction : float;
var currentSpeed : int;
var exhaust: ParticleSystem;
var gearRatio : int[];
var SpeedText : UI.Text;

function Start() {
    GetComponent.<Rigidbody>().centerOfMass = Vector3(0, -0.05, 0);
    rightBrakeLight.intensity = 0.5f;
    leftBrakeLight.intensity = 0.5f;
    SetValues();
}

function SetValues (){
    myForwardFriction  = rearWheel2.forwardFriction.stiffness;
    mySidewayFriction  = rearWheel2.sidewaysFriction.stiffness;
    slipForwardFriction = 0.1f;
    slipSidewayFriction = 0.85f;
}

function Update(){
    EngineSound();
    ReverseSlip();
}

function FixedUpdate () {
    speed = GetComponent.<Rigidbody>().velocity.sqrMagnitude;
    currentSpeed = 2*22/7*frontWheel1.radius*frontWheel1.rpm*60/1000;
    SpeedText.text = "Speed: " + currentSpeed.ToString() + " km/h";
    steer = Input.GetAxis("Horizontal2");
    forward = Mathf.Clamp(Input.GetAxis("Vertical2"), 0, 1);
    back = -1 * Mathf.Clamp(Input.GetAxis("Vertical2"), -1, 0);
    if(speed == 0 && forward == 0 && back == 0) {
        brakeRelease = true;
    }
    if (currentSpeed >= topSpeed){
        forward = 0;
    }
    if(speed == 0 && brakeRelease) {
        if(back > 0) { reverse = true; }
        if(forward > 0) {
            reverse = false; 
            exhaust.emissionRate = 200;
        }
        else{
            exhaust.emissionRate = 1;
        }
    }
    if(reverse) {
        motor = -1 * back;
        brake = forward;
    } else {
        motor = forward;
        brake = back;
    }
    if (brake > 0 ) { 
        brakeRelease = false;
        rightBrakeLight.intensity = 1.5f;
        leftBrakeLight.intensity = 1.5f;
    }
    else if (Input.GetButton("Jump2")){
        brake = brake_max;
    }
    else {
        rightBrakeLight.intensity = 0.5f;
        leftBrakeLight.intensity = 0.5f;
    }
    rearWheel1.motorTorque = motor_max * motor;
    rearWheel2.motorTorque = motor_max * motor;
    rearWheel1.brakeTorque = brake_max * brake;
    rearWheel2.brakeTorque = brake_max * brake;
       
    if ( steer == 0 && frontWheel1.steerAngle != 0) {
        if (Mathf.Abs(frontWheel1.steerAngle) <= (steerSpeed * Time.deltaTime)) {
            frontWheel1.steerAngle = 0;
        } else if (frontWheel1.steerAngle > 0) {
            frontWheel1.steerAngle = frontWheel1.steerAngle - (steerSpeed * Time.deltaTime);
        } else {
            frontWheel1.steerAngle = frontWheel1.steerAngle + (steerSpeed * Time.deltaTime);
        }
    } else {
        frontWheel1.steerAngle = frontWheel1.steerAngle + (steer * steerSpeed * Time.deltaTime);
        if (frontWheel1.steerAngle > steer_max) { frontWheel1.steerAngle = steer_max; }
        if (frontWheel1.steerAngle < -1 * steer_max) { frontWheel1.steerAngle = -1 * steer_max; }
    }
    frontWheel2.steerAngle = frontWheel1.steerAngle;
    wheelFL.localEulerAngles.y = frontWheel1.steerAngle;
    wheelFR.localEulerAngles.y = frontWheel2.steerAngle;
}

function EngineSound(){
    for (var i = 0; i < gearRatio.length; i++){
        if(gearRatio[i] >= currentSpeed){
            break;
        }
    }
    var gearMinValue : float = 0.00;
    var gearMaxValue : float = 0.00;
    if (i == 0){
        gearMinValue = 0;
    }
    else {
        gearMinValue = gearRatio[i-1];
    }
    gearMaxValue = gearRatio[i];
    var enginePitch : float = ((currentSpeed - gearMinValue)/(gearMaxValue - gearMinValue) + 1);
    GetComponent.<AudioSource>().pitch = enginePitch;
}

function ReverseSlip(){
    if (currentSpeed < 0){
        SetFrontSlip(slipForwardFriction ,slipSidewayFriction);
    }
    else {
        SetFrontSlip(myForwardFriction ,mySidewayFriction);
    }
}
function SetRearSlip (currentForwardFriction : float,currentSidewayFriction : float){
    rearWheel1.forwardFriction.stiffness = currentForwardFriction;
    rearWheel2.forwardFriction.stiffness = currentForwardFriction;
    rearWheel1.sidewaysFriction.stiffness = currentSidewayFriction;
    rearWheel2.sidewaysFriction.stiffness = currentSidewayFriction;
}
    function SetFrontSlip (currentForwardFriction : float,currentSidewayFriction : float){
        frontWheel1.forwardFriction.stiffness = currentForwardFriction;
        frontWheel2.forwardFriction.stiffness = currentForwardFriction;
        frontWheel1.sidewaysFriction.stiffness = currentSidewayFriction;
        frontWheel2.sidewaysFriction.stiffness = currentSidewayFriction;
    }