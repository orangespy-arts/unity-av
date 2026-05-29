using UnityEngine;

[RequireComponent(typeof(TunnelGenerator))]
public class TunnelDeformer : MonoBehaviour
{
    [SerializeField] float bassScale = 2f;
    [SerializeField] float waveFrequency = 3f;   // 沿隧道方向的波数
    [SerializeField] float waveSpeed = 1f;        // 波沿Z轴传播速度

    TunnelGenerator tunnel;
    Vector3[] deformedVerts;
    float debugTimer;

    void Awake()
    {
        tunnel = GetComponent<TunnelGenerator>();
    }

    void Start()
    {
        deformedVerts = (Vector3[])tunnel.BaseVertices.Clone();
    }

    void Update()
    {
        if (AudioAnalyzer.Instance == null)
        {
            Debug.LogWarning("[TunnelDeformer] AudioAnalyzer.Instance is null");
            return;
        }

        float bass = AudioAnalyzer.Instance.Bass;

        debugTimer += Time.deltaTime;
        if (debugTimer >= 1f)
        {
            debugTimer = 0f;
            Debug.Log($"[TunnelDeformer] Bass={bass:F6}  displacement={bass * bassScale:F4}");
        }
        var   base_ = tunnel.BaseVertices;
        int   segments = tunnel.Mesh.vertexCount / (base_.Length / base_.Length); // 用原始顶点数

        for (int i = 0; i < base_.Length; i++)
        {
            Vector3 v = base_[i];
            float radialDir = Mathf.Sqrt(v.x * v.x + v.y * v.y); // 原始半径（归一化用）
            if (radialDir < 0.001f) { deformedVerts[i] = v; continue; }

            // 沿Z轴传播的波，叠加在径向上
            float wave = Mathf.Sin(v.z * waveFrequency - Time.time * waveSpeed);
            float displacement = bass * bassScale * (1f + wave * 0.5f);

            Vector3 radial = new Vector3(v.x, v.y, 0f).normalized;
            deformedVerts[i] = v + radial * displacement;
        }

        tunnel.Mesh.vertices = deformedVerts;
        tunnel.Mesh.RecalculateNormals();
    }
}
