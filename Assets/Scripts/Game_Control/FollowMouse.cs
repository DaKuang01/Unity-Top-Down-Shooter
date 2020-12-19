using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class FollowMouse : MonoBehaviour
{
    private float mouseX; 
    private float mouseY; 
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //mouseX = Input.m
        transform.position = Input.mousePosition; 
        
    }

   
}
