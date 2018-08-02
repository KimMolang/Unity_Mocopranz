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
        switch (characterAnimationState)
        {
            case CharacterAnimationState.Moving_F:
            case CharacterAnimationState.Moving_B:
            case CharacterAnimationState.Moving_L:
            case CharacterAnimationState.Moving_R:
            case CharacterAnimationState.Moving_FL:
            case CharacterAnimationState.Moving_FR:
            case CharacterAnimationState.Moving_BL:
            case CharacterAnimationState.Moving_BR:
                animator.CrossFade(characterAnimationState.ToString(), 0.5f);
                break;
        }

        do
        {
            yield return null;

        } while (!isNewCharacterState);
    }

    protected virtual IEnumerator Attack()
    {
        do
        {
            yield return null;

        } while (!isNewCharacterState);
    }
}