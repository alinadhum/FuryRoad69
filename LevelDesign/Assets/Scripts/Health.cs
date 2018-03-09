using UnityEngine; 
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour { 
	public int maxHealth = 100;
	public Text HealthText;
	public Canvas PlayerDeath;


	public void Update(){
		HealthText.text = "Health: " + maxHealth.ToString ();
		HealthText.color = Color.red;
	}

	public bool Applydamage()
	{
		this.maxHealth = maxHealth - 1;
		if (maxHealth <= 0) {
			PlayerDeath.enabled = true;
			return true;
		} else {
			PlayerDeath.enabled = false;
			return false;
		}
	}
}