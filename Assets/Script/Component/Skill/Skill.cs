using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackInfo
{
    public float nextInputWatingTime;

    //public GameObject owner;
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

public class Skill : MonoBehaviour {

    [Header("Skill Base Info")]
    [SerializeField]
    private float startDeleyTime = 0.1f;
    [SerializeField]
    private float coolTime = 0.2f;


    protected GameObject ownCharacter;
    public void SetOwnCharacter(GameObject _own) { ownCharacter = _own; }

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

        attackBox.GetComponent<AttackBox>().aliveTime = _attackInfo.attackBox.aliveTime;

        attackBox.transform.position += (attackBox.transform.right * _attackInfo.attackBox.offset.x);
        attackBox.transform.position += (attackBox.transform.up * _attackInfo.attackBox.offset.y);
        attackBox.transform.position += (attackBox.transform.forward * _attackInfo.attackBox.offset.z);

        // (수정) 사이즈도 셋팅해야함

        return attackBox;
    }
}
