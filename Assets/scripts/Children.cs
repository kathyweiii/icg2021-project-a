using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Children : MonoBehaviour {

    public SpriteRenderer[] myChildren;
    public int collisionCount = 0;
    bool isColliding = false;

    void Start() {
        myChildren = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update() {
        if(!isColliding) {
            if (collisionCount >= 4) {
                // Debug.Log("Vehicle parked successfully.");
                ChangeColor(Color.green);
            }else {
                ResetColor();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        ChangeColor(Color.red);
        isColliding = true;
        
    }
    void OnCollisionExit2D(Collision2D collision) {
        ChangeColor(Color.white);
        isColliding = false;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.tag == "wheel") {
            collisionCount++;
            // Debug.Log("++");
        }
    }
    void OnTriggerExit2D(Collider2D coll) {
        if (coll.tag == "wheel") {
            collisionCount--;
            // Debug.Log("--");
        }
    }

    void ChangeColor(Color color){
        foreach(SpriteRenderer r in myChildren){
            r.color = color;
        }
    }
    void ResetColor(){
        ChangeColor(Color.white);
    }
}
