using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int damage; 
    [SerializeField] private float speed = 2;
    [SerializeField] private float speedCap = 2; 
    public float health; 
    private Vector3 lookDirection;  
    private Rigidbody enemyRb; 
    private GameObject player; 
    private FollowMouse followMouse; 
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>(); 
        player = GameObject.FindGameObjectWithTag("Player"); 
        followMouse = GameObject.Find("CrossHair").GetComponent<FollowMouse>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        lookDirection = (player.transform.position - transform.position).normalized; 
        enemyRb.AddForce(lookDirection * speed);
        enemyRb.velocity = Vector3.ClampMagnitude(enemyRb.velocity, speedCap);

        if(transform.position.y < -1){
            Destroy(gameObject); 
        }

        //GamePlay
        if(health < 0){
            Destroy(gameObject); 
            followMouse.image.color = new Color(255,255,255);
        }
    }
    private void OnMouseEnter() {
        followMouse.image.color = new Color(255,0,0);
    }

    private void OnMouseExit() {
        followMouse.image.color = new Color(255,255,255);
    }
}
