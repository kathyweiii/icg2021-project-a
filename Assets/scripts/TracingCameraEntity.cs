using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingCameraEntity : MonoBehaviour {
    
    public GameObject targetObject;
    public float MOVING_THERSHOLD = 3f;

    void Update() {
        Vector3 deltaPos = targetObject.transform.position - this.transform.position;
        Vector3 position = deltaPos * Time.deltaTime;

        this.transform.position += new Vector3(position.x, position.y, 0);  // new是為了不考慮z
    }

    void LateUpdate() {
        Vector2 deltaPos = this.transform.position - targetObject.transform.position;
        if(deltaPos.magnitude > MOVING_THERSHOLD){
            deltaPos.Normalize();

            Vector2 newPosition = new Vector2(targetObject.transform.position.x, targetObject.transform.position.y) + deltaPos * MOVING_THERSHOLD;
            this.transform.position = new Vector3(newPosition.x, newPosition.y, this.transform.position.z);
        }
    }
}
