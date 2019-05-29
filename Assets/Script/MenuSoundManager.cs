using System;
using UnityEngine;

[System.Serializable]
public enum SoundName{
    EnterSE,
    CatSE,
    CanselSE,
    ResultSE,
    BoostSE,
    ItemSE,
    DamageSE,
    EnemySE,
    AddTimeSE
}

public class MenuSoundManager : MonoBehaviour {
    public static MenuSoundManager instance;


    [Header("SE")]
    [SerializeField]
    AudioClip EnterSE;
    [SerializeField]
    AudioClip CatSE;
    [SerializeField]
    AudioClip CanselSE;
    [SerializeField]
    AudioClip ResultSE;


    private AudioSource[] source;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        init();
    }

    private void init()
    {
        source = GetComponents<AudioSource>();
    }

    private void OnDisable()
    {
        instance = null;
    }

    [EnumAction(typeof(SoundName))]
    public void OnPlaySound(int sound)
    {
        //SoundName sound = SoundName.EnterSE;
        switch(sound)
        {
            case (int)SoundName.EnterSE:
                source[0].PlayOneShot(EnterSE);
                break;
            case (int)SoundName.CanselSE:
                source[0].PlayOneShot(CanselSE);
                break;
            case (int)SoundName.CatSE:
                source[0].PlayOneShot(CatSE);
                break;
            case (int)SoundName.ResultSE:
                source[0].PlayOneShot(ResultSE);
                break;
            default:
                Debug.LogError("Sound Null!");
                break;
        }

    }


}

