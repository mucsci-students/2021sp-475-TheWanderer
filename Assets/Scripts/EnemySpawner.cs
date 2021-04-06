using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public GameObject[] thingsToSpawn;
    //public int randomRangeMin, randomRangeMax;
    //public int carrotChanceMin, carrotChanceMax;
    //public int goldChanceMin, goldchanceMax;
    //public int powerUpMin, powerUpMax;
    //public int rottonMin, rottonMax;
    //public int obstacleChanceMin, obstacleChanceMax;
    //public int healthCarrotChanceMin, healthCarrotChanceMax;
    //public int diamondMin, diamondMax;

    public int enemyChanceMin, enemyChanceMax;
    public int itemChanceMin, itemChanceMax;
    public int randomRangeMin, randomRangeMax;
    public int enemy_1Min, enemy_1Max;
    public int enemy_2Min, enemy_2Max;
    public int enemy_3Min, enemy_3Max;
    public int enemy_4Min, enemy_4Max;

    public int item_1Min, item_1Max;
    public int item_2Min, item_2Max;
    public int item_3Min, item_3Max;
    public int item_4Min, item_4Max;

    public int numberOfEnemies;

    //enum SpawnEntities {CARROT,CARROT_GOLDEN, CARROT_POWERUP, CARROT_ROTTEN, HURDLE, TREE, FURMAN,CARROT_HEALTH, UKNUCKS, CARROT_DIAMOND};
    enum SpawnEntities {enemy_1, enemy_2, enemy_3, enemy_4, item_1, item_2, item_3, item_4};

    SpawnEntities item;
    // Start is called before the first frame update
    void Start()
    {
        if(thingsToSpawn.Length > 0)
        {
            SpawnItem();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnItem()
    {
        ItemChooser();
        GameObject thing = null;
        int randomNum = Random.Range(randomRangeMin, randomRangeMax);
        
        //Vector3 enemySpawnPos = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
        Vector3 enemySpawnPos = new Vector3(this.transform.position.x, this.transform.position.y + 1);
        switch (item)
        {
            case SpawnEntities.item_1:
                thing = Instantiate(thingsToSpawn[0], enemySpawnPos, this.transform.rotation) as GameObject;
                break;
            case SpawnEntities.item_2:
                thing = Instantiate(thingsToSpawn[1], enemySpawnPos, this.transform.rotation) as GameObject;
                break;
            case SpawnEntities.item_3:
                thing = Instantiate(thingsToSpawn[2], enemySpawnPos, this.transform.rotation) as GameObject;
                break;
            case SpawnEntities.item_4:
                thing = Instantiate(thingsToSpawn[3], enemySpawnPos, this.transform.rotation) as GameObject;
                break;
            case SpawnEntities.enemy_1:
                if (numberOfEnemies > 0)
                {
                    thing = Instantiate(thingsToSpawn[4], this.transform.position, this.transform.rotation) as GameObject;
                }
                break;
            case SpawnEntities.enemy_2:
                if (numberOfEnemies > 0)
                {
                    thing = Instantiate(thingsToSpawn[5], this.transform.position, Quaternion.identity) as GameObject;
                }
                break;
            case SpawnEntities.enemy_3:
                if (numberOfEnemies > 0)
                {
                    thing = Instantiate(thingsToSpawn[6], this.transform.position, Quaternion.identity) as GameObject;
                }
                break;
            case SpawnEntities.enemy_4:
                if (numberOfEnemies > 0)
                {
                    thing = Instantiate(thingsToSpawn[7], this.transform.position, Quaternion.identity) as GameObject;
                }
                break;
        }

        if(thing != null)
        {
            thing.transform.SetParent(this.transform);
        }
    }

    void ItemChooser()
    {
        int randomNum = Random.Range(randomRangeMin, randomRangeMax);
        if (randomNum >= itemChanceMin && randomNum <= itemChanceMax)
        {
            int spawnRange = Random.Range(randomRangeMin, randomRangeMax);
            if(spawnRange >= item_1Min && spawnRange <= item_1Max)
            {
                item = SpawnEntities.item_1;
            }
            else if(spawnRange >= item_2Min && spawnRange <= item_2Max)
            {
                item = SpawnEntities.item_2;
            }
            else if(spawnRange >= item_3Min && spawnRange <= item_3Max)
            {
                item = SpawnEntities.item_3;
            }
            else if(spawnRange >= item_4Min && spawnRange <= item_4Max)
            {
                item = SpawnEntities.item_4;
            }
        }
        else if (randomNum >= enemyChanceMin && randomNum <= enemyChanceMax)
        {
            int enemyChoice = Random.Range(0, numberOfEnemies);

            if(enemyChoice == 0)
            {
                item = SpawnEntities.enemy_1;
            }
            else if(enemyChoice == 1)
            {
                item = SpawnEntities.enemy_2;
            }
            else if (enemyChoice == 2)
            {
               item = SpawnEntities.enemy_3;
            }
             else if (enemyChoice == 3)
            {
               item = SpawnEntities.enemy_4;
            }
        }
    }
}
