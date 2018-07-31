using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct AttackInfo
{
    public float nextInputWatingTime;

    public GameObject owner;
    public string ownerAnimationName;

    public AttackBoxInfo attackBox;
}

public struct AttackBoxInfo
{
    public Vector3 offset;
    public Vector3 size;
    public float aliveTime;
    public string effectName;
}

public class Skill_Melee : MonoBehaviour {

    [Header("Skill Base Info")]
    [SerializeField] private float startDeleyTime = 0.1f;
    [SerializeField] private float coolTime = 0.2f;

    [Header("Continuous Attack Info")]
    [SerializeField] private int continuousAttackNum = 3;

    [SerializeField] private AttackInfo[] attackInfoList;


    private int curContinuousAttackCnt = 0;
    private float timer = 0.0f;

    private GameObject ownCharacter;
    public void SetOwnCharacter(GameObject _own) { ownCharacter = _own; }

    void Awake()
    {
        // test
        // 이 데이터를 넣는 파싱 작업을 해야겠어용
        attackInfoList = new AttackInfo[continuousAttackNum];
        

        AttackBoxInfo attackBoxInfo = new AttackBoxInfo();
        attackBoxInfo.offset = new Vector3(0.0f, 0.0f, 1.0f);
        attackBoxInfo.size = new Vector3(1.0f, 1.0f, 1.0f);
        attackBoxInfo.aliveTime = 1.0f;

        AttackInfo attackInfo = new AttackInfo();
        attackInfo.nextInputWatingTime = 0.0f;
        attackInfo.attackBox = attackBoxInfo;
        attackInfo.owner = ownCharacter;

        attackInfoList[0] = attackInfo;

        attackInfo.nextInputWatingTime = 1.0f;
        attackInfoList[1] = attackInfo;

        attackInfo.nextInputWatingTime = 0.8f;
        attackInfoList[2] = attackInfo;
    }

    // Use this for initialization
    void Start ()
    {
        if (ownCharacter == null)
            return;


        AttackInfo attacInfo
            = attackInfoList[curContinuousAttackCnt];

        CreateAttackBox(attacInfo);

        ++curContinuousAttackCnt;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ownCharacter == null)
            return;


        timer += Time.deltaTime;

        AttackInfo attacInfo = attackInfoList[curContinuousAttackCnt];

        // (수정)
        if ( timer >= attacInfo.nextInputWatingTime )
        {
            timer = 0.0f;

            CreateAttackBox(attacInfo);
            ++curContinuousAttackCnt;
        }

        if (curContinuousAttackCnt >= continuousAttackNum)
            Destroy(this.gameObject); // Destroy(bin);
    }

    private GameObject CreateAttackBox(AttackInfo _attackInfo)
    {
        //if (_attackInfo.owner == null)
        //    return null;

        GameObject attackBox
            = Instantiate(ObjectMgr.commonObjectList[(int)ObjectMgr.CommonObjectType.AttackBox]
                , ownCharacter.transform.position
                , ownCharacter.transform.rotation);

        attackBox.GetComponent<AttackBox>().aliveTime = _attackInfo.attackBox.aliveTime;

        attackBox.transform.position += (attackBox.transform.right * _attackInfo.attackBox.offset.x);
        attackBox.transform.position += (attackBox.transform.up * _attackInfo.attackBox.offset.y);
        attackBox.transform.position += (attackBox.transform.forward * _attackInfo.attackBox.offset.z);

        // (수정) 사이즈도 셋팅해야함

        return attackBox;
    }
}
