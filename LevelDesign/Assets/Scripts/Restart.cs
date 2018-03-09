using UnityEngine;
using System.Collections;
public class Restart : MonoBehaviour {
	
	public void RestartGame(string name) {
		Application.LoadLevel (name);
	}

	public void QuitGame(){
		Application.Quit ();
	}

}
