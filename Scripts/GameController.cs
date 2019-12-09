using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public AudioSource musicSource;
	public AudioClip musicClipOne;
	public AudioClip musicClipTwo;

	public Text pointsText;
	public Text restartText;
	public Text gameOverText;
	public Text winText;

	private bool win;
	private bool gameOver;
	private bool restart;
	private int points;

	void Start ()
	{
		gameOver = false;
		restart = false;
		win = false;
		restartText.text = "";
		gameOverText.text = "";
		winText.text = "";
		points = 0;
		UpdatePoints();
		StartCoroutine(SpawnWaves());
	}

	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.T))
			{
				SceneManager.LoadScene("Main");
			}
		}
		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards[Random.Range (0,hazards.Length)];
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
			if (gameOver)
			{
				restartText.text = "Press 'T' to Try again";
				restart = true;
				break;
			}
		}
	}

	public void AddPoints (int newPointsValue)
	{
		points += newPointsValue;
		UpdatePoints();
	}

	void UpdatePoints()
	{
		pointsText.text = "Points: " + points;
		if (points >= 200)
		{
			winText.text = "You Win! Game created by Joshua Bonilla!";
			gameOver = true;
            restart = true;
			musicSource.clip = musicClipOne;
			musicSource.Play();
		}
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
		musicSource.clip = musicClipTwo;
		musicSource.Play();
	}
}