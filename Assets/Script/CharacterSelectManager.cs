//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour {
	[SerializeField]
	private Transform Content;
	private int CharacterCount = 10;

	// Use this for initialization
	void Start () {
		init();
	}

	// Update is called once per frame
	void Update () {

	}

	void init()
	{
		for(int i = 0; i < CharacterCount; i++)
		{
			Instantiate(Resources.Load<GameObject>("Prefab/CharaSelectButton"), Content);
		}
	}
}
