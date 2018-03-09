using UnityEngine;
using System.Collections;

public class CrashSound : MonoBehaviour {

	public AudioClip crashSoft;
	public AudioClip crashHard;
    public GameObject spark;

	private AudioSource source;
	private float lowPitchRange = .75F;
	private float highPitchRange = 1.5F;
	private float velToVol = .2F;
	private float velocityClipSplit = 10F;
   
    void Awake () {

		source = GetComponent<AudioSource>();
	}


    void OnCollisionEnter (Collision coll)
	{
		source.pitch = Random.Range (lowPitchRange,highPitchRange);
		float hitVol = coll.relativeVelocity.magnitude * velToVol;
        if (coll.transform != transform && coll.contacts.Length != 0) {
            for(int i = 0; i < coll.contacts.Length; i++) {
                Instantiate(spark, coll.contacts[i].point, Quaternion.identity);
            }
            if (coll.relativeVelocity.magnitude < velocityClipSplit){
                source.PlayOneShot(crashSoft, hitVol);
            }
            else {
                source.PlayOneShot(crashHard, hitVol);
            }
        }
			
	}

}
