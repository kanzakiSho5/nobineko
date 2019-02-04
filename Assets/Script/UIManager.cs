using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("CanvasObj")]
    [SerializeField]
    TextMeshProUGUI ScoreText;
    [SerializeField]
    TextMeshProUGUI TimeText;
    [SerializeField]
    TextMeshProUGUI FPSText;
    [SerializeField]
    GameObject StartCountDownPanel;
    [SerializeField]
    Button BoostButton;
    [SerializeField]
    TextMeshProUGUI BoostItemText;

    [Header("Debug")]
    [SerializeField]
    bool isDebugMode;

    int currentTime = 0;

    // Use this for initialization
    void Start()
    {
        init();
    }

    void init()
    {
        StartCountDownPanel.SetActive(true);
        FPSText.transform.parent.gameObject.SetActive(isDebugMode);
    }

    // Update is called once per frame
    void Update()
    {
        UIUpdate(Mathf.FloorToInt(GameManager.instance.Score));
        int CountDownNum = GameManager.instance.StartCountDownNum;

        if (CountDownNum > 0)
        {
            StartCountDownPanel.GetComponentInChildren<Text>().text = CountDownNum.ToString();
            currentTime = 3;
        }
        else
        {
            StartCountDownPanel.SetActive(false);
        }

        TimeText.text = GameManager.instance.LifeTime.ToString();

        if(isDebugMode)
            DebugUpdate();

        if (PlayerMove.instance.GetIsBoosting() && !BoostButton.interactable)
        {
            BoostButton.interactable = true;
            OnChengeBoostItemText("0");
        }

        if (GameManager.instance.BoostItemCount <= 0)
            BoostButton.interactable = false;
        else
            BoostButton.interactable = true;
            
    }
    private void DebugUpdate()
    {
        FPSText.text = Mathf.Floor(1 / Time.deltaTime).ToString();
    }


    public void UIUpdate(int Score)
    {
        ScoreText.text = Score + " m!";
    }

    public void OnChengeBoostItemText(string value)
    {
        BoostItemText.text = value;
    }

    public void OnBoostButton()
    {
        BoostButton.interactable = false;
        PlayerMove.instance.startPlayerBoost(GameManager.instance.BoostItemCount);
    }


}
