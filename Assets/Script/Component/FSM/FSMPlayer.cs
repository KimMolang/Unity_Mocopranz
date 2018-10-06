using UnityEngine;
using System.Collections;


[RequireComponent(typeof(CharacterSkillController))]
public class FSMPlayer : FSMBase
{
    //CharacterSkillController charSkillController;


    protected override void Awake()
    {
        base.Awake();

        //charSkillController = this.gameObject.GetComponent<CharacterSkillController>();
    }

    protected override IEnumerator Idle()
    {
        switch (characterAnimationState)
        {
            case CharacterAnimationState.Idle:
                animator.CrossFade(characterAnimationState.ToString(), 0.1f);
                break;

            case CharacterAnimationState.Idle_NotingInput:
                break;
        }


        do
        {
            yield return null;

            

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
                animator.CrossFade(characterAnimationState.ToString(), 0.1f);
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