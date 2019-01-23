using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{


    UIManager UIMan;
    GameObject playerGameObj;
    float HorizontalDistance;

    public static GameManager instance;

    private PlayerMove playerMove;
    private GameManagerSettings settings;
    private int currentTime;
    private float gameStartTime;

    public bool isStarted { get; protected set; }
    public int LifeTime { get; protected set; }
    public float Score { get; protected set; }
    public float MaxVertical { get; protected set; }
    public float MinVerTical { get; protected set; }
    public int StartCountDownNum { get; protected set; }
    public float currentTIme { get; protected set; }
    public Texture CharacterMat { get; protected set; }

    void OnEnable()
    {
        init();
        if (UIMan == null)
            Debug.LogError("UIMan is NotFound!");
        MaxVertical = HorizontalDistance;
        MinVerTical = -HorizontalDistance;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void init()
    {
        Debug.Log("GameManager init");
        if (instance == null)
        {
            instance = this.gameObject.GetComponent<GameManager>();
        }
        settings            = GameManagerSettings.instance;
        UIMan               = settings.UIManager;
        playerGameObj       = settings.PlayerGameObject;
        playerMove          = settings.PlayerGameObject.GetComponent<PlayerMove>();
        HorizontalDistance  = settings.HorizontalDistance;
        CharacterMat        = CharacterSelectManager.CharacterMat;
        Score               = 0;
        StartCountDownNum   = 3;
        currentTime         = 0;
        LifeTime            = 20;
        gameStartTime       = Time.time;
        isStarted           = false;
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(StartCountDown());

        PlayerManager.Instance.ChengePlayerMat();
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "MainGameScene")
        {
            Score = playerGameObj.transform.position.y;
            if ((currentTime < (Time.time)) && isStarted)
            {
                UpdateTime();
            }

            //TODO:リザルトへの遷移
            if (LifeTime <= 0)
            {
                StartCoroutine(OnEndGame());
            }
        }
    }

    void UpdateTime()
    {
        currentTime++;
        LifeTime--;
    }

    IEnumerator OnEndGame()
    {
        // シーンが切り替わってからカンバスを切り替える
        var async = SceneManager.LoadSceneAsync(0);
        yield return async;
        GameObject.FindWithTag("StartCanvas").GetComponent<StartSceneUIManager>().OnEnableResult();
    }

    IEnumerator StartCountDown()
    {
        for (int i = 3; i > 0; i--)
        {
            yield return new WaitForSeconds(1.0f);
            StartCountDownNum--;
        }
        isStarted = true;
        gameStartTime = Time.time;
        currentTime = Mathf.FloorToInt(Time.time);
        playerMove.SetIsCanMove(true);
        WorldItemCreater.instance.isCanCreate = true;

    }

    void OnDisable()
    {
        instance = null;
    }

    #region public Method

    /// <summary>
    /// GameManagerの初期化。
    /// </summary>
    public void Init()
    {
        init();
    }

    /// <summary>
    /// LifeTimeが減る
    /// </summary>
    public void OnPlayerDamaged()
    {
        LifeTime -= 2;
        StartCoroutine(PlayerManager.Instance.DamagedAnimCoroutine());
    }
    #endregion

}
