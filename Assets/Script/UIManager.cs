using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    Text ScoreText;
    [SerializeField]
    Text TimeText;
    [SerializeField]
    Text FPSText;
    [SerializeField]
    GameObject StartCountDownPanel;

    int currentTime = 0;

	// Use this for initialization
	void Start ()
    {
        init();
	}

    void init()
    {
        StartCountDownPanel.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
    UIUpdate(Mathf.FloorToInt(GameManager.instance.Score));
    int CountDownNum = GameManager.instance.StartCountDownNum;

    if(CountDownNum > 0)
    {
      StartCountDownPanel.GetComponentInChildren<Text>().text = CountDownNum.ToString();
      currentTime = 3;
    }
    else
    {
      StartCountDownPanel.SetActive(false);
    }

    TimeText.text = GameManager.instance.LifeTime.ToString();

    DebugUpdate();
	}

  private void DebugUpdate()
  {
    FPSText.text = Mathf.Floor(1 / Time.deltaTime).ToString();
  }


  public void UIUpdate(int Score)
  {
      ScoreText.text = Score + " m!";
  }
}
