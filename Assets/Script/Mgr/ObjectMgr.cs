﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectMgr : MonoBehaviour {

    public enum CommonObjectType
    {
        Bin,
        AttackBox,
        MAX
    }

    static public GameObject[] commonObjectList;


    private static ObjectMgr _instance;
    public static ObjectMgr getInstance
    {
        get
        {
            if (_instance == null)
            {
                GameObject inMapMgrs = GameObject.Find("Mgrs");

                if (inMapMgrs == null)
                {
                    inMapMgrs = new GameObject();
                    inMapMgrs.name = "Mgrs";
                }

                _instance = inMapMgrs.GetComponent<ObjectMgr>();

                if (_instance == null)
                    _instance = inMapMgrs.AddComponent<ObjectMgr>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        RoadCommonObject();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void RoadCommonObject()
    {
        commonObjectList = new GameObject[(int)CommonObjectType.MAX];

        commonObjectList[(int)CommonObjectType.Bin]
                = Resources.Load("Prefab/Bin") as GameObject;
        commonObjectList[(int)CommonObjectType.AttackBox]
                = Resources.Load("Prefab/AttackBox") as GameObject;
    }

    static public GameObject CreateSkill(string _strScripName, GameObject _owner)
    {
        // (수정) 오브젝트 풀
        if (_owner == null)
            return null;


        GameObject binForCreatingSkill
                = Instantiate(ObjectMgr.commonObjectList[(int)ObjectMgr.CommonObjectType.Bin]);


        Skill componentSkill = null;

        //try
        //{
            componentSkill
                = binForCreatingSkill.AddComponent(Type.GetType(_strScripName)) as Skill;
        //}
        //catch(System.Exception e)
        //{
        //    Debug.Log(e.Message);
        //}
        //Debug.Log("!!");
        //Debug.LogWarning("!!");
        //Debug.LogError("!!");

        if (componentSkill == null)
        {
            Debug.LogError("ObjectMgr::CreateSkill -- Failed Create a skill. Script name is wrong. [_strScripName : "
                + _strScripName + "]");
            return null;
        }


        binForCreatingSkill.name = _strScripName;
        componentSkill.SetOwnCharacter(_owner);


        return binForCreatingSkill;
    }
}
