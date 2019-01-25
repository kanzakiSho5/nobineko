using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager Instance;

    [SerializeField]
    private GameObject HeadObj;
    [SerializeField]
    private GameObject BottomObj;
    [SerializeField]
    private GameObject eyeL;
    [SerializeField]
    private GameObject eyeR;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = gameObject.GetComponent<PlayerManager>();
        }
    }

    private void Start()
    {
        
    }

    public void ChengePlayerMat()
    {
        HeadObj.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", GameManager.instance.CharacterMat);
        BottomObj.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", GameManager.instance.CharacterMat);
        eyeL.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", GameManager.instance.CharacterMat);
        eyeR.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", GameManager.instance.CharacterMat);
    }

    public IEnumerator DamagedAnimCoroutine()
    {
        int isHide = 0;
        Color currentColor = HeadObj.GetComponent<MeshRenderer>().material.color;
        for (int i = 0; i < 6;i++)
        {
            if (GameManager.instance.LifeTime <= 0)
                break;
            Color color = new Color(currentColor.r, currentColor.g, currentColor.b, isHide);
            HeadObj.GetComponent<MeshRenderer>().material.color = color;
            eyeL.GetComponent<MeshRenderer>().material.color = color;
            eyeR.GetComponent<MeshRenderer>().material.color = color;
            gameObject.GetComponent<MeshRenderer>().material.color = color;
            isHide++;
            isHide %= 2;
            yield return new WaitForSeconds(.2f);
        }
        GameManager.instance.OnEndDamageAnim();
    }
}
