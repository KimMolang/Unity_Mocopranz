using UnityEngine;
using System.Collections;

/*
 * Character State Info
 * Update Character Animation
 */

[RequireComponent(typeof(Animator))]
public class FSMBase : MonoBehaviour
{
    protected Animator animator;

    protected CharacterState characterState;
    protected ChracterDamagedState characterDamagedState;
    protected CharacterAnimationState characterAnimationState;


    protected bool isNewCharacterState;



    protected virtual void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        characterState = CharacterState.Idle;
        characterDamagedState = ChracterDamagedState.None;
        characterAnimationState = CharacterAnimationState.Idle;

        StartCoroutine(FSMMain());
    }

    protected virtual void OnDisable()
    {
        StopCoroutine(FSMMain());
    }

    IEnumerator FSMMain()
    {
        while (true)
        {
            isNewCharacterState = false;
            yield return StartCoroutine(characterState.ToString());
        }
    }

    //개체의 상태가 바뀔때마다 메소드가 실행된다.
    public void SetState(CharacterState _newCharState, CharacterAnimationState _newCharAnimState)
    {
        if (characterState == _newCharState && characterAnimationState == _newCharAnimState)
            return;


        isNewCharacterState = true;

        characterState = _newCharState;
        characterAnimationState = _newCharAnimState;

        //개체가 가진 Animator 컴포넌트의 state Parameters 에게 상태변화 값을 전달한다.
        animator.SetInteger("State", (int)characterState);
    }

    #region public bool IsAbleTo~()
    public bool IsAbleToRotate()
    {
        switch (characterState)
        {
            case CharacterState.Idle:
                return true;

            case CharacterState.Moving:
                return true;

            case CharacterState.Attack:
                return true;

            case CharacterState.Damaged:
                switch (characterDamagedState)
                {
                    case ChracterDamagedState.None:
                        return true;

                    case ChracterDamagedState.Normal:
                        return true;

                    case ChracterDamagedState.Stiffen:
                        return false;

                    case ChracterDamagedState.Down:
                        return true;
                }
                return false;

            case CharacterState.WakeUp:
                return false;

            case CharacterState.Die:
                return false;
        }

        return false;
    }

    public bool IsAbleToMove()
    {
        switch (characterState)
        {
            case CharacterState.Idle:
                return true;

            case CharacterState.Moving:
                return true;

            case CharacterState.Attack:
                {
                    // (Need a modification)
                    // 스킬 마다 다름
                }
                return false; // tmp

            case CharacterState.Damaged:
                switch (characterDamagedState)
                {
                    case ChracterDamagedState.None:
                        return true;

                    case ChracterDamagedState.Normal:
                        return true;

                    case ChracterDamagedState.Stiffen:
                        return false;

                    case ChracterDamagedState.Down:
                        return false;
                }
                return false;

            case CharacterState.WakeUp:
                return false;

            case CharacterState.Die:
                return false;
        }

        return false;
    }

    public bool IsAbleToUseSkill()
    {
        switch (characterState)
        {
            case CharacterState.Idle:
                return true;

            case CharacterState.Moving:
                return true;

            case CharacterState.Attack:
                return false;   // 이미 다른 스킬을 사용하고 있다.

            case CharacterState.Damaged:
                switch (characterDamagedState)
                {
                    case ChracterDamagedState.None:
                        return true;

                    case ChracterDamagedState.Normal:
                        return true;

                    case ChracterDamagedState.Stiffen:
                        return false;

                    case ChracterDamagedState.Down:
                        return false;
                }
                return false;

            case CharacterState.WakeUp:
                return false;

            case CharacterState.Die:
                return false;
        }

        return false;
    }
    #endregion

    protected virtual IEnumerator Idle()
    {
        switch (characterAnimationState)
        {
            case CharacterAnimationState.Idle:
                break;

            case CharacterAnimationState.Idle_NotingInput:
                break;
        }

        do
        {
            yield return null;

        } while (!isNewCharacterState);
    }
}