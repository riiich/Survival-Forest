using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

public enum SpawnState {SPAWN, COUNT, WAIT}

[System.Serializable]   // make this System.Serializble so we can edit it in the Inspector
public class Wave
{
    public string name;
    public Enemy enemy; // enemy object
    public int amount;
    public float spawnRate;
}

public class SpawnEnemyWaves : MonoBehaviour
{
    private int nextWave = 0;   // the index for the waves array
    private float searchForEnemyTime = 1f;
    private SpawnState state = SpawnState.COUNT;

    public Wave[] waves;   // holds the enemy objects in an array
    public float waveDelayTime = 5.0f;  // the amount of time between each spawn wave
    public float waveCountdown; // time difference between each spawn wave
    

    // Start is called before the first frame update
    void Start()
    {
        this.waveCountdown = this.waveDelayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == SpawnState.WAIT)
        {
            if(!isAlive())  // checks to see if there aren't any enemies that are alive
            {
                newRound();
                return;
            }
            else
                return; // if enemies aren't all killed off, player will have to kill them before new wave starts
        }

        // spawns enemy wave when the time between each spawn wave reaches 0 seconds
        if(this.waveCountdown <= 0f)
            if(state != SpawnState.SPAWN)
                StartCoroutine(SpawnWave(waves[nextWave]));     // coroutine can be paused at any point with a yield statement (pauses execution and resumes at next frame)
        else
            this.waveCountdown -= Time.deltaTime;
    }
        
    bool isAlive()
    {
        
        this.searchForEnemyTime -= Time.deltaTime; 
        if(searchForEnemyTime <= 0f)
        {
            this.searchForEnemyTime = 1f;   // reset the time to search if enemies are alive still (which is 1 second)

            if (GameObject.FindGameObjectWithTag("Enemy") == null)  // null because there are no enemies left
            {
                Debug.Log("INSIDE isAlive FUNCTION");
                return false; 
            } 
        }

        return true;
    }

    // IEnumerator pauses the time a little bit before executing this function
    IEnumerator SpawnWave(Wave wave)
    {   
        this.state = SpawnState.SPAWN;  // set the current state to the SPAWN state
        Debug.Log("Spawning Wave: " + wave.name + " AND the current SpawnState is " + this.state);

        // spawn the enemies
        for(int i = 0; i < wave.amount; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);   // wait for a 1f/spawnRate amount of seconds
        }

        // once the enemies have been spawned, change the state back to the WAIT state to delay between each wave
        this.state = SpawnState.WAIT;

        yield break;
    }

    // spawn individual enemy
    void SpawnEnemy(Enemy enemy)
    {
        Debug.Log("Spawning enemy: " + enemy.name);
        Instantiate(enemy, transform.position, transform.rotation); // spawn enemies
    }

    void newRound()
    {
        Debug.Log("Current wave is done!");
        this.state = SpawnState.COUNT;
        this.waveCountdown = this.waveDelayTime;
        this.nextWave++;
    }
}
