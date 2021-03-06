﻿
namespace CharacterState
{
    public enum CharacterBasicState
    {
        Idle,
        Moving,

        Attack,
        Damaged,

        WakeUp, // 기상 중
                //Unbeatable, // 무적

        Die
    }

    public enum ChracterDamagedState
    {
        None,

        Normal,
        Stiffen,

        Down,
    }

    // 상태이상
    // 버프
    // 디버프

    public enum CharacterAnimationState
    {
        Idle,
        Idle_NotingInput,

        Moving_F,
        Moving_B,
        Moving_L,
        Moving_R,

        Moving_FL,
        Moving_FR,
        Moving_BL,
        Moving_BR,

        Attack_ML,
        Attack_MR,
        Attack_Q,
        Attack_E,
        Attack_Shift,
        Attack_Space,

        Stiffen,    // 경직
                    //NnockBack,
        Down,

        Die
    }

}