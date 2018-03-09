using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickUp : MonoBehaviour {

	public float sec = 10f;
	public GameObject CrazySparks;
	public GameObject Chunks;
	public GameObject Spray;
	public GameObject Multi;
	//Default
	public GameObject BulletSmoke;
	Shoot shootControl;
	Collider collidedItem;
	public AudioClip PickUpSound;
	public Text CurrentWeaponText;

	void Start(){
		CrazySparks = Resources.Load ("PickUpItems/Detonator-Crazysparks") as GameObject;
		Chunks = Resources.Load ("PickUpItems/Detonator-Chunks") as GameObject;
		Spray = Resources.Load ("PickUpItems/Detonator-Spray") as GameObject;
		Multi = Resources.Load ("PickUpItems/Detonator-MultiExample") as GameObject;
		BulletSmoke = Resources.Load ("PickUpItems/bullet_impact_smoke") as GameObject;
		GameObject.Find ("MuzzleSpawns_black").GetComponent<Shoot> ().bulletImpact = BulletSmoke;
		CurrentWeaponText.text = "CurrentWeapon: Default Bullets";
	}
		
	void OnTriggerEnter(Collider other) 
	{
		collidedItem = other;
		if (other.gameObject.CompareTag ("CrazySparksItem")) {
			GameObject.Find ("MuzzleSpawns_black").GetComponent<Shoot> ().bulletImpact = CrazySparks;
			other.gameObject.SetActive (false);
			StartCoroutine (LateCall ());
			GetComponent<AudioSource>().PlayOneShot(PickUpSound);
			CurrentWeaponText.text = "CurrentWeaon: CrazySparks";

		} else if (other.gameObject.CompareTag ("ChunksItem")) {
			GameObject.Find ("MuzzleSpawns_black").GetComponent<Shoot> ().bulletImpact = Chunks;
			other.gameObject.SetActive (false);
			StartCoroutine (LateCall ());
			GetComponent<AudioSource>().PlayOneShot(PickUpSound);
			CurrentWeaponText.text = "CurrentWeaon: Chunks";

		} else if (other.gameObject.CompareTag ("SprayItem")) {
			GameObject.Find ("MuzzleSpawns_black").GetComponent<Shoot> ().bulletImpact = Spray;
			other.gameObject.SetActive (false);
			StartCoroutine (LateCall ());
			GetComponent<AudioSource>().PlayOneShot(PickUpSound);
			CurrentWeaponText.text = "CurrentWeaon: Spray Bombs";

		} else if (other.gameObject.CompareTag ("MultiItem")) {
			GameObject.Find ("MuzzleSpawns_black").GetComponent<Shoot> ().bulletImpact = Multi;
			other.gameObject.SetActive (false);
			StartCoroutine (LateCall ());
			GetComponent<AudioSource>().PlayOneShot(PickUpSound);
			CurrentWeaponText.text = "CurrentWeaon: MultiExplosions";
		}
	}

	IEnumerator LateCall(){
		yield return new WaitForSeconds (sec);
		collidedItem.gameObject.SetActive (true);
	}
}