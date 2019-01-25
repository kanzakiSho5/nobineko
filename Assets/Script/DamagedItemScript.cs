using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DamagedItemScript : MonoBehaviour {

    [NonSerialized]
    public bool isRightMove;

    [SerializeField]
    private float MoveSpeed = 2.0f;

    float MoveDir;


    public void Init()
    {
        //trueなら1、falseなら-1
        MoveDir = Convert.ToInt32(isRightMove) * 2 - 1;
        transform.eulerAngles = new Vector3(0.0f, 90f * MoveDir, 0.0f);
    }

    void FixedUpdate()
    {
        transform.position += new Vector3 (MoveSpeed * MoveDir, 0.0f, 0.0f) * 0.1f;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            GameManager.instance.OnPlayerDamaged();

        }
    }
}
