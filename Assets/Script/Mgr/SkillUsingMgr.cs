using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using CommonObjectName;
using ResourceInformation;


public class SkillUsingMgr : SingletonBase<ObjectMgr>
{
    public struct CharacterSkillInfo
    {
        public ResourceInformation.Character.ControllableCharacter charIndex;
        public GameObject[] skills;
    }

    private CharacterSkillInfo charSkillInfo;


    protected SkillUsingMgr() { Init(); }

    private void Init()
    {
        // 캐릭터 체크 먼저 해야함
        RoadMainPlayerCharacterSkills();
    }

    private void RoadMainPlayerCharacterSkills()
    {
        // 해당 캐릭터의 정보 읽어와서 (정보는 stateMgr에 있습니다)
        // 해당 캐릭터의 스킬 정보를 가져와 할당합니다.
    }
}
