using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private GameManager gameManager;
    private Button startButton; 
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); 

        //startButton = GameObject.Find("Start Button").GetComponent<Button>(); 
         


        //startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        gameManager.StartGame(); 
        
    }

    public void restartGame(){
        gameManager.RestartGame(); 
    }
}
