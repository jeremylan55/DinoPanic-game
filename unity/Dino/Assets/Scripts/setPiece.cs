using UnityEngine;
using System.Collections;

public class setPiece : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ  
		| RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
