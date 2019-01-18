using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour {
	
    [SerializeField]
	private Transform Content;
    [SerializeField]
    private Texture[] CharactersMat;

    public static Texture CharacterMat;

	void Awake () {
		init();
	}


	void init()
	{
        for(int i = 0; i < CharactersMat.Length; i++)
		{
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefab/CharaSelectButton"), Content);
            obj.GetComponent<CharacterSelectButton>().CharactorNum = i;
		}
        ChangeCharactorByIndex(0);
	}

    public void ChangeCharactorByIndex(int index)
    {
        CharacterMat = CharactersMat[index];
    }
}
