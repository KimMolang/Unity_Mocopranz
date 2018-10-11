using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    ObjectMgr objectMgr;

    void Awake()
    {
        // (Need a modification)
        // 로딩

        objectMgr = ObjectMgr.instance;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
