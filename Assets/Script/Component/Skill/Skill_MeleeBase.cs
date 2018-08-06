using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_MeleeBase : Skill
{
    [Header("Continuous Attack Info")]
    [SerializeField] private int continuousAttackNum = 3;

    [SerializeField] private AttackInfo[] attackInfoList;


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
        attackInfo.waitingTime = 0.0f;
        attackInfo.needInput = KeyCode.None;
        attackInfo.attackBox = attackBoxInfo;
        attackInfo.effectIndex = 0;
        attackInfo.effectOffset = new Vector3(0.0f, 1.0f, 1.0f);
        attackInfo.ownerAnimationName = "ML_0";
        attackInfo.needInput = KeyCode.None;

        attackInfoList[0] = attackInfo;

        attackInfo.waitingTime = 1.0f;
        attackInfo.needInput = KeyCode.Mouse0;
        attackInfo.ownerAnimationName = "ML_1";
        attackInfoList[1] = attackInfo;

        attackInfo.waitingTime = 1.0f;
        attackInfo.needInput = KeyCode.Mouse0;
        attackInfo.ownerAnimationName = "ML_2";
        attackInfoList[2] = attackInfo;
    }

    // Use this for initialization
    protected override void Start ()
    {

    }

    protected override void Work()
    {
        timer += Time.fixedDeltaTime;


        AttackInfo attacInfo = attackInfoList[curContinuousAttackCnt];
        bool isReadyToReleasAttack = false;

        // 1) Unneed user key input.
        if (attacInfo.needInput == KeyCode.None)
        {
            // Process it automatically.
            if (timer >= attacInfo.waitingTime)
            {
                isReadyToReleasAttack = true;
            }
        }
        else // 2) Need user key input
        {
            // Key must be entered within the attacInfo.waitingTime.
            if (timer < attacInfo.waitingTime)
            {
                if (Input.GetKeyDown(attacInfo.needInput))
                {
                    isReadyToReleasAttack = true;
                }
            }
            else if (timer >= attacInfo.waitingTime)
            {
                //Debug.Log(attacInfo.waitingTime);
                curContinuousAttackCnt = continuousAttackNum;

                // Reduce endDelayTime as player wait for the key input
                endDelayTime -= attacInfo.waitingTime;
            }
        }

        // CreateAttackBox
        if (isReadyToReleasAttack)
        {
            timer = 0.0f;

            CreateAttackBox(attacInfo);

            ownCharacterAnimator.CrossFade(attacInfo.ownerAnimationName, 0.1f);
            ++curContinuousAttackCnt;
        }


        if (curContinuousAttackCnt >= continuousAttackNum)
        {
            // If working is done, you have to call this function.
            SetSkillDelayStateToEnd();
        }
    }

    protected override void Finish()
    {
        base.Finish();
    }
}
