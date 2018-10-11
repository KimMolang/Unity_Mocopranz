using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using CommonObjectName;
using ResourceInformation;


public class ObjectMgr : SingletonBase<ObjectMgr>
{
    /*static*/ private GameObject[] commonObjectList;
    /*static*/ private GameObject[] commonEffectList;
    // (Need a modification) 공통 이펙트 말고 캐릭터별 이펙트는 어떡하지..?

    protected ObjectMgr()
    {
        RoadCommonObject();
    }

    //void Awake()
    //{
    //    RoadCommonObject();
    //}

    private void RoadCommonObject()
    {

        commonObjectList
            = new GameObject[(int)ResourceInformation.Object.CommonObject.MAX];

        for(int i = 0; i < commonObjectList.Length; ++i)
        {
            ResourceInformation.Object.CommonObject index
                = (ResourceInformation.Object.CommonObject)i;

            commonObjectList[i]
                = Resources.Load(ResourceInformation.Object.Path.COMMON_OBJECT + index.ToString()) as GameObject;
        }


        commonEffectList
            = new GameObject[(int)ResourceInformation.Effect.CommonEffec.MAX];

        for (int i = 0; i < commonEffectList.Length; ++i)
        {
            ResourceInformation.Effect.CommonEffec index
                = (ResourceInformation.Effect.CommonEffec)i;

            commonEffectList[i]
                = Resources.Load(ResourceInformation.Effect.Path.COMMON_EFFECT + index.ToString()) as GameObject;
        }
    }

    public GameObject GetCommonObject(ResourceInformation.Object.CommonObject _index)
    {
        return commonObjectList[(int)_index];
    }

    public GameObject GetCommonEffect(ResourceInformation.Effect.CommonEffec _index)
    {
        return commonEffectList[(int)_index];
    }

    // Character Skill // PreLoad (Skill data preroad) // (Need a modification)
    //private GameObject[] playerCharacterSkillObjectList;

    public GameObject CreateSkill(string _strScripName, GameObject _owner)
    {
        // (Need a modification) 오브젝트 풀 (Destroy 랑 새로 생성하는 곳 다 확인해야함)
        if (_owner == null)
            return null;


        GameObject binForCreatingSkill
                = UnityEngine.Object.Instantiate(GetCommonObject(ResourceInformation.Object.CommonObject.Bin));


        Skill componentSkill = null;

        //try
        //{
            componentSkill
                = binForCreatingSkill.AddComponent(Type.GetType(_strScripName)) as Skill;
        //}
        //catch(System.Exception e)
        //{
        //    Debug.LogError(e.Message);
        //}

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
