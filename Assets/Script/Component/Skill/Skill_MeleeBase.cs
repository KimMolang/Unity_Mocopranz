using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_MeleeBase : Skill
{
    [Header("Continuous Attack Info")]
    [SerializeField] private int continuousAttackNum = 3;

    [SerializeField] private AttackInfo[] attackInfoList;


    private int curContinuousAttackCnt = 0;
    private float timer = 0.0f;


    void Awake()
    {
        // (Need a modify)
        // 이 데이터를 스크립트에 넣으면 각 해당하는 스킬 마다
        // 초기화하는 작업 해야겠어용
        attackInfoList = new AttackInfo[continuousAttackNum];
        

        AttackBoxInfo attackBoxInfo = new AttackBoxInfo();
        attackBoxInfo.offset = new Vector3(0.0f, 1.0f, 1.0f);
        attackBoxInfo.size = new Vector3(1.0f, 1.0f, 1.0f);
        attackBoxInfo.aliveTime = 1.0f;

        AttackInfo attackInfo = new AttackInfo();
        attackInfo.nextInputWatingTime = 0.0f;
        attackInfo.attackBox = attackBoxInfo;
        attackInfo.effectIndex = 0;
        attackInfo.effectOffset = new Vector3(0.0f, 1.0f, 1.0f);

        attackInfoList[0] = attackInfo;

        attackInfo.nextInputWatingTime = 1.0f;
        attackInfoList[1] = attackInfo;

        attackInfo.nextInputWatingTime = 0.8f;
        attackInfoList[2] = attackInfo;
    }

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ownCharacter == null)
            return;


        timer += Time.deltaTime;

        AttackInfo attacInfo = attackInfoList[curContinuousAttackCnt];

        // (수정) 키 입력 시 발동 되도록
        if ( timer >= attacInfo.nextInputWatingTime )
        {
            timer = 0.0f;

            CreateAttackBox(attacInfo);
            ++curContinuousAttackCnt;
        }

        if (curContinuousAttackCnt >= continuousAttackNum)
        {
            Destroy(this.gameObject); // Destroy(bin);
        }
    }
}
