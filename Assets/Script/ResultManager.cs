﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {

    [SerializeField]
    Text ScoreText;

    private void OnEnable()
    {
        ScoreText.text = GameManager.instance.Score + " m!";
    }
}