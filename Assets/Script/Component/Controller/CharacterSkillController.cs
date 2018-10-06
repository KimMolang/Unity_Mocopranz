using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Aim, 스킬 관련 UI도 업데이트 해줄 예정이당

public enum SkillState
{
    Available,
    Working,
    End
}

struct SkillCheckInfo
{
    public SkillState state;

    public float coolTime;
    public float coolTimer;
}


[RequireComponent(typeof(FSMPlayer))]
public class CharacterSkillController : MonoBehaviour {

    private FSMPlayer fsmPlyer;
    private SkillCheckInfo[] skillCheckInfo;

    private void Awake()
    {
        fsmPlyer = this.gameObject.GetComponent<FSMPlayer>();

        skillCheckInfo = new SkillCheckInfo[(int)SkillInputType.MAX];
        ClearAllCoolTime();
    }

    public void StartCoolTimer(SkillInputType _skillInputType, float _coolTime)
    {
        skillCheckInfo[(int)_skillInputType].state = SkillState.End;
        skillCheckInfo[(int)_skillInputType].coolTimer = _coolTime;

        fsmPlyer.SetState(CharacterState.Idle, CharacterAnimationState.Idle);
    }

    public void ClearAllCoolTime()
    {
        for (int i = 0; i < skillCheckInfo.Length; ++i)
        {
            skillCheckInfo[i].state = SkillState.Available;
            skillCheckInfo[i].coolTimer = -1.0f;
        }
    }

    // Use this for initialization
    private void Start () {
		
	}

    private void FixedUpdate()
    {
        UpdateSkillCoolTime();
        UpdateAimPosition();

        CheckSkillKey();
    }

    private void UpdateSkillCoolTime()
    {
        for (int i = 0; i < skillCheckInfo.Length; ++i)
        {
            if (skillCheckInfo[i].state != SkillState.End)
                continue;


            skillCheckInfo[i].coolTimer -= Time.fixedDeltaTime;

            if (skillCheckInfo[i].coolTimer <= 0.0f)
            {
                skillCheckInfo[i].state = SkillState.Available;
                skillCheckInfo[i].coolTimer = -1.0f;
            }
        }
    }

    private void UpdateAimPosition()
    {
        //float fVerticalRotation = -1 * (Input.GetAxis("Mouse Y") * mouseSensitivity);
    }

    private void CheckSkillKey()
    {
        // Check This character able to use skills
        if (fsmPlyer.IsAbleToUseSkill() == false)
            return;


        // (Need a Modify)
        // 이거 다 스크립트
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(skillCheckInfo[0].state == SkillState.Available) // coolTimer[0] -1 일 경우 스킬 사용 가능 상태
            {
                ObjectMgr.CreateSkill("Skill_MeleeBase", this.gameObject);
                skillCheckInfo[0].state = SkillState.Working;

                fsmPlyer.SetState(CharacterState.Attack, CharacterAnimationState.Attack_ML);
            }
        }
    }
}
