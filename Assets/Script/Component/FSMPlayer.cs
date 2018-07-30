using UnityEngine;
using System.Collections;

//FSMPlayer 는 FSMBase 로 부터 상속받는다. (FSMPlayer 는 FSMBase 의 코드 내용을 담는다 라고 생각하면 된다.)
public class FSMPlayer : FSMBase
{
    //캐릭터 파라메타
    public int currentHP = 100;
    public int maxHP = 100;
    public int exp = 0;
    public int level = 1;
    public int gold = 0;
    public float attack = 40.0f;  // 공격력
    public float attackRange = 1.5f; // 공격범위


    protected override void Awake() //Awake문 전체 추가
    {
        base.Awake();
    }

    protected void Update()
    {
        //if (CHState == CharacterState.Idle
        //    && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)
        //    || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)) )
        //{
        //    SetState(CharacterState.Run);
        //}

        //if (Input.GetKeyDown(KeyCode.Mouse0) )
        //{
        //    SetState(CharacterState.Attack);
        //}
    }

    protected override IEnumerator Idle()
    {
        do
        {
            yield return null;
        } while (!isNewState);
    }

    protected virtual IEnumerator Run()
    {
        do
        {
            yield return null;

            //if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)
            //    || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))
            //{
            //    SetState(CharacterState.Idle);
            //}

        } while (!isNewState);
    }

    protected virtual IEnumerator Attack()
    {
        do
        {
            yield return null;

            //// (수정) 어택박스 생성하고 사라지면 해제
            //SetState(CharacterState.Idle);
        } while (!isNewState);
    }
}