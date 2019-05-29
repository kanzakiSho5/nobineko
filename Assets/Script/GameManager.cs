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
    private bool isAddTime;
    private bool isDamaged;

    public bool isStarted        { get; protected set; }
    public int LifeTime          { get; protected set; }
    public int BoostItemCount    { get; protected set; }
    public int StartCountDownNum { get; protected set; }
    public float Score           { get; protected set; }
    public float MaxVertical     { get; protected set; }
    public float MinVerTical     { get; protected set; }
    public float currentTIme     { get; protected set; }
    public Texture CharacterMat  { get; protected set; }

    void OnEnable()
    {
        if (instance == null)
        {
            instance = this.gameObject.GetComponent<GameManager>();
        }
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void init()
    {

        settings            = GameManagerSettings.instance;
        UIMan               = settings.UIManager;
        playerGameObj       = settings.PlayerGameObject;
        playerMove          = settings.PlayerGameObject.GetComponent<PlayerMove>();
        HorizontalDistance  = settings.HorizontalDistance;
        CharacterMat        = CharacterSelectManager.CharacterMat;
        Score               = 0;
        BoostItemCount      = 0;
        StartCountDownNum   = 3;
        currentTime         = 0;
        LifeTime            = 30;
        isStarted           = false;
        isAddTime           = false;
        MaxVertical         = HorizontalDistance;
        MinVerTical         = -HorizontalDistance;
        if (UIMan == null)
            Debug.LogError("UIMan is NotFound!");
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
            if (isStarted)
            {

                if ((currentTime < (Time.timeSinceLevelLoad)))
                {
                    UpdateTime();
                }

                if (!isAddTime)
                {
                    isAddTime = false; 

                    if (((Mathf.FloorToInt(Score) % 100) == 0) && Mathf.FloorToInt(Score) != 0)
                    {
                        GameSoundManager.instance.OnPlaySound((int)SoundName.AddTimeSE);
                        LifeTime += 10;
                        isAddTime = true;
                    }
                }

                if (((Mathf.FloorToInt(Score) % 100) != 0))
                {
                    isAddTime = false;
                }
            }


        }
    }

    void UpdateTime()
    {
        currentTime++;
        LifeTime--;

        if (LifeTime <= 0)
        {
            StartCoroutine(OnEndGame());
        }
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
        for (int i = 3; i >= 0; i--)
        {
            Debug.Log("StartCountDown =" + StartCountDownNum);
            StartCountDownNum--;
            yield return new WaitForSeconds(1.0f);
        }
        isStarted = true;
        currentTime = Mathf.FloorToInt(Time.timeSinceLevelLoad);
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
        if(!isDamaged)
        {
            GameSoundManager.instance.OnPlaySound((int)SoundName.DamageSE);
            isDamaged = true;
            LifeTime -= 2;
            StartCoroutine(PlayerManager.Instance.DamagedAnimCoroutine());
        }

    }

    public void DestroyBoostItem()
    {
        BoostItemCount = 0;
    }

    /// <summary>
    /// 当たり判定を有効化
    /// </summary>
    public void OnEndDamageAnim()
    {
        isDamaged = false;
    }

    public void OnGetBoostItem()
    {
        GameSoundManager.instance.OnPlaySound((int)SoundName.ItemSE);
        BoostItemCount++;
        UIMan.OnChengeBoostItemText(BoostItemCount.ToString());
    }
    #endregion

}
