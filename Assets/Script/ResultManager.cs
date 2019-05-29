using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI ScoreText;

    private void OnEnable()
    {
        MenuSoundManager.instance.OnPlaySound((int)SoundName.ResultSE);
        ScoreText.text = (Mathf.Floor(GameManager.instance.Score * 10) / 10f) + " m";
    }
}
