using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    private Vector3 offset = new Vector3(0,10,0);
    public float cameraZoom = 1; 
    [SerializeField] GameObject player; 
    private Camera cam;  
    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>(); 
        cam.orthographicSize += cameraZoom;
    }

    // Update is called once per frame
    void Update()
    {
         
        transform.position = player.transform.position + offset; 
    }
}
