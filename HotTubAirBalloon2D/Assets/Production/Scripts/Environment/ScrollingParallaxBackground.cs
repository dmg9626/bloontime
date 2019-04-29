using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingParallaxBackground : MonoBehaviour
{
    public bool parallaxToggle, scrollingToggle;
    public float backgroundSize;
    public float parallaxHorizontalSpeed, parallaxVerticalSpeed;
    public float viewZone;

    private Transform cameraTransform;
    private Transform[] layers;
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;
    private float lastCameraY;

    
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        lastCameraY = cameraTransform.position.y;
        int childIndex = transform.childCount;
        layers = new Transform[childIndex];
        for(int i = 0; i < childIndex; i++){
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;
        
    }

    private void Update(){

        if(parallaxToggle){
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * parallaxHorizontalSpeed);
            lastCameraX = cameraTransform.position.x;
            
            float deltaY = cameraTransform.position.y - lastCameraY;
            transform.position += Vector3.up * (deltaY * parallaxVerticalSpeed);
            lastCameraY = cameraTransform.position.y;
        }

        if(scrollingToggle){
            if(cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone)){
                ScrollLeft();
            }

            if(cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone)){
                ScrollRight();
            }
        }
        
    }

    private void ScrollLeft(){
        int lastRight = rightIndex;
        //layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        layers[rightIndex].position = new Vector3((Vector3.right * (layers[leftIndex].position.x - backgroundSize)).x, layers[rightIndex].position.y, layers[rightIndex].position.z);
        leftIndex = rightIndex;
        rightIndex--;
        if(rightIndex<0){
            rightIndex = layers.Length-1;
        }
    }

    private void ScrollRight(){
        int lastLeft = leftIndex;
        layers[leftIndex].position = new Vector3((Vector3.right * (layers[rightIndex].position.x + backgroundSize)).x, layers[rightIndex].position.y, layers[rightIndex].position.z);
        rightIndex = leftIndex;
        leftIndex++;
        if(leftIndex == layers.Length){
            leftIndex = 0;
        }

    }

    public void resetBackgroundToCamera(){

        cameraTransform = Camera.main.transform;
        
        lastCameraX = cameraTransform.position.x;
        lastCameraY = cameraTransform.position.y;

        transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, transform.position.z);

    }

}
