using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counting : MonoBehaviour {
    
    int mush_num = 0;
    int countAll;
    public Children[] m_Children = new Children[2];
    [SerializeField] CarEntity[] m_Cars = new CarEntity[3];

    void Update() {
        countAll = m_Children[0].collisionCount + m_Children[1].collisionCount;
        if(countAll == 8) {
            Debug.Log("Great!!!!");
            
            // StartCoroutine(destroyCar(2f));
            // for(int i = 0; i < m_Cars.Length; i++) {
            //     GameObject.Destroy(m_Cars[i].gameObject);
            // }

            StartCoroutine(mushrooming());
            // Debug.Log("Done!!!");
            // Application.Quit();
        }
    }

    // IEnumerator destroyCar() {
    //     yield return new WaitForSeconds(2f);
    //     for(int i = 0; i < m_Cars.Length; i++) {
    //         GameObject.Destroy(m_Cars[i].gameObject);
    //     }
    // }

    IEnumerator mushrooming() {
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < m_Cars.Length; i++) {
            GameObject.Destroy(m_Cars[i].gameObject);
        }
        
        float x;
        float y;
        float z;

        while(mush_num < 100) {
            x = Random.Range(-18, 18);
            y = Random.Range(-10, 10);
            z = 0f;
            
            var prefab = Resources.Load<GameObject>("prefabs/mushroom");
            GameObject.Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            mush_num++;
        }
        Debug.Log("Done!!!");
        Application.Quit();
    }
}



    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.F))
    //     {
    //         var prefab = Resources.Load<GameObject>("car");
    //         GameObject.Instantiate(prefab, Random.insideUnitCircle, Quaternion.identity);
    //     }
    // }
