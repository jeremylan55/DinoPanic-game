using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {

	public GameObject explosion;
	private float speed = 40f;
	private bool moving = true;
	private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		explosion = this.gameObject.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			transform.Translate (0, -1 * speed * Time.deltaTime, 0);
		}
	}

	void OnTriggerEnter(Collider target) {
		if( target.tag == "setPiece" ) {
			moving = false;	
			coroutine = explode (0.1f, 0.2f, 2.0f);
			StartCoroutine(coroutine);
		}
	}

	private IEnumerator explode(float expandDuration, float contractDuration, float blastRadius)
	{	
		for (int k = 1; k < 4; k++) {
			Destroy(this.gameObject.transform.GetChild(k).gameObject);
		}

		float expandRate = blastRadius / (expandDuration / Time.deltaTime);
		float contractRate = blastRadius / (contractDuration / Time.deltaTime);
		Vector3 currentScale = new Vector3 ( 0, 0, 0);

		for(float i = 0; i < expandDuration; i+=Time.deltaTime) {
			yield return new WaitForSeconds(Time.deltaTime);
			explosion.transform.localScale = new Vector3( currentScale.x+=expandRate, currentScale.y+=expandRate, currentScale.z+=expandRate);
		}
		for(float j = 0; j < contractDuration; j+=Time.deltaTime) {
			yield return new WaitForSeconds(Time.deltaTime);
			explosion.transform.localScale = new Vector3( currentScale.x-=contractRate, currentScale.y-=contractRate, currentScale.z-=contractRate);
		}

		Destroy (gameObject);

	}

}
