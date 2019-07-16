using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToWorld_ScreenClick : MonoBehaviour
{
    public GameObject cube;
    public float Depth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;
            CameraSnapshot snapshot = new CameraSnapshot(Camera.main);

            var destination = snapshot.Screen2dToWorldPixel(mousePosition, Depth);
            cube.transform.position = destination;
        }
        
    }


   
}
