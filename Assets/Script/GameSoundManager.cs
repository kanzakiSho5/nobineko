using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour {
    
    public static GameSoundManager instance;

    [Header("BGM")]
    [SerializeField]
    private AudioClip BGM;

    [Header("SE")]
    [SerializeField]
    private AudioClip BoostSE;
    [SerializeField]
    private AudioClip ItemSE;
    [SerializeField]
    private AudioClip DamageSE;
    [SerializeField]
    private AudioClip EnemySE;
    [SerializeField]
    private AudioClip AddTimeSE;

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
        switch (sound)
        {
            case (int)SoundName.BoostSE:
                source[0].PlayOneShot(BoostSE);
                break;
            case (int)SoundName.ItemSE:
                source[0].PlayOneShot(ItemSE);
                break;
            case (int)SoundName.DamageSE:
                source[0].PlayOneShot(DamageSE);
                break;
            case (int)SoundName.AddTimeSE:
                source[0].PlayOneShot(AddTimeSE);
                break;
            default:
                Debug.LogError("Sound Missing!");
                break;
        }

    }

    public AudioClip GetEnemySound()
    {
        return EnemySE;
    }
}
