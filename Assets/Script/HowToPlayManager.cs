using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayManager : MonoBehaviour {
    public static HowToPlayManager instance;

    private int Currentpage;
    private Image image;

    [SerializeField]
    private Sprite[] Img;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void init()
    {
        image = gameObject.transform.GetChild(0).GetComponent<Image>();
        Currentpage = 0;
        image.sprite = Img[Currentpage];
        transform.GetChild(0).GetComponent<Animator>().SetBool("isPlay", true);
    }

    public void OnClickNextPage()
    {
        Currentpage++;
        if (Currentpage >= Img.Length)
        {
            Hide();
            return;
        }

        image.sprite = Img[Currentpage];
    }

    private void OnEnable()
    {
        init();
    }

    private void Hide()
    {
        StartSceneUIManager.instance.OnEnableStartScene();
        gameObject.SetActive(false);
    }

    private void Show()
    {
        Currentpage = 0;
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        instance = null;
    }
}
