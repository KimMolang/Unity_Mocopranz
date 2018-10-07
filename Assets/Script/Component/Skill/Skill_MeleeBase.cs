using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_MeleeBase : Skill
{
    [Header("Continuous Attack Info")]
    [SerializeField] private int continuousAttackNum = 3;


    private int curContinuousAttackCnt = 0;


    protected override void Awake()
    {
        base.Awake();
        // (Need a modification)
        // 이 데이터를 스크립트에 넣으면 각 해당하는 스킬 마다
        // 초기화하는 작업 해야겠어용

        //startDelayTime = 3.0f;
        endDelayTime = 0.5f;
        coolTime = 0.0f;

        attackInfoList = new AttackInfo[continuousAttackNum];
        

        AttackBoxInfo attackBoxInfo = new AttackBoxInfo();
        attackBoxInfo.offset = new Vector3(0.0f, 1.0f, 1.0f);
        attackBoxInfo.size = new Vector3(1.0f, 1.0f, 1.0f);
        attackBoxInfo.aliveTime = 1.0f;

        AttackInfo attackInfo = new AttackInfo();
        attackInfo.inputDelayTime = 0.0f;
        attackInfo.nextSkillTimeOrinputWaitingTime = 0.0f;
        attackInfo.needInput = KeyCode.None;
        attackInfo.attackBox = attackBoxInfo;
        attackInfo.effectIndex = 0;
        attackInfo.effectOffset = new Vector3(0.0f, 1.0f, 1.0f);
        attackInfo.ownerAnimationName = "ML_0";
        attackInfo.needInput = KeyCode.None;

        attackInfoList[0] = attackInfo;

        attackInfo.inputDelayTime = 0.1f;
        attackInfo.nextSkillTimeOrinputWaitingTime = 0.9f;
        attackInfo.needInput = KeyCode.Mouse0;
        attackInfo.ownerAnimationName = "ML_1";
        attackInfoList[1] = attackInfo;

        attackInfo.inputDelayTime = 0.1f;
        attackInfo.nextSkillTimeOrinputWaitingTime = 0.9f;
        attackInfo.needInput = KeyCode.Mouse0;
        attackInfo.ownerAnimationName = "ML_2";
        attackInfoList[2] = attackInfo;


        // (Need a modification)
        // AdditionMovement Test
        isMovable = true;

        AdditionMovement additionMovementInfo_none;
        additionMovementInfo_none.needInput = KeyCode.None;
        additionMovementInfo_none.duringTime = 0.2f;
        additionMovementInfo_none.additionMovement = new Vector3(0.0f, 0.0f, 0.5f);

        AdditionMovement additionMovementInfo_w;
        additionMovementInfo_w.needInput = KeyCode.W;
        additionMovementInfo_w.duringTime = 0.2f;
        additionMovementInfo_w.additionMovement = new Vector3(0.0f, 0.0f, 1.0f);

        AdditionMovement additionMovementInfo_s;
        additionMovementInfo_s.needInput = KeyCode.S;
        additionMovementInfo_s.duringTime = 0.2f;
        additionMovementInfo_s.additionMovement = new Vector3(0.0f, 0.0f, 0.0f);

        attackInfoList[0].additionMovementList = new AdditionMovement[3];
        attackInfoList[0].additionMovementList[0] = additionMovementInfo_none;
        attackInfoList[0].additionMovementList[1] = additionMovementInfo_w;
        attackInfoList[0].additionMovementList[2] = additionMovementInfo_s;

        attackInfoList[1].additionMovementList = new AdditionMovement[3];
        attackInfoList[1].additionMovementList[0] = additionMovementInfo_none;
        attackInfoList[1].additionMovementList[1] = additionMovementInfo_w;
        attackInfoList[1].additionMovementList[2] = additionMovementInfo_s;

        attackInfoList[2].additionMovementList = new AdditionMovement[3];
        attackInfoList[2].additionMovementList[0] = additionMovementInfo_none;
        attackInfoList[2].additionMovementList[1] = additionMovementInfo_w;
        attackInfoList[2].additionMovementList[2] = additionMovementInfo_s;
    }

    // Use this for initialization
    //protected override void Start ()
    //{
    //      base.Start();
    //}

    protected override void Work()
    {
        timer += Time.fixedDeltaTime;


        AttackInfo attacInfo = attackInfoList[curContinuousAttackCnt];
        bool isReadyToReleasAttack = false;

        // 1) Unneed user key input.
        if (attacInfo.needInput == KeyCode.None)
        {
            // Process it automatically.
            if (timer >= attacInfo.nextSkillTimeOrinputWaitingTime)
            {
                isReadyToReleasAttack = true;
            }
        }
        else // 2) Need user key input
        {
            // Key must be entered within the attacInfo.waitingTime.
            if (attacInfo.inputDelayTime <= timer && timer < attacInfo.nextSkillTimeOrinputWaitingTime)
            {
                if (Input.GetKeyDown(attacInfo.needInput))
                {
                    isReadyToReleasAttack = true;
                }
            }
            else if (timer >= attacInfo.nextSkillTimeOrinputWaitingTime)
            {
                //Debug.Log(attacInfo.waitingTime);
                curContinuousAttackCnt = continuousAttackNum;

                // Reduce endDelayTime as player wait for the key input
                endDelayTime -= attacInfo.nextSkillTimeOrinputWaitingTime;
            }
        }

        // CreateAttackBox
        if (isReadyToReleasAttack)
        {
            timer = 0.0f;

            CreateAttackBox(attacInfo);
            SetAdditionMovement(attacInfo.additionMovementList);

            ownCharacterAnimator.CrossFade(attacInfo.ownerAnimationName, 0.1f);
            ++curContinuousAttackCnt;
        }


        if (curContinuousAttackCnt >= continuousAttackNum)
        {
            // If the function of work is done, you have to call this function.
            SetSkillDelayStateToEnd();
        }


        base.Work();
    }

    protected override void Finish()
    {
        base.Finish();
    }
}
