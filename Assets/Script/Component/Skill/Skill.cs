using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackInfo
{
    public float inputDelayTime;
    public KeyCode needInput;  // 필요한 Input 정보
    public float nextSkillTimeOrinputWaitingTime;
    // 1) needInput 이 KeyCode.None 일 경우 : 해당 시간이 지나면 자동으로 발동된다.
    // 2) needInput 이 KeyCode.None 이 아닐 경우 : 해당 시간 안에 needInput 을 입력하면 발동된다.

    //public GameObject owner;
    public string ownerAnimationName;

    public int effectIndex;
    public Vector3 effectOffset;

    public AttackBoxInfo attackBox;
    public AdditionMovement[] additionMovementList;
}

public struct AttackBoxInfo
{
    public float delayTime;

    public Vector3 offset;
    public Vector3 size;

    public float aliveTime;
}

public struct AdditionMovement
{
    public KeyCode needInput;
    public float   duringTime;
    public Vector3 additionMovement;
}

public enum SkillDelayState
{
    Start,
    Working,
    End
}

public abstract class Skill : MonoBehaviour {

    [Header("Skill Base Info")]
    [SerializeField]
    protected float startDelayTime = 0.0f; // 스킬 사용 후 발동하기 까지 (현재 그 사이 애니메이션 없음.)
    [SerializeField]
    protected float endDelayTime = 0.0f;   // 모든 AttackInfo 처리 후 캐릭터가 Idle 상태로 가기까지 (현재 그 사이 애니메이션 없음.)
    [SerializeField]
    protected float coolTime = 0.2f;
    [SerializeField]
    protected bool isMovable = false; // 움직이면서 사용할 수 있는 스킬입니까?
    // 방향키 속성
    // ex) 앞 키를 누르고 있으면 조금 더 앞으로 감
    // ex) 뒷 키 누르면 제자리
    // ex) 아무것도 안 누르면 조금 전진

    [SerializeField] protected AttackInfo[] attackInfoList;


    protected GameObject ownCharacter;
    protected Animator ownCharacterAnimator;
    protected CharacterController ownCharacterController;

    private SkillDelayState skillDelayState;
    protected float timer;


    public void SetOwnCharacter(GameObject _own)
    {
        ownCharacter = _own;
        ownCharacterAnimator = ownCharacter.GetComponent<Animator>();
        ownCharacterController = ownCharacter.GetComponent<CharacterController>();
    }

    // You have to this function In Work() function.
    protected void SetSkillDelayStateToEnd()
    {
        timer = 0.0f;
        skillDelayState = SkillDelayState.End;
    }

    protected virtual void Awake()
    {
        skillDelayState = SkillDelayState.Start;
        timer = 0.0f;
    }

    // Use this for initialization
    protected virtual void Start () {
		
	}

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (ownCharacter == null)
            return;


        switch (skillDelayState)
        {
            case SkillDelayState.Start:
                UpdateStartDelayTimer();
                break;

            case SkillDelayState.Working:
                Work();
                break;

            case SkillDelayState.End:
                UpdateEndDelayTimer();
                break;
        }
    }

    protected GameObject CreateAttackBox(AttackInfo _attackInfo)
    {
        if (ownCharacter == null)
            return null;

        GameObject attackBox
            = Instantiate(ObjectMgr.commonObjectList[(int)ObjectMgr.CommonObjectType.AttackBox]
                , ownCharacter.transform.position
                , ownCharacter.transform.rotation);

        attackBox.GetComponent<AttackBox>().SetAttackBoxInfo(_attackInfo.attackBox);

        attackBox.transform.position += (attackBox.transform.right * _attackInfo.attackBox.offset.x);
        attackBox.transform.position += (attackBox.transform.up * _attackInfo.attackBox.offset.y);
        attackBox.transform.position += (attackBox.transform.forward * _attackInfo.attackBox.offset.z);


        GameObject effect
            = Instantiate(ObjectMgr.commonEffectList[_attackInfo.effectIndex]
                , ownCharacter.transform.position
                , ownCharacter.transform.rotation);

        effect.transform.position += (effect.transform.right * _attackInfo.effectOffset.x);
        effect.transform.position += (effect.transform.up * _attackInfo.effectOffset.y);
        effect.transform.position += (effect.transform.forward * _attackInfo.effectOffset.z);


        return attackBox;
    }


    private float additionMovementTimer = -1.0f;
    private Vector3 additionMovementPerFixedTime;

    protected bool SetAdditionMovement(AdditionMovement[] _additionMovementInfoList)
    {
        if (isMovable == false)
            return false;

        int infoNum = _additionMovementInfoList.Length;

        for (int i = 0; i < infoNum; ++i)
        {
            if(_additionMovementInfoList[i].needInput == KeyCode.None)
            {
                // (Need a modification) 인풋 관련된 건 다 싹다 다꿔야 할 듯
                // 그리고 이동 관련 인풋 누르고 있는지 아닌지 확인하는 함수 만들기
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) 
                    || Input.GetKey(KeyCode.A)  || Input.GetKey(KeyCode.D) )
                    continue;
            }
            else
            {
                if (Input.GetKey(_additionMovementInfoList[i].needInput) == false)
                    continue;
            }


            additionMovementTimer = _additionMovementInfoList[i].duringTime;
            additionMovementPerFixedTime
                = (_additionMovementInfoList[i].additionMovement * Time.fixedDeltaTime) / additionMovementTimer;
            break;
        }

        return true;
    }

    protected void UpdateAdditionMovement()
    {
        if (additionMovementTimer < 0.0f)
            return;


        additionMovementTimer -= Time.fixedDeltaTime;

        ownCharacter.transform.position += (ownCharacter.transform.right * additionMovementPerFixedTime.x);
        ownCharacter.transform.position += (ownCharacter.transform.up * additionMovementPerFixedTime.y);
        ownCharacter.transform.position += (ownCharacter.transform.forward * additionMovementPerFixedTime.z);

        if (additionMovementTimer <= 0.0f)
            additionMovementTimer = -1.0f;
    }

    protected bool UpdateStartDelayTimer()
    {
        //if (skillDelayState != SkillDelayState.Start)
        //    return false;


        timer += Time.fixedDeltaTime; 
        
        if(timer >= startDelayTime)
        {
            skillDelayState = SkillDelayState.Working;
            timer = 0.0f;
            return false;
        }

        return true;
    }

    protected bool UpdateEndDelayTimer()
    {
        //if (skillDelayState != SkillDelayState.End)
        //    return false;


        timer += Time.fixedDeltaTime;

        if (timer >= endDelayTime)
        {
            
            timer = 0.0f;
            Finish();
            return false;
        }

        return true;
    }

    protected virtual void Work()
    {
        // If the function of work is done, you have to call a function
        // of SetSkillDelayStateToEnd() in it.

        UpdateAdditionMovement();
    }

    protected virtual void Finish()
    {
        // (Need a modification) 스크립트에서 정보 다 가져와야합니다.
        ownCharacter.GetComponent<CharacterSkillController>().StartCoolTimer(SkillInputType.Button_ML, coolTime);

        Destroy(this.gameObject); // Destroy(bin); // temp
    }
}
