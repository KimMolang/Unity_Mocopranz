using UnityEngine;
using System.Collections;


public class FSMPlayer : FSMBase
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected void Update()
    {
        //if (CHState == CharacterState.Idle
        //    && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)
        //    || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))
        //{
        //    SetState(CharacterState.Moving);
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

            switch (characterAnimationState)
            {
                case CharacterAnimationState.Idle:
                    break;

                case CharacterAnimationState.Idle_NotingInput:
                    break;
            }

        } while (!isNewCharacterState);
    }

    protected virtual IEnumerator Moving()
    {
        do
        {
            yield return null;

            //if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)
            //    || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))
            //{
            //    SetState(CharacterState.Idle);
            //}

        } while (!isNewCharacterState);
    }

    protected virtual IEnumerator Attack()
    {
        do
        {
            yield return null;

            //// (수정) 어택박스 생성하고 사라지면 해제
            //SetState(CharacterState.Idle);
        } while (!isNewCharacterState);
    }
}