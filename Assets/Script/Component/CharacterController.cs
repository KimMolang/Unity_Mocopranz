using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    [Header("Ratating And Moving")]
    [SerializeField] [Range(1.0f, 10.0f)] private float mouseSensitivity = 8.0f;

    [SerializeField] [Range(0.0f, 80.0f)] private float turnSpeed = 30.0f;
    [SerializeField] private float moveSpeed_forward = 8.0f;
    [SerializeField] private float moveSpeed_side = 5.0f;

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


    private const float MAX_DIR_X = 70.0f;
    private const float MIN_DIR_X = -70.0f;

    Vector3 curRatationRadian = new Vector3(0.0f, 0.0f, 0.0f);

    private void UpdateRotation()
    {
        

        float fHorizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        float fVerticalRotation = -1 * (Input.GetAxis("Mouse Y") * mouseSensitivity);

        curRatationRadian.y += fHorizontalRotation * turnSpeed * Time.fixedDeltaTime;
        curRatationRadian.x += 0;//fVerticalRotation * m_fTurnSpeed * Time.smoothDeltaTime; // (test)

        if (curRatationRadian.x > MAX_DIR_X)
            curRatationRadian.x = MAX_DIR_X;
        else if (curRatationRadian.x < MIN_DIR_X)
            curRatationRadian.x = MIN_DIR_X;

        transform.rotation = Quaternion.Euler
            (transform.rotation.x + curRatationRadian.x
            , transform.rotation.y + curRatationRadian.y
            , 0.0f);
    }

    private void UpdateMoving()
    {
        //Debug.Log("!!");

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        float moveSpeed = moveSpeed_forward;

        if ( (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            && !(Input.GetKey(KeyCode.W)) )
        {
            moveSpeed = moveSpeed_side;
        }


        Vector3 movement = (transform.forward * v) + (transform.right * h);
        movement = movement.normalized * moveSpeed * Time.fixedDeltaTime;

        transform.position += movement;
    }
}
