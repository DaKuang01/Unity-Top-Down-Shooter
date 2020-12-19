using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 50; 
    private int clipSize = 10; 
    [SerializeField] int ammo;  
    [SerializeField] float speed = 10; 
    private float forwardInput; 
    private float horizontalInput; 
    private float deltaTime; 
    private float reloadTime = 1.5f; 
    private float recoilForce = 1.5f;
    private float recoilTime = 0.1f; 
    private float invincibleTime = 2; 
    private bool reload; 
    private bool recoil = false;  
    public bool invincible = false; 
    private Rigidbody playerRb; 
    private GameObject bullet;  
    private ObjectPool objectPool; 
    private UIBehavior uIBehavior; 
    private EnemyController enemyController; 
    private GameManager gameManager; 

    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        objectPool = GameObject.Find("Game Manager").GetComponent<ObjectPool>();
        uIBehavior = GameObject.Find("Game Manager").GetComponent<UIBehavior>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); 

        //Set Variables
        ammo = clipSize; 
        deltaTime = Time.deltaTime; 

        //Set UI
        uIBehavior.UpdateAmmo(ammo);
        uIBehavior.UpdateHealth(health); 
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameActive && !recoil){
            //***************MOVEMENT***************//
            WASDMovement_Rotation(); 

            //***********GamePlay**********//
            //Input
            // Shoot projectile
            if(Input.GetMouseButtonDown(0) && !reload){
                //Spawn bullet using object pool
                SpawnBullet(); 
                uIBehavior.UpdateAmmo(ammo);
            }
            if(Input.GetKeyDown(KeyCode.R) && ammo != clipSize){
                StartCoroutine(ReloadCoolDown()); 
            }
        }
        
    }

    IEnumerator ReloadCoolDown(){
        reload = true;
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadTime);
        ammo = clipSize;
        uIBehavior.UpdateAmmo(ammo);
        reload = false; 
    }

    IEnumerator RecoilCoolDown(){
        recoil = true; 
        yield return new WaitForSeconds(recoilTime); 
        recoil = false; 
    }
    IEnumerator InvincibleCoolDown(){
        invincible = true; 
        yield return new WaitForSeconds(invincibleTime);
        invincible = false; 
    }

    private void SpawnBullet(){
        bullet = ObjectPool.ShardInstance.GetPooledObject(); 
        if(bullet != null){
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation; 
            bullet.SetActive(true); 
            --ammo;
        } 
        if(ammo == 0){ 
            StartCoroutine(ReloadCoolDown()); 
        }
    }

    private void WASDMovement_Rotation(){
        forwardInput = Input.GetAxis("Vertical"); 
        horizontalInput = Input.GetAxis("Horizontal"); 
        //Rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;
 
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
 
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle + 90, 0));

        // WASD movement
        transform.Translate(Vector3.forward * deltaTime * forwardInput * speed, Space.World); 
        transform.Translate(Vector3.right * deltaTime * horizontalInput * speed, Space.World); 
         // If no input, should not move
        if(forwardInput == 0 && horizontalInput == 0){
            playerRb.velocity = Vector3.zero; 
            
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("enemy") && gameManager.isGameActive && !invincible){
            enemyController = other.gameObject.GetComponent<EnemyController>(); 
            //Recoil
            Vector3 awayDirection = transform.position - other.transform.position;
            //playerRb.AddForce(awayDirection * recoilForce, ForceMode.Impulse); 
            transform.Translate(awayDirection * recoilForce);
            StartCoroutine(RecoilCoolDown()); 
            StartCoroutine(InvincibleCoolDown()); 

            // Reduce health and update health ui 
            health -= enemyController.damage; 
            uIBehavior.UpdateHealth(health); 

            // If health gets to 0, game over; 
            if(health <= 0){
                // Set isGameActive to true and stop character from moving
                playerRb.constraints = RigidbodyConstraints.FreezeAll; 
                gameManager.EndGame(); 
            }
        }
    }
}
