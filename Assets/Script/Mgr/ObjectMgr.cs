using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMgr : MonoBehaviour {

    public enum CommonObjectType
    {
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
        commonObjectList = new GameObject[(int)CommonObjectType.MAX];
        commonObjectList[(int)CommonObjectType.AttackBox]
                = (GameObject)Resources.Load("Prefab/AttackBox");

        CreateCommonObject();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateCommonObject()
    {

    }
}
