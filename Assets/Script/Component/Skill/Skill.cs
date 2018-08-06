using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackInfo
{
    public KeyCode needInput;  // 필요한 Input 정보
    public float waitingTime;
    // 1) needInput 이 KeyCode.None 일 경우 : 해당 시간이 지나면 자동으로 발동된다.
    // 2) needInput 이 KeyCode.None 이 아닐 경우 : 해당 시간 안에 needInput 을 입력하면 발동된다.

    //public GameObject owner;
    public string ownerAnimationName;

    public int effectIndex;
    public Vector3 effectOffset;

    public AttackBoxInfo attackBox;
}

public struct AttackBoxInfo
{
    public float delayTime;

    public Vector3 offset;
    public Vector3 size;

    public float aliveTime;
}

public class Skill : MonoBehaviour {

    [Header("Skill Base Info")]
    [SerializeField]
    protected float startDeleyTime = 0.0f;    // 스킬 사용 후 발동하기 까지
    [SerializeField]
    protected float latterDeleyTime = 0.0f;   // 스킬 사용 후 캐릭터가 Idle 상태로 가기까지
    [SerializeField]
    protected float coolTime = 0.2f;
    [SerializeField]
    protected bool isMovable = false; // 움직이면서 사용할 수 있는 스킬입니까?

    // 방향키 속성
    // ex) 앞 키를 누르고 있으면 조금 더 앞으로 감
    // ex) 뒷 키 누르면 제자리
    // ex) 아무것도 안 누르면 조금 전진


    protected GameObject ownCharacter;
    protected Animator ownCharacterAnimator;
    protected CharacterController ownCharacterController;

    public void SetOwnCharacter(GameObject _own)
    {
        ownCharacter = _own;
        ownCharacterAnimator = ownCharacter.GetComponent<Animator>();
        ownCharacterController = ownCharacter.GetComponent<CharacterController>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
