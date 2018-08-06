using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FSMPlayer))]
public class CharacterSkillController : MonoBehaviour {

    private FSMPlayer fsmPlyer;
    private float[] coolTimer;

    private void Awake()
    {
        fsmPlyer = this.gameObject.GetComponent<FSMPlayer>();

        // cool timer
        coolTimer = new float[(int)SkillInputType.MAX];
        ClearAllCoolTime();
    }

    public void SetCoolTime(SkillInputType _skillInputType, float _coolTime)
    {
        coolTimer[(int)_skillInputType] = _coolTime;
    }

    public void ClearAllCoolTime()
    {
        for (int i = 0; i < coolTimer.Length; ++i)
        {
            coolTimer[i] = -1.0f;
        }
    }

    // Use this for initialization
    private void Start () {
		
	}

    // Update is called once per frame
    private void Update () {
		
	}

    private void FixedUpdate()
    {
        UpdateSkillCoolTime();
        UpdateAimPosition();

        CheckSkillKey();
    }

    private void UpdateSkillCoolTime()
    {
        for (int i = 0; i < coolTimer.Length; ++i)
        {
            if (coolTimer[i] < 0.0f)
                continue;


            coolTimer[i] -= Time.fixedDeltaTime;

            if (coolTimer[i] <= 0.0f)
                coolTimer[i] = -1.0f;
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
            if(coolTimer[0] < 0.0f) // coolTimer[0] -1 일 경우 스킬 사용 가능 상태
            {
                ObjectMgr.CreateSkill("Skill_MeleeBase", this.gameObject);
                this.SetCoolTime(SkillInputType.Button_ML, 5.0f); // test
            }
        }
    }
}
