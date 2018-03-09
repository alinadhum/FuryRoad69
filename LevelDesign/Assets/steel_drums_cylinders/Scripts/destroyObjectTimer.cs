using UnityEngine;
using System.Collections;

public class destroyObjectTimer : MonoBehaviour
{
    public float destroyTimer = 2f;
    void Start () 
    {
        Destroy(gameObject, destroyTimer);
	}
	
}
