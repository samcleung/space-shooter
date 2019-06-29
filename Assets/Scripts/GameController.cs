using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;

    public Text gameOverText;
    public Text restartText;

    private int score;

    private float _maxZvalue;
    private float _maxXvalue;

    private bool restart;
    private bool gameOver;

    private void Start()
    {
        // Initializing the text values as nothing
        gameOverText.text = "";
        restartText.text = "";
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        _maxZvalue = screenBounds.z * 1.2f;
        _maxXvalue = screenBounds.z * 0.9f;

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];

                Vector3 spawnPosition = new Vector3(Random.Range(-_maxXvalue, _maxXvalue), 0f, _maxZvalue);
                Quaternion spawnRotation = Quaternion.identity;

                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press R to restart the game.";
                restart = true;
                // Usually break is a bad idea, but in this case, it makes sense
                break;
            }
        }
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }

    public void AddScore(int newScoreVal)
    {
        score += newScoreVal;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
