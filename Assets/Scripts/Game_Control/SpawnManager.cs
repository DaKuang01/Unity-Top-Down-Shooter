using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int spawnNumber = 2; 
    public int waveNumber;
    //private float spawnRate = 2; 
    //private bool coolDown = false; 
    private Camera cam; 
    [SerializeField] GameObject[] enemies; 
    private GameManager gameManager; 
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); 
        cam = Camera.main; 
        waveNumber = 0; 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameActive){
            int enemyLeft = FindObjectsOfType<EnemyController>().Length; 
            if(enemyLeft == 0){
                gameManager.NightIntermission(); 
            }
        } // if game is active
    } // Update()

    // Gets a random position out of camera range
    private Vector3 GetRandomPosition(){
        float height = cam.orthographicSize +1; 
        float width = height * cam.aspect; 
        int topBottom = Random.Range(0,2); 
        float z; 

        float x = cam.transform.position.x + Random.Range(-width, width); 

        if(topBottom == 0){
            z = cam.transform.position.z + height + Random.Range(10,30); 
        }
        else{
            z = cam.transform.position.z - height - Random.Range(10,30);
        }

        return new Vector3(x,1,z); 
    }

    public void Spawn() {
        if(gameManager.isGameActive){
            ++waveNumber; 
            ++spawnNumber; 
            Debug.Log("spawn");
            for(int i = 0; i < waveNumber; ++i){
                SpawnEnemy();  
            }
        }        
    }

    // Spawns an enemy at a random position
    public void SpawnEnemy(){
        for(int i = 0; i < spawnNumber / 1.25; ++i){
            GameObject randomEnemy = GetRandomEnemy(); 
            Instantiate(randomEnemy, GetRandomPosition(), randomEnemy.transform.rotation); 
        } 
    }

    private GameObject GetRandomEnemy(){
        return enemies[Random.Range(0,enemies.Length)]; 
    }

   
}
