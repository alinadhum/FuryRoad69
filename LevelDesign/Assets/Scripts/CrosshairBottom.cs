using UnityEngine;
using System.Collections;

public class CrosshairBottom : MonoBehaviour {

	public Texture2D GreenCrosshairImage;
	public Texture2D RedCrosshairImage;
	public Texture2D EnemyHitCrosshairImage;
	Shoot controller;
	public bool IsEnemy;
	Vector2 m_WindowSize;    //More like "last known window size".
	Rect m_CrosshairRect;

	void Awake(){
		controller = GameObject.Find("MuzzleSpawns_black").GetComponent<Shoot> ();
		EnemyHitCrosshairImage = Resources.Load ("Textures/targetHit") as Texture2D;
		RedCrosshairImage = Resources.Load ("Textures/targetRed") as Texture2D;
		GreenCrosshairImage = Resources.Load ("Textures/targetGreen") as Texture2D;
		m_WindowSize = new Vector2(Screen.width, Screen.height);
		CalculateRect();
	}

	void CalculateRect()
	{
		float crosshairSize = Screen.width*0.02f;
		m_CrosshairRect =  new Rect(Screen.width/2 - 15,
			Screen.height/2 + crosshairSize * 1.6f,
			crosshairSize, crosshairSize);
	}

	void Update () {
		if(m_WindowSize.x != Screen.width || m_WindowSize.y != Screen.height)
		{
			CalculateRect();
		}
	}

	void FixedUpdate() {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward,out hit)) {
			IsEnemy = (hit.transform.name == "Car2(Red)");
		}
	}

	void OnGUI()
	{
		if (IsEnemy && !controller.EnemyHit) {
			GUI.DrawTexture (m_CrosshairRect, RedCrosshairImage);

		} else if(controller.EnemyHit || (IsEnemy && controller.EnemyHit)) {
			GUI.DrawTexture (m_CrosshairRect, EnemyHitCrosshairImage);			
		}
		else{
			GUI.DrawTexture (m_CrosshairRect, GreenCrosshairImage);
		}
			
	}
}
