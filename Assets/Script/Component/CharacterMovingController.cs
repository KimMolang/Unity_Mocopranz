using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FSMPlayer))]
[RequireComponent(typeof(CharacterSkillController))]
public class CharacterMovingController : MonoBehaviour {

    [Header("Ratating And Moving")]
    [SerializeField] [Range(1.0f, 10.0f)] private float mouseSensitivity = 8.0f;

    [SerializeField] [Range(0.0f, 80.0f)] private float turnSpeed = 30.0f;
    [SerializeField] private float moveSpeed_forward = 8.0f;
    [SerializeField] private float moveSpeed_side = 5.0f;

    private FSMPlayer fsmPlyer;
    private CharacterSkillController charSkillController;

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
        UpdateRotation();
        UpdateMoving();
    }


    Vector3 curRatationRadian = new Vector3(0.0f, 0.0f, 0.0f);

    private void UpdateRotation()
    {
        // Check This character able to rotate
        if (fsmPlyer.IsAbleToRotate() == false)
            return;

        float fHorizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;

        curRatationRadian.y += fHorizontalRotation * turnSpeed * Time.fixedDeltaTime;


        transform.rotation = Quaternion.Euler
            ( 0.0f
            , transform.rotation.y + curRatationRadian.y
            , 0.0f);
    }

    private void UpdateMoving()
    {
        // Check This character able to move
        if (fsmPlyer.IsAbleToMove() == false)
            return;
        

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
