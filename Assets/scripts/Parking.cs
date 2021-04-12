using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parking : MonoBehaviour
{
    int collisionCount = 0;

    void Update() {
        if (collisionCount >= 4) {
            Debug.Log("Vehicle parked successfully.");
            // green
            
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.tag == "wheel") {
            collisionCount++;
            Debug.Log("++");
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.tag == "wheel") {
            collisionCount--;
            Debug.Log("--");
        }
    }
}
