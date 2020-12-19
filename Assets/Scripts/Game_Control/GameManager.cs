using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public bool isGameActive = false; 
    private float nightTime = 2.5f; 
    private bool isNight = false; 
    private UIBehavior uIBehavior; 
    private SpawnManager spawnManager; 
    // Start is called before the first frame update
    void Start()
    {
        uIBehavior = GetComponent<UIBehavior>(); 
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        NightIntermission();  
        uIBehavior.StartScreenSetActive(false);  
    }

    public void NightIntermission(){
        StartCoroutine(IntermissionTime()); 
    }

    public void EndGame(){
        isGameActive = false; 
        uIBehavior.EndScreenSetActive(true); 
        uIBehavior.GameScreenSetActive(false); 
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator IntermissionTime(){
        if(!isNight) {
            isGameActive = false; 
            isNight = true; 
            uIBehavior.GameScreenSetActive(false); 
            uIBehavior.NightScreenSetActive(true, spawnManager.waveNumber); 
            Debug.Log("First Half");
            yield return new WaitForSeconds(nightTime); 
            Debug.Log("Second Half");
            uIBehavior.NightScreenSetActive(false, spawnManager.waveNumber); 
            uIBehavior.GameScreenSetActive(true); 
            isGameActive = true; 
            isNight = false; 
            spawnManager.Spawn(); 
        } // if is currently night
    } // IntermissionTime()
}
