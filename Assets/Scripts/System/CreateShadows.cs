using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateShadows : MonoBehaviour
{

    public GameObject lightmeshholder;

    private int RaysToShoot = 10240;
    public int distance = 60;
    private Vector3[] vertices;
    private Vector2[] vertices2d;
    private int[] triangles;
    private Mesh mesh;

    // Use this for initialization
    void Start()
    {
        vertices2d = new Vector2[RaysToShoot];
        mesh = lightmeshholder.GetComponent<MeshFilter>().mesh;
        lightmeshholder.layer = 13;
        BuildMesh();
    }

    // Update is called once per frame
    void Update()
    {
        vertices = mesh.vertices;
        mesh.RecalculateBounds();

        float angle = 0;
        for (int i = 0; i < RaysToShoot; i++)
        {
            var x = Mathf.Sin(angle);
            var y = Mathf.Cos(angle);
            angle += 2 * Mathf.PI / RaysToShoot;

            Vector3 dir = new Vector3(x, y, 0);
            RaycastHit2D hit;
            if ((hit = Physics2D.Raycast(transform.position, dir, distance, (1 << 0) | (1<<9) | (1<<11) | (1<<12))))
            {
                //Debug.DrawLine (transform.position, hit.point, new Color(1,1,0,1));
                var tmp = lightmeshholder.transform.InverseTransformPoint(hit.point);
                vertices[i] = new Vector3(tmp.x, tmp.y, 0);
            }
            else { // no hit
                //Debug.DrawRay (transform.position, dir*distance, new Color(1,1,0,1));
                var tmp2 = lightmeshholder.transform.InverseTransformPoint(lightmeshholder.transform.position + dir);
                vertices[i] = new Vector3(tmp2.x, tmp2.y, 0);
            }
        }

        // last vertice is at the player location (center point)
        vertices[vertices.Length - 1] = lightmeshholder.transform.InverseTransformPoint(transform.position);

        mesh.vertices = vertices;

    }

    void BuildMesh()
    {

        float angle = 0;
        for (int j = 0; j < RaysToShoot; j++)
        {
            var x = Mathf.Sin(angle);
            var y = Mathf.Cos(angle);
            angle += 2 * Mathf.PI / RaysToShoot;

            Vector3 dir = new Vector3(x, 0, y);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, distance, (1 << 0) | (1 << 9) | (1 << 11) | (1 << 12)))
            {

                var tmp = lightmeshholder.transform.InverseTransformPoint(hit.point);
                vertices2d[j] = new Vector2(tmp.x, tmp.z);


            }
            else
            {
                var tmp2 = lightmeshholder.transform.InverseTransformPoint(lightmeshholder.transform.position + dir * distance);
                vertices2d[j] = new Vector2(tmp2.x, tmp2.z);
            }
        }

        // build mesh
        Vector2[] uvs = new Vector2[vertices2d.Length + 1];
        Vector3[] newvertices = new Vector3[vertices2d.Length + 1];
        for (int n = 0; n < newvertices.Length - 1; n++)
        {
            newvertices[n] = new Vector3(vertices2d[n].x, 0, vertices2d[n].y);
        }

        triangles = new int[newvertices.Length * 3];

        // triangle list
        int i = -1;

        for (int n = 0; n < triangles.Length - 3; n += 3)
        {
            i++;
            triangles[n + 2] = newvertices.Length - 1;
            if (i >= newvertices.Length)
            {
                triangles[n + 1] = 0;
            }
            else {
                triangles[n + 1] = i + 1;
            }
            triangles[n] = i;
        }
        i++;
        // central point
        newvertices[newvertices.Length - 1] = new Vector3(0, 0, 0);
        triangles[triangles.Length - 1] = newvertices.Length - 1;
        triangles[triangles.Length - 2] = 0;
        triangles[triangles.Length - 3] = i - 1;

        // Create the mesh
        mesh.vertices = newvertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

    }
}
