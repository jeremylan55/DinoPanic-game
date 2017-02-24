using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	private float speed = 40f;
	private float direction = -1f; // -1 => Right, 1 => Left
	private bool moving = false;
	private bool alive = true;
	private GameObject skeleton;
	private GameObject dino;

	private IEnumerator coroutine;

	//public float rotationSpeed = 180f;
	//private float rotation = 180f;
	//private float duration = 50f;
	//private bool spinning = false;

	// Use this for initialization
	void Start () {
		coroutine = startMoving();
		StartCoroutine(coroutine);

		dino = this.gameObject.transform.GetChild (0).gameObject;
		skeleton = this.gameObject.transform.GetChild (1).gameObject;
		skeleton.SetActive (false);

		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY  
			| RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
	}
	
	// Update is called once per frame
	void Update () {
		if (alive) {
			//float degreePerFrame = rotation / (duration / Time.deltaTime);
			if (moving) {
				float velocity = speed * direction;
				transform.Translate (velocity * Time.deltaTime, 0, 0);
			}
			if (Input.GetKeyDown ("h"))
				spin ();
		}
	}

	void OnTriggerEnter(Collider target) {
		if ( target.tag == "boundLeft" || target.tag == "boundRight") {
			spin ();
		}

		if (target.tag == "Meteor") {
			GameObject temp = GameObject.Find ("gameController");
			gameController gameCtrl;
			gameCtrl = temp.GetComponent<gameController> ();
			gameCtrl.gameOver();

			moving = false;
			alive = false;
			dino.SetActive (false);
			skeleton.SetActive (true);
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY  
				| RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
		}

		if (target.tag == "Fruit") {
			
		}
	}

	public void spin() {
		transform.Rotate(0, 180, 0);
	}
		
	private IEnumerator startMoving() {
		for(int i =0; i<1; i++) {
			yield return new WaitForSeconds(0.30f);
		}
		moving = true;
	}
}
