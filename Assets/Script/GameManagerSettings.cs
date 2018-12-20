using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSettings : MonoBehaviour {

    [SerializeField]
    UIManager UIMan;
    [SerializeField]
    GameObject playerGameObj;
    [SerializeField][Range(5, 20)]
    float m_HorizontalDistance;

    public UIManager UIManager { get { return UIMan; }}
    public GameObject PlayerGameObject { get { return playerGameObj; }}
    public float HorizontalDistance { get { return m_HorizontalDistance; }}

    public static GameManagerSettings instance;


    private void Awake()
    {
        if (instance == null) instance = this;

        GameObject Manager = GameObject.FindWithTag("GameManager");
        if (Manager == null) {
            Manager = Instantiate(new GameObject());
            Manager.tag = "GameManager";
            Manager.AddComponent<GameManager>();
        }
        GameManager.instance.Init();
    }
}
