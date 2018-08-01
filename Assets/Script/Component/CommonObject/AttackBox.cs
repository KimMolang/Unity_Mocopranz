using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AttackBox : MonoBehaviour {

    private BoxCollider boxCollider;

    private float aliveTime = 0.0f;
    private float timer = 0.0f;

    public void SetAttackBoxInfo(AttackBoxInfo _info)
    {
        aliveTime = _info.aliveTime;
        boxCollider.size = _info.size;
    }

    void Awake()
    {
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= aliveTime)
        {
            Destroy(this.gameObject);
        }
            
    }
}
