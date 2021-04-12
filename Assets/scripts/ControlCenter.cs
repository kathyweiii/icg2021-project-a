using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCenter : MonoBehaviour {
    
    [SerializeField] Camera[] m_Cameras = new Camera[3];
    [SerializeField] CarEntity[] m_Cars = new CarEntity[3];
    int m_SelectedIndex = 0;

    void Start() {
        Select(m_SelectedIndex);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.C)) {
            Next();
        }
    }

    void Next() {
        m_SelectedIndex++;
        if(m_SelectedIndex >= m_Cameras.Length) {
            m_SelectedIndex = 0;
        }
        Select(m_SelectedIndex);
    }

    void Select(int index) {
        for(int i = 0; i < m_Cameras.Length; i++) {
            m_Cameras[i].enabled = i == index;
            m_Cars[i].enabled = i == index;
        }
    }
}
