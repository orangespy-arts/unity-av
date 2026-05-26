using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TunnelGenerator : MonoBehaviour
{
    [SerializeField] int rings = 64;
    [SerializeField] int segments = 24;
    [SerializeField] float radius = 3f;
    [SerializeField] float length = 80f;

    public Vector3[] BaseVertices { get; private set; }
    public Mesh Mesh { get; private set; }

    void Awake()
    {
        Mesh = BuildMesh();
        GetComponent<MeshFilter>().mesh = Mesh;
    }

    Mesh BuildMesh()
    {
        int vertCount = rings * segments;
        var verts = new Vector3[vertCount];
        var uvs   = new Vector2[vertCount];
        var tris  = new int[(rings - 1) * segments * 6];

        for (int r = 0; r < rings; r++)
        {
            float z = (float)r / (rings - 1) * length;
            for (int s = 0; s < segments; s++)
            {
                float angle = (float)s / segments * Mathf.PI * 2f;
                int idx = r * segments + s;
                verts[idx] = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, z);
                uvs[idx]   = new Vector2((float)s / segments, (float)r / rings);
            }
        }

        int t = 0;
        for (int r = 0; r < rings - 1; r++)
        {
            for (int s = 0; s < segments; s++)
            {
                int curr = r * segments + s;
                int next = r * segments + (s + 1) % segments;
                int currNext = (r + 1) * segments + s;
                int nextNext = (r + 1) * segments + (s + 1) % segments;

                // 法线朝内（摄像机在隧道内部）
                tris[t++] = curr;
                tris[t++] = currNext;
                tris[t++] = next;
                tris[t++] = next;
                tris[t++] = currNext;
                tris[t++] = nextNext;
            }
        }

        BaseVertices = (Vector3[])verts.Clone();

        var mesh = new Mesh();
        mesh.name = "Tunnel";
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices  = verts;
        mesh.uv        = uvs;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        return mesh;
    }
}
