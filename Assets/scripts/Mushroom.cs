using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour{
    void OnTriggerEnter2D(Collider2D other){
        GameObject.Destroy(this.gameObject);
    }
}
