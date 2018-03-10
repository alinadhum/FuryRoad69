#pragma strict

private var wheelMesh : Transform;
private var wheelCollider : WheelCollider;
var currentWl : String;

function Start () {

wheelCollider = gameObject.GetComponent(WheelCollider);

if (this.gameObject.name == "FLCollider") {currentWl = "FrontLeft";}
else if (this.gameObject.name == "FRCollider") {currentWl = "FrontRight";}
else if (this.gameObject.name == "BLCollider") {currentWl = "RB&LB";}
else if (this.gameObject.name == "BRCollider") {currentWl = "RB&LB";}

wheelMesh = GameObject.Find(currentWl).transform;
}

function Update () {

	var hit : RaycastHit;
	
	if (Physics.Raycast(wheelCollider.transform.position, -wheelCollider.transform.up, hit, wheelCollider.suspensionDistance + wheelCollider.radius)) {
		wheelMesh.position = hit.point + wheelCollider.transform.up * wheelCollider.radius;
	}

	else {
		wheelMesh.position = wheelCollider.transform.position - (wheelCollider.transform.up * wheelCollider.suspensionDistance);
	}
	
		wheelMesh.transform.Rotate(wheelCollider.rpm/60*360*Time.deltaTime*-1,0,0);
		wheelMesh.localEulerAngles.y = wheelCollider.steerAngle - wheelMesh.localEulerAngles.z;
}