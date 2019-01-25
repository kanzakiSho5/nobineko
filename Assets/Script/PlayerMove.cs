using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    UIManager UIMan;
    GameObject handObj;
    GameObject headObj;
    bool isBoosting = false;
    bool isInvincible = false;
    bool isCanControll = true;
    bool isCanMove = false;
    float moveSpeed;
    Gyroscope gyro;

    [SerializeField]
    float MoveSpeed = .1f;

    public static PlayerMove instance;

    // Use this for initialization
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = MoveSpeed;
        if (isBoosting) moveSpeed *= 10f;

        if (isCanMove)
        {
            //#if UNITY_EDITOR
            //    Move();
            //#elif UNITY_IOS
                GyroMove(); 
            //#endif
        }
    }

    private void init()
    {
        if (instance == null)
            instance = this.GetComponent<PlayerMove>();
        handObj = transform.GetChild(0).gameObject;
        headObj = transform.GetChild(1).gameObject;
        gyro = Input.gyro;
        gyro.enabled = true;
    }

    void GyroMove()
    {
        var rotRH = Input.gyro.attitude;
        var rot = new Quaternion(-rotRH.x, -rotRH.z, -rotRH.y, rotRH.w) * Quaternion.Euler(90f, 0f, 0f);
        float RotateAngleZ = Mathf.Clamp(rot.eulerAngles.z, 0, 45);
        if (rot.eulerAngles.z > 180) RotateAngleZ = Mathf.Clamp(rot.eulerAngles.z, 315, 360);

        headObj.transform.eulerAngles = new Vector3(0, 0, -RotateAngleZ);
        handObj.transform.eulerAngles = new Vector3(0, 0, -RotateAngleZ);

        Vector3 MoveDir = headObj.transform.up;//new Vector3(Mathf.Sin(-rot.eulerAngles.z),Mathf.Cos(-rot.eulerAngles.z),0);
        MoveDir.Normalize();
        if (!isCanControll) MoveDir = Vector3.up;
        transform.position += MoveDir * 0.1f * moveSpeed * Time.deltaTime;
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float UpSpeed = transform.position.y * .001f + .5f;
        UpSpeed = Mathf.Floor(UpSpeed * 10) / 10;
        //UpSpeed = Mathf.Pow(UpSpeed, 2);
        UpSpeed = Mathf.Clamp(UpSpeed, 0, 5);
        Vector3 MoveDir = new Vector3(h, .5f, 0);
        MoveDir.Normalize();
        MoveDir.Scale(new Vector3(UpSpeed, UpSpeed, 0));
        Vector3 axis = Vector3.forward;
        float angle = Mathf.Atan2(MoveDir.y, MoveDir.x);
        angle = angle * Mathf.Rad2Deg - 90;

        transform.position += MoveDir * 0.1f * moveSpeed * Time.deltaTime;

        transform.position = new Vector3(
          Mathf.Clamp(
            transform.position.x,
            GameManager.instance.MinVerTical,
            GameManager.instance.MaxVertical),
          transform.position.y,
          0);

        handObj.transform.rotation = Quaternion.AngleAxis(angle, axis);
        headObj.transform.rotation = Quaternion.AngleAxis(angle, axis);

    }

    public void startPlayerBoost(int boostItemvalue)
    {
        if (!isInvincible)
        {
            isCanControll = false;
            isBoosting = true;
            isInvincible = true;
            StartCoroutine(endBoostTimeCoroutine(1.5f * boostItemvalue));

        }
    }

    public void endPlayerBoost()
    {
        isCanControll = true;
        isBoosting = false;
        isInvincible = false;
        GameManager.instance.DestroyBoostItem();
    }

    public void SetIsCanMove(bool SetValue)
    {
        isCanMove = SetValue;
    }

    public bool GetIsBoosting()
    {
        return isBoosting;
    }

    IEnumerator endBoostTimeCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        endPlayerBoost();
    }
}
