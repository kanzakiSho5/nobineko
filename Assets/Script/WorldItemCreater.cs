using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItemCreater : MonoBehaviour {

    GameObject ItemPearentObj;
    GameObject BoostPearent;
    GameObject DamagePearent;

    int BoostItemCount;
    int DamageItemCount;

    //[System.NonSerialized]
    public bool isCanCreate;

    public static WorldItemCreater instance;

    private void Awake()
    {
        init();
    }


    void init()
    {
        if (instance == null)
            instance = this.GetComponent<WorldItemCreater>();
        
        ItemPearentObj = new GameObject();
        ItemPearentObj.name = "ItemPearent";
        ItemPearentObj.transform.parent = transform;
        ItemPearentObj.transform.localPosition = Vector3.zero;

        BoostPearent = new GameObject();
        BoostPearent.name = "BoostItem";
        BoostPearent.transform.parent = ItemPearentObj.transform;

        DamagePearent = new GameObject();
        DamagePearent.name = "DamageItem";
        DamagePearent.transform.parent = ItemPearentObj.transform;

        BoostItemCount = 0;
        DamageItemCount = 0;
        isCanCreate = false;
    }

    void createBoostObj(float hight)
    {
        GameObject item = Instantiate((GameObject)Resources.Load("Prefab/BoostItem"), BoostPearent.transform);
        item.transform.position = new Vector3(Random.Range(-10,10),hight + 10,0);
    }

    void createDamageObj(float hight)
    {
        GameObject item         = Instantiate((GameObject)Resources.Load("Prefab/DamageItem"), DamagePearent.transform);
        int rand                = Random.Range(0, 2);
        item.GetComponent<DamagedItemScript>().isRightMove = (rand == 0);
        Vector3 RespawnPos      = new Vector3((rand * 2 - 1) * (GameManager.instance.MaxVertical + 10), hight + 1, 0);
        item.transform.position = RespawnPos;
        item.GetComponent<DamagedItemScript>().Init();
    }

	void Update () {
        if(GameManager.instance.isStarted && isCanCreate)
        {
            BoostPearent.SetActive(false);
            DamagePearent.SetActive(false);
            if (!PlayerMove.instance.GetIsBoosting())
            {
                BoostPearent.SetActive(true);
                DamagePearent.SetActive(true);

                if (Time.timeSinceLevelLoad >= BoostItemCount * 2)
                {
                    BoostItemCount++;
                    createBoostObj(Mathf.FloorToInt(GameManager.instance.Score));
                }

                if (Time.timeSinceLevelLoad >= DamageItemCount * 3)
                {
                    DamageItemCount++;
                    createDamageObj(Mathf.FloorToInt(GameManager.instance.Score + 5f));
                }

                if (BoostPearent.transform.childCount > 5)
                {
                    Destroy(BoostPearent.transform.GetChild(0).gameObject);
                }

                if (DamagePearent.transform.childCount > 5)
                {
                    Destroy(DamagePearent.transform.GetChild(0).gameObject);
                }
            }
        }
	}

    private void OnDisable()
    {
        instance = null;
    }
}
