using UnityEngine;
using System.Collections;

public class Fruit : MonoBehaviour {

	private IEnumerator coroutine;
	private GameObject body;
 

	// Use this for initialization
	void Start () {
		coroutine = lifeTime (2.0f);
		StartCoroutine(coroutine);
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void OnTriggerEnter(Collider target) {
		if (target.tag == "Character") {
			GameObject temp = GameObject.Find ("gameController");
			gameController gameCtrl;
			gameCtrl = temp.GetComponent<gameController> ();
			gameCtrl.incrementGameScore();

			Destroy (gameObject);
		}
	}

	private IEnumerator lifeTime(float duration) {
		for(float i = 0; i < duration; i+=Time.deltaTime) {
			yield return new WaitForSeconds(Time.deltaTime);
		}
		Destroy (gameObject);
	}
}
