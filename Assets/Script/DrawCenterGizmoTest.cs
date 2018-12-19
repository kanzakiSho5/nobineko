using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class DrawCenterGizmoTest : MonoBehaviour {

    MeshFilter meshFilter;
    MeshRenderer renderer;
    List<Vector3> PlayerBodyCenterPos = new List<Vector3>();
    List<Vector3> PlayerBodyMeshPos = new List<Vector3>();

    [SerializeField]
    float BodyDistance = 1.0f;
    [SerializeField]
    float BodyWidth = .5f;
    [SerializeField]
    int BodyMaxVertices = 20;



    // Use this for initialization
    void Start ()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        renderer = gameObject.GetComponent<MeshRenderer>();
        PlayerBodyCenterPos.Add(Vector3.down);
        PlayerBodyMeshPos.Add(Vector3.right * BodyWidth);
        PlayerBodyMeshPos.Add(Vector3.left * BodyWidth);
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 curPlayerPos = transform.position;
        Vector2 prePlayerPos = PlayerBodyCenterPos[PlayerBodyCenterPos.Count - 1];
        if (PlayerBodyCenterPos.Count > 2)
            prePlayerPos = PlayerBodyCenterPos[PlayerBodyCenterPos.Count - 2];
        float distance = Mathf.Sqrt(Mathf.Pow(prePlayerPos.x - curPlayerPos.x, 2) + Mathf.Pow(prePlayerPos.y - curPlayerPos.y, 2));
        if (distance >= BodyDistance)
        {
            PlayerBodyCenterPos.Add(transform.position);
            createPlayerBodyMeshPos(PlayerBodyMeshPos[PlayerBodyCenterPos.Count - 1], PlayerBodyMeshPos[PlayerBodyCenterPos.Count - 2]);

            
        }
        if(PlayerBodyCenterPos.Count > BodyMaxVertices)
            PlayerBodyCenterPos.RemoveAt(0);
        if (PlayerBodyCenterPos.Count > 2)
            PlayerBodyCenterPos[PlayerBodyCenterPos.Count - 1] = transform.position;
        if (PlayerBodyMeshPos.Count > BodyMaxVertices * 2)
            PlayerBodyMeshPos.RemoveRange(0, 2);
        if (PlayerBodyCenterPos.Count > 1)
            PlayerBodyMeshPosUpdate();
        CreateBodyMesh();
    }

    private void PlayerBodyMeshPosUpdate()
    {       
        float rad = 0;
        if (PlayerBodyCenterPos.Count >= 3)
        {
            Vector2 topBodyPos = PlayerBodyCenterPos[PlayerBodyCenterPos.Count - 1];
            Vector2 preBodyPos = PlayerBodyCenterPos[PlayerBodyCenterPos.Count - 3];
            rad = Mathf.Atan2(preBodyPos.y - topBodyPos.y, preBodyPos.x - topBodyPos.x);
        }
        rad += Mathf.PI / 2;
        Vector2 pos = PlayerBodyCenterPos[PlayerBodyCenterPos.Count - 2];
        Vector2 LeftSidePos = new Vector2(
            pos.x + (Mathf.Cos(rad) * BodyWidth), 
            pos.y + (Mathf.Sin(rad) * BodyWidth));

        Vector2 RightSidePos = new Vector2(
            pos.x + (Mathf.Cos(rad + Mathf.PI) * BodyWidth),
            pos.y + (Mathf.Sin(rad + Mathf.PI) * BodyWidth));

        PlayerBodyMeshPos[PlayerBodyMeshPos.Count - 1] = LeftSidePos - pos + (Vector2)transform.position;
        PlayerBodyMeshPos[PlayerBodyMeshPos.Count - 2] = RightSidePos - pos + (Vector2)transform.position;
        PlayerBodyMeshPos[PlayerBodyMeshPos.Count - 3] = LeftSidePos;
        PlayerBodyMeshPos[PlayerBodyMeshPos.Count - 4] = RightSidePos;
    }

    private void createPlayerBodyMeshPos(Vector2 LeftPos, Vector2 RightPos)
    {
        PlayerBodyMeshPos.Add(LeftPos);
        PlayerBodyMeshPos.Add(RightPos);
    }

    private void CreateBodyMesh()
    {
        Mesh mesh = meshFilter.mesh;

        mesh.Clear();
        mesh.name = "BodyMesh";
        
        int verQuantity = PlayerBodyCenterPos.Count;
        
        Vector3[] vertices = new Vector3[verQuantity * 2];
        Vector2[] uvs = new Vector2[verQuantity * 4];


        //四角形２つは三角形３つ
        int[] triangles = new int[(verQuantity-1)*2 *3];


        for(int i = 0;i < verQuantity;i++)
        {
            vertices[i * 2 + 0] = PlayerBodyMeshPos[i * 2 + 0] - transform.position;
            vertices[i * 2 + 1] = PlayerBodyMeshPos[i * 2 + 1] - transform.position;


            uvs[i * 4 + 0] = Vector2.zero;
            uvs[i * 4 + 1] = Vector2.right;
            uvs[i * 4 + 2] = Vector2.left + Vector2.one;
            uvs[i * 4 + 3] = Vector2.one;
        }

        int positionIndex = 0;

        for (int i = 0;i < verQuantity - 1; i++)
        {
            triangles[positionIndex++] = i * 2 + 1;
            triangles[positionIndex++] = i * 2 + 0;
            triangles[positionIndex++] = (i + 1) * 2 + 0;

            triangles[positionIndex++] = (i + 1) * 2 + 0;
            triangles[positionIndex++] = (i + 1) * 2 + 1;
            triangles[positionIndex++] = i * 2 + 1;
        }

        mesh.vertices = vertices;
        //mesh.uv = uvs;
        //mesh.uv2 = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        //renderer.material = this.material;
        //renderer.material.color = Color.red;
        
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < PlayerBodyCenterPos.Count - 1; i++)
        {
            Gizmos.DrawLine(PlayerBodyCenterPos[i], PlayerBodyCenterPos[i + 1]);
            Gizmos.DrawSphere(PlayerBodyCenterPos[i],.2f);
            Gizmos.DrawLine(PlayerBodyMeshPos[i * 2 + 2], PlayerBodyMeshPos[i * 2 + 3]);
        }
    }
}
