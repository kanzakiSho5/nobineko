using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    [SerializeField]
    GameObject Player;
    [SerializeField]
    float CameraDistance = 10.0f;

	// Update is called once per frame
	void Update () {
		if(Player != null)
        {
            transform.position = Player.transform.position + Vector3.back * CameraDistance;
        }
	}
}
