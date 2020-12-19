using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 40; 
    [SerializeField] private float damage; 
    private EnemyController enemyScript; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Projectile shoots forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed); 
        
    }

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player")){
            gameObject.SetActive(false);  
        }

        if(other.gameObject.CompareTag("enemy")){
            enemyScript = other.GetComponent<EnemyController>(); 
            enemyScript.health -= damage; 
            
        }
    }

    private void OnBecameInvisible() {
        gameObject.SetActive(false);  
    }
}
