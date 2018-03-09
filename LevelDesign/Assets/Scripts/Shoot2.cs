using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shoot2 : MonoBehaviour {
	public GameObject bulletHole;
	public GameObject bulletImpact;
	public AudioClip gunsound;
	public Canvas playerWonCanvas;
	public GameObject muzzleflash;
	public float shootDistance;
	public Health enemyHealth;
	public Text enemyHealthText;
	public bool destroyed;
	public AudioClip explosionsound;
	public GameObject currentDetonator;
	public GameObject[] detonatorPrefabs;
	public float explosionLife = 10;
	public float timeScale = 1.0f;
	public float detailLevel = 1.0f;
	public bool EnemyHit;


	// Use this for initialization
	void Start()
	{
		muzzleflash.SetActive(false);
		playerWonCanvas.enabled = false;
		enemyHealthText.color = Color.blue;
		enemyHealthText.text = "Enemy Health: " + enemyHealth.maxHealth.ToString ();
	}

	void FixedUpdate(){
		EnemyHit = false;
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetButton("Fire2") || Input.GetKey(KeyCode.Space))
		{
			RaycastHit hit;
			Ray bullet = new Ray(transform.position, transform.forward);
			GetComponent<AudioSource>().PlayOneShot(gunsound);
			if (Physics.Raycast(bullet, out hit, shootDistance))	
			{	
				if (hit.transform.name == "Car1(Black)" || hit.collider.tag == "Desert")	
				{
					Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
					if (bulletImpact.gameObject != null) {
 						Instantiate (bulletImpact, (hit.point + (hit.normal * 0.0001f)), Quaternion.LookRotation (hit.normal));
					}
				}

				if (hit.transform.name == "Car1(Black)")
				{
					enemyHealthText.text = "Enemy Health: " + enemyHealth.maxHealth.ToString ();
					destroyed = enemyHealth.Applydamage();
					if (destroyed == true) {
						Destroy(hit.collider.transform.root.gameObject); 
						Detonator dTemp = (Detonator)currentDetonator.GetComponent("Detonator");
						float offsetSize = dTemp.size/3;
						Vector3 hitPoint = hit.point +
							((Vector3.Scale(hit.normal, new Vector3(offsetSize, offsetSize, offsetSize))));
						GameObject exp = (GameObject) Instantiate(currentDetonator, hitPoint, Quaternion.identity);
						dTemp = (Detonator)exp.GetComponent("Detonator");
						dTemp.detail = detailLevel;
						GetComponent<AudioSource>().PlayOneShot(explosionsound);
						Destroy(exp, explosionLife);
						playerWonCanvas.enabled = true;
						enemyHealth.PlayerDeath.enabled = false;
					}
					EnemyHit = true;
				}

			}
			muzzleflash.SetActive(true);
		}
		else
		{
			muzzleflash.SetActive(false);
		}
	}
}
