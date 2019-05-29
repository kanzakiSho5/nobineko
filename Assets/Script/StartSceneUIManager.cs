using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUIManager : MonoBehaviour
{

    public static StartSceneUIManager instance;

    private bool isEverStart = false;

    [SerializeField]
    private GameObject StartScene;
    [SerializeField]
    private GameObject CharaSelectScene;
    [SerializeField]
    private GameObject ResultScene;
    [SerializeField]
    private GameObject HowToPlay;
    [SerializeField]
    private GameObject Credit;
    [SerializeField]
    private GameObject Story;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        init();
    }

    private void init()
    {
        var value = PlayerPrefs.GetInt("isEverStart", isEverStart ? 1 : 0);
        isEverStart = value == 1;
        OnEnableStartScene();
    }

    private void HideAllWindow()
    {
        ResultScene.SetActive(false);
        CharaSelectScene.SetActive(false);
        HowToPlay.SetActive(false);
        Credit.SetActive(false);
        StartScene.SetActive(false);
        Story.SetActive(false);
    }

    public void OnDownStartGameButton()
    {
        if (!isEverStart)
        {
            isEverStart = true;
            PlayerPrefs.SetInt("isEverStart", isEverStart ? 1 : 0);
            OnEnableStory();
            return;
        }
        SceneManager.LoadScene(1);
    }

    public void ToggleCharacterSelectAndStartSceneChenger()
    {
        CharaSelectScene.SetActive(!CharaSelectScene.activeSelf);
        StartScene.SetActive(!StartScene.activeSelf);
        ResultScene.SetActive(false);
    }

    public void OnClickHowToPlayBtn()
    {
        HideAllWindow();
        HowToPlay.SetActive(true);
    }

    public void OnClickCreditBtn()
    {
        HideAllWindow();
        Credit.SetActive(true);
    }

    public void OnEnableResult()
    {
        HideAllWindow();
        ResultScene.SetActive(true);
    }

    public void OnEnableStartScene()
    {
        HideAllWindow();
        StartScene.SetActive(true);
    }

    public void OnEnableStory()
    {
        HideAllWindow();
        Story.SetActive(true);
    }

    public void OnClickStoryBtn()
    {
        SceneManager.LoadScene(1);
    }

    #region Debug
    public void ResetIsEverStarted()
    {
        isEverStart = false;
        PlayerPrefs.SetInt("isEverStart", isEverStart ? 1 : 0);
    }
    #endregion

}
