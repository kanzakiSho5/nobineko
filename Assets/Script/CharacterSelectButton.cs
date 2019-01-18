using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectButton : MonoBehaviour {

    public int CharactorNum;

    private CharacterSelectManager manager;

	
	void OnEnable () {
        manager = GameObject.FindWithTag("CharacerSelectManager").GetComponent<CharacterSelectManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickButton(){
        manager.ChangeCharactorByIndex(CharactorNum);
        GameObject.FindWithTag("StartCanvas").GetComponent<StartSceneUIManager>().ToggleCharacterSelectAndStartSceneChenger();
    }
}
