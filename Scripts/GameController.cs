using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
    
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    
    private bool gameOver;
    private bool restart;
    private int score;

	// Use this for initialization
	void Start () {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore ();
        StartCoroutine (SpawnWaves ());
	}
    
    //function to spawn waves of hazards
    IEnumerator SpawnWaves (){
        yield return new WaitForSeconds (startWait);
        
        while(true)
        {
            for(int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                //wait for next asteroid
                yield return new WaitForSeconds (spawnWait);
            }//end for loop for creating hazard waves
            //wait for next wave 
            yield return new WaitForSeconds(waveWait);
            
            if (gameOver) {
                restartText.text = "Press 'R' for Restart";
                restart = true; 
                break;
            }
        }//end while
    }//end SpawnWaves function
    
    void UpdateScore (){
        scoreText.text = "Score: " + score;
    }
    
    public void AddScore (int newScoreValue){
        score += newScoreValue;
        UpdateScore ();
    }
    
	
	// Update is called once per frame
	void Update () {
		if (restart){
            if (Input.GetKeyDown (KeyCode.R))
            {
                Application.LoadLevel (Application.loadedLevel);
            }
        }
	}
    
    public void GameOver (){
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
    
    
}
