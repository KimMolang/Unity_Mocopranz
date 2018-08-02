using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FSMPlayer))]
public class CharacterController : MonoBehaviour {

    [Header("Ratating And Moving")]
    [SerializeField] [Range(1.0f, 10.0f)] private float mouseSensitivity = 8.0f;

    [SerializeField] [Range(0.0f, 80.0f)] private float turnSpeed = 30.0f;
    [SerializeField] private float moveSpeed_forward = 8.0f;
    [SerializeField] private float moveSpeed_side = 5.0f;

    private FSMPlayer fsmPlyer;

    private void Awake()
    {
        fsmPlyer = this.gameObject.GetComponent<FSMPlayer>();
    }

    // Use this for initialization
    private void Start () {
		
	}

    // Update is called once per frame
    private void Update () {
		
	}

    private void FixedUpdate()
    {
        CheckKey();

        UpdateRotation();
        UpdateMoving();
    }


    private void CheckKey()
    {
        // 이 데이터를 스크립트에 넣으면 각 해당하는 스킬 마다
        // 초기화하는 작업 해야겠어용
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ObjectMgr.CreateSkill("Skill_MeleeBase", this.gameObject);
        }
    }

    Vector3 curRatationRadian = new Vector3(0.0f, 0.0f, 0.0f);

    private void UpdateRotation()
    {
        float fHorizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        //float fVerticalRotation = -1 * (Input.GetAxis("Mouse Y") * mouseSensitivity);
        // 이건 UI에 사용하자

        curRatationRadian.y += fHorizontalRotation * turnSpeed * Time.fixedDeltaTime;


        transform.rotation = Quaternion.Euler
            ( 0.0f
            , transform.rotation.y + curRatationRadian.y
            , 0.0f);
    }

    private void UpdateMoving()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h == 0.0f && v == 0.0f)
        {
            fsmPlyer.SetState(CharacterState.Idle, CharacterAnimationState.Idle);
            return;
        }


        Vector3 movement = (transform.forward * v) + (transform.right * h);

        CharacterAnimationState animState
            = CharacterAnimationState.Moving_F;

        bool isFrondSide = (Input.GetKey(KeyCode.W)) ? true : false;
        float moveAngle = Vector3.Angle(movement, transform.right);

        float moveSpeed = (Input.GetKey(KeyCode.W)) ? moveSpeed_forward : moveSpeed_side;

        //Debug.Log(moveAngle);
        if (180.0f <= moveAngle)
        {
            animState = CharacterAnimationState.Moving_L;
        }
        else if(135.0f <= moveAngle)
        {
            if (isFrondSide)
                animState = CharacterAnimationState.Moving_FL;
            else
                animState = CharacterAnimationState.Moving_BL;
        }
        else if (90.0f <= moveAngle)
        {
            if (isFrondSide)
                animState = CharacterAnimationState.Moving_F;
            else
                animState = CharacterAnimationState.Moving_B;
        }
        else if (45.0f <= moveAngle)
        {
            if (isFrondSide)
                animState = CharacterAnimationState.Moving_FR;
            else
                animState = CharacterAnimationState.Moving_BR;
        }
        else if(0.0f <= moveAngle)
        {
            animState = CharacterAnimationState.Moving_R;
        }
        

        movement = movement.normalized * moveSpeed * Time.fixedDeltaTime;
        transform.position += movement;

        fsmPlyer.SetState(CharacterState.Moving, animState);
    }
}
