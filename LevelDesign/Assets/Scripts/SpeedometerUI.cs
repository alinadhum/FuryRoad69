using UnityEngine;
using System.Collections;

public class SpeedometerUI : MonoBehaviour {
	public Texture2D dialTex;
	public Texture2D needleTex;
	public Vector2 dialPos;
	public float topSpeed=0;
	public float stopAngle=0;
	public float topSpeedAngle=0;
	public float speed=0;


	void  OnGUI (){
		GUI.DrawTexture( new Rect(dialPos.x, dialPos.y, dialTex.width, dialTex.height), dialTex);
		Vector2 centre= new Vector2((dialPos.x + dialTex.width) / 2, (dialPos.y + dialTex.height) / 2);
		Matrix4x4 savedMatrix= GUI.matrix;
		float speedFraction= speed / topSpeed;
		float needleAngle= Mathf.Lerp(stopAngle, topSpeedAngle, speedFraction);
		GUIUtility.RotateAroundPivot(needleAngle, centre);
		GUI.DrawTexture( new Rect(centre.x, centre.y - needleTex.height / 2, needleTex.width, needleTex.height), needleTex);
		GUI.matrix = savedMatrix;
	}
}
