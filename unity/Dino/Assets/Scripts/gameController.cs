using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gameController : MonoBehaviour {

	public int gameScore = 0;
	private IEnumerator fruitCoroutine;
	private IEnumerator meteorCoroutine;
	private bool started = false;

	public GameObject MeteorPrefab;
	GameObject MeteorPrefabClone;

	public GameObject FruitPrefab;
	GameObject FruitPrefabClone;

	public GameObject EggPrefab;
	GameObject EggPrefabClone;

	public GameObject CharacterPrefab;
	GameObject CharacterPrefabClone;

	private float fruitInterval = Random.Range (1.5f, 30.0f);

	public GameObject ResultScreen;
	public GameObject StartScreen;
	public GameObject GameScore;

	// Use this for initialization
	void Start () {
		ResultScreen = GameObject.Find ("ResultScreen");
		ResultScreen.SetActive (false);
		StartScreen = GameObject.Find ("StartScreen");
		GameScore = GameObject.Find ("GameScore");
		layEgg ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void createMeteor () {
		float positionX = Random.Range (-29.5f, 29.5f);
		MeteorPrefabClone = Instantiate (MeteorPrefab, new Vector3 ( positionX, 130, 7), 
			Quaternion.identity) as GameObject;
	}

	void createFruit () {
		float positionX = Random.Range (-29.5f, 29.5f);
		FruitPrefabClone = Instantiate (FruitPrefab, new Vector3 ( positionX, 9, 7), 
			Quaternion.identity) as GameObject;
	}

	void layEgg () {
		EggPrefabClone = Instantiate (EggPrefab, new Vector3 ( 0, 12, 6), 
			Quaternion.identity) as GameObject;
		Debug.Log("layegg");


	}

	void hatchCharacter () {
		GameObject prevCharacter = GameObject.FindGameObjectWithTag ("Character");
		Destroy (prevCharacter);

		GameObject temp = GameObject.FindGameObjectWithTag ("Egg");
		Egg egg;
		egg = temp.GetComponent<Egg> ();
		egg.crack ();

		CharacterPrefabClone = Instantiate (CharacterPrefab, new Vector3 ( 0, 9, 7), 
			Quaternion.identity) as GameObject;
	}



	private IEnumerator fruitTimer() {
		while(started)
		{
			yield return new WaitForSeconds(Random.Range(1.5f, 3.0f));
			createFruit ();
		}
	}

	private IEnumerator meteorTimer() {
		while(started)
		{
			yield return new WaitForSeconds(Random.Range(0.3f, 4.0f));
			createMeteor ();
		}
	}


	public void incrementGameScore() {
		gameScore++;
		GameObject UIGameScoreGameObject = GameObject.Find ("UIGameScore");
		UnityEngine.UI.Text UIGameScoreText = UIGameScoreGameObject.GetComponent<UnityEngine.UI.Text> (); 
		UIGameScoreText.text = gameScore.ToString();
	}

	public void resetGameScore() {
		gameScore = 0;
		GameObject UIGameScoreGameObject = GameObject.Find ("UIGameScore");
		UnityEngine.UI.Text UIGameScoreText = UIGameScoreGameObject.GetComponent<UnityEngine.UI.Text> (); 
		UIGameScoreText.text = gameScore.ToString();
	}

	public void tap() {
		if (!started) {
			ResultScreen.SetActive (false);
			GameScore.SetActive (true);
			started = true;

			if (GameObject.FindGameObjectWithTag ("Egg") == null) {
				layEgg ();
			}

			hatchCharacter ();
			resetGameScore ();
			StartScreen.SetActive (false);

			fruitCoroutine = fruitTimer ();
			StartCoroutine (fruitCoroutine);
			meteorCoroutine = meteorTimer ();
			StartCoroutine (meteorCoroutine);
		}
		else {
			GameObject temp = GameObject.FindGameObjectWithTag("Character");
			Character charCtrl;
			charCtrl = temp.GetComponent<Character> ();
			charCtrl.spin();
		}


	}

	public void gameOver() {
		started = false;
		ResultScreen.SetActive (true);
		GameScore.SetActive (false);
		GameObject UIResultsGameScore = GameObject.Find ("UIResultsGameScore");
		UnityEngine.UI.Text UIGameScoreText = UIResultsGameScore.GetComponent<UnityEngine.UI.Text> (); 
		UIGameScoreText.text = gameScore.ToString();

		StopAllCoroutines();
//		GameObject UIOverlay = GameObject.Find ("UIOverlay");
//		UIOverlay.transform.localScale = new Vector3 (1, 1, 1);
	}
}
