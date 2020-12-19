using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI; 

public class UIBehavior : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText; 
    [SerializeField] TextMeshProUGUI healthText; 
    [SerializeField] TextMeshProUGUI nightText; 
    [SerializeField] GameObject startScreen; 
    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject endScreen; 
    [SerializeField] GameObject nightUI; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAmmo(int ammo){
        ammoText.text = "Ammo:" + ammo; 
    }

    public void UpdateHealth(int health){
        healthText.text = "Health:" + health; 
    }

    public void UpdateNight(int waveNumber){
        nightText.text = "Night " + (waveNumber + 1); 
    }

    public void StartScreenSetActive(bool active){
        startScreen.SetActive(active); 
    }

    public void GameScreenSetActive(bool active){
        gameScreen.SetActive(active); 
    }

    public void EndScreenSetActive(bool active){
        endScreen.SetActive(active); 
    }

    public void NightScreenSetActive(bool active, int waveNumber){
        UpdateNight(waveNumber); 
        nightUI.SetActive(active); 
    }
}
