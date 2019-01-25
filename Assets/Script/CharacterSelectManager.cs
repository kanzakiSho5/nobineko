using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour {
	
    [SerializeField]
	private Transform Content;
    [SerializeField]
    private Texture[] CharactersMat;
    [SerializeField]
    private Sprite[] SelectTexture;
    [SerializeField]
    private string[] CharactorName;


    public static Texture CharacterMat;

	void Awake () {
		init();
	}

	void init()
	{
        for (int i = 0; i < CharactersMat.Length; i++)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefab/CharaSelectButton"), Content);
            obj.GetComponent<CharacterSelectButton>().CharactorNum = i;
            obj.transform.GetChild(1).GetComponent<Image>().sprite = SelectTexture[i];
            obj.transform.GetChild(0).GetComponent<Text>().text = CharactorName[i];
		}
        ChangeCharactorByIndex(0);
	}

    public void ChangeCharactorByIndex(int index)
    {
        CharacterMat = CharactersMat[index];
    }
}
