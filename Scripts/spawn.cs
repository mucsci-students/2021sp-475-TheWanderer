using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class spawn : MonoBehaviour
{
    public enum SpawnState {SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        
        public int count;
        public float rate;
    }

    public Transform spawner;
    public Transform spawner2;
    public Transform spawner3;
    public Wave[] waves;
    private int nextWave = 0;
    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    public int finalWave;
    private float searchCountDown = 1f;
    private int randomNumber = 0;
    private SpawnState state = SpawnState.COUNTING;

    private Vector3 initialOne;
    private Vector3 initialTwo;
    private Vector3 initialThree;
    void Start ()
    {
        waveCountdown = timeBetweenWaves;
        initialOne = spawner.transform.position;
        initialTwo = spawner2.transform.position;
        initialThree = spawner3.transform.position;
        randomNumber = 1;
    }
    void Update()
    {
       
        if(finalWave == waves.Length || finalWave == 2 && waves.Length == 2)
        {   
            Debug.Log("Finished Level");
            if(waveCountdown <= 2)
            {
               SceneManager.LoadScene("StartScreen");  
            }
        }

        if(state == SpawnState.WAITING)
        {
            if(EnemyIsAlive())
            {
                //Begin new round
                WaveCompleted();
                return;
            }
            else
            {
                return;
            }
        }


        if(waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine( SpawnWave ( waves[nextWave] ) );
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed ");
        finalWave += 1;
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if(nextWave + 1 > waves.Length - 1)
        {
            Debug.Log("All waves complete!");
        }
        else
        {
            nextWave++;
        }
        
    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if(GameObject.FindGameObjectWithTag("enemy") == null)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; ++i)
        {
            SpawnEnemy( _wave.enemy, _wave);
            yield return new WaitForSeconds( 1f/_wave.rate );
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy (Transform _enemy, Wave _wave)
    {
        
        Debug.Log("Spawning Enemy" + _enemy.name);
        if(randomNumber == 1 )
        {
            Instantiate(_enemy, spawner.position, transform.rotation);
            if(waves.Length == 7 || waves.Length == 2 && finalWave == 1){}
            else
                spawner.transform.position = new Vector3(Random.Range(initialOne.x, initialOne.x +50f),initialOne.y,initialOne.z);
            randomNumber = 2;
        }
        else if(randomNumber == 2)
        {
            Instantiate(_enemy, spawner2.position, transform.rotation);
            if(waves.Length == 7 || waves.Length == 2 && finalWave == 1){}
            else
                spawner2.transform.position = new Vector3(Random.Range(initialTwo.x-50f, initialTwo.x+50f),initialTwo.y,initialTwo.z);
            randomNumber = 3;
        }
        else if(randomNumber == 3 || waves.Length == 10)
        {
            Instantiate(_enemy, spawner3.position, transform.rotation);
            if(waves.Length == 7 || waves.Length == 2 && finalWave == 1){}
            else
                spawner3.transform.position = new Vector3(Random.Range(initialThree.x, initialThree.x -50f),initialThree.y,initialThree.z);
            randomNumber = 1;
        }
    }
}