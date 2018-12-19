using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItemManager : MonoBehaviour {
	

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            col.GetComponent<PlayerMove>().startPlayerboost();
            Destroy(gameObject);
        }
    }
}
