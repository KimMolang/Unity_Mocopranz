using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour {

    // (수정)
    // Skill_Melee 의 AttackBoxInfo 랑 너무 겹치는데.. 어떻게 통일 안 되나요
    public float aliveTime = 0.0f;

    private float timer = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer >= aliveTime)
            Destroy(this.gameObject);
    }
}
