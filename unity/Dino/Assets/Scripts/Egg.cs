using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour {
	private IEnumerator coroutine;
	private GameObject inside;
	private GameObject[] shells;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void crack () {
		shells = GameObject.FindGameObjectsWithTag ("Shell");
		foreach (GameObject shell in shells)
		{
			shell.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		}
		coroutine = lifeTime (0.5f);
		StartCoroutine(coroutine);
	}

	private IEnumerator lifeTime(float duration) {
		for(float i = 0; i < duration; i+=Time.deltaTime) {
			yield return new WaitForSeconds(Time.deltaTime);
		}
		Destroy (gameObject);
	}
}
