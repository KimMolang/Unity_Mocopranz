using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AttackBox : MonoBehaviour {

    private BoxCollider boxCollider;

    //private GameObject effectHitSomething;

    private float delayTime = 0.0f;
    private float aliveTime = 0.0f;
    private float timer = 0.0f;

    public void SetAttackBoxInfo(AttackBoxInfo _info)
    {
        delayTime = _info.delayTime;
        boxCollider.size = _info.size;
        aliveTime = _info.delayTime + _info.aliveTime;
    }

    void Awake()
    {
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if(this.enabled == false && timer >= delayTime)
        {
            boxCollider.enabled = true;
        }

        if (timer >= aliveTime)
        {
            Destroy(this.gameObject);
        }
            
    }
}
