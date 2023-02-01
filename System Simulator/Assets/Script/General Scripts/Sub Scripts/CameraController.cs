using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class CameraController : MonoBehaviour
{
    [BoxGroup("Camera")]
    public float maxZoom, minZoom;
    public float walkingSpeed, shiftedSpeed;
    public float dragSpeed = 0.01f;
    [Space]
    public bool _enableMovement = true;
    [Space]
    public RectTransform DetectLayer;

    void Update(){
        if(_enableMovement){
            if(this.GetComponent<Camera>().orthographicSize > minZoom && Input.mouseScrollDelta.y > 0 || Input.GetKey(KeyCode.Q)){ // negative -1
                this.GetComponent<Camera>().orthographicSize += -1;
            } else if(this.GetComponent<Camera>().orthographicSize < maxZoom && Input.mouseScrollDelta.y < 0 || Input.GetKey(KeyCode.E)){ // positive +1
                this.GetComponent<Camera>().orthographicSize += 1;
            }
        }

        Vector2 mousePosition = Input.mousePosition;
        if (RectTransformUtility.RectangleContainsScreenPoint(DetectLayer, mousePosition)) {
            Debug.Log("NOT MOVING");
            _enableMovement = false;
        } else if(_enableMovement == false){
            _enableMovement = true;
        }
    }

    void FixedUpdate() {

        if(_enableMovement){
            if(Input.GetKey(KeyCode.W)){
                transform.Translate(Vector2.up * walkingSpeed, Space.World);
            }
            if(Input.GetKey(KeyCode.S)){
                transform.Translate(Vector2.down * walkingSpeed, Space.World);
            }
            if(Input.GetKey(KeyCode.D)){
                transform.Translate(Vector2.right * walkingSpeed, Space.World);
            }
            if(Input.GetKey(KeyCode.A)){
                transform.Translate(Vector2.left * walkingSpeed, Space.World);
            }
        }
    }
}
