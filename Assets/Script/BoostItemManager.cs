using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItemManager : MonoBehaviour {
	

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            col.GetComponent<PlayerMove>().startPlayerboost();
            Destroy(gameObject);
        }
    }
}
