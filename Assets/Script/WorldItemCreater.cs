using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItemCreater : MonoBehaviour {

    GameObject ItemPearentObj;

    int currentTime;

	// Use this for initialization
	void Start () {
        ItemPearentObj = new GameObject();
        ItemPearentObj.name = "ItemPearent";
        ItemPearentObj.transform.parent = transform;
        createObj(10);
	}

    void init()
    {

    }

    void createObj(int hight)
    {
        GameObject item = Instantiate((GameObject)Resources.Load("Prefab/BoostItem"), ItemPearentObj.transform);

        item.transform.position = new Vector3(Random.Range(-10,10),hight + 10,0);
    }

	// Update is called once per frame
	void Update () {

		if(Time.time >= currentTime * 2)
        {
            currentTime++;
            createObj(Mathf.FloorToInt(GameManager.instance.Score));

        }

        if(ItemPearentObj.transform.childCount > 5)
        {
            Destroy(ItemPearentObj.transform.GetChild(0).gameObject);
        }
	}
}
