using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItemManager : MonoBehaviour {
	

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            GameManager.instance.OnGetBoostItem();
            Destroy(gameObject);
        }
    }
}
