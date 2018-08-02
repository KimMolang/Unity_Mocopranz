using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]
public class FSMBase : MonoBehaviour
{
    protected Animator animator;


    public CharacterState characterState;
    public CharacterAnimationState characterAnimationState;

    public bool isNewCharacterState;



    protected virtual void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        characterState = CharacterState.Idle;
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