using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    ObjectMgr objectMgr;
    StateMgr stateMgr;

    UIMgr_Tool_Skill uiMgr_ToolSkill;


    void Awake()
    {
        // (Need a modification)
        // 로딩

        objectMgr = ObjectMgr.instance;
        stateMgr = StateMgr.instance;

        // (Tool)
        uiMgr_ToolSkill = UIMgr_Tool_Skill.instance;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
