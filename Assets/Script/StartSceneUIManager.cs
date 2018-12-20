using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUIManager : MonoBehaviour {
	[SerializeField]
	private GameObject StartScene;
	[SerializeField]
	private GameObject CharaSelectScene;
    [SerializeField]
    private GameObject ResultScene;

	public void OnDownStartGameButton()
	{
        SceneManager.LoadScene(1);
	}

	public void TogleCharacterSelectAndStartSceneChenger()
	{
		CharaSelectScene.SetActive(!CharaSelectScene.activeSelf);
		StartScene.SetActive(!StartScene.activeSelf);
        ResultScene.SetActive(false);
	}


    public void OnEnableResult()
    {
        ResultScene.SetActive(true);

        CharaSelectScene.SetActive(false);
        StartScene.SetActive(false);
    }

    public void OnEnableStartScene()
    {
        ResultScene.SetActive(false);
        CharaSelectScene.SetActive(false);
        StartScene.SetActive(true);
    }

}
