using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LojaManager : MonoBehaviour {

    void Awake()
    {
        Destroy(GameObject.Find("UIManager(Clone)"));
        Destroy(GameObject.Find("GameManager(Clone)"));
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
