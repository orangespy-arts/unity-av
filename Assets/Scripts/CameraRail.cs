using UnityEngine;

public class CameraRail : MonoBehaviour
{
    [SerializeField] float baseSpeed = 5f;
    [SerializeField] float rmsSpeedBoost = 10f;
    [SerializeField] float tunnelLength = 80f;   // 和 TunnelGenerator.length 保持一致

    void Update()
    {
        float speed = baseSpeed;
        if (AudioAnalyzer.Instance != null)
            speed += AudioAnalyzer.Instance.RMS * rmsSpeedBoost;

        transform.position += Vector3.forward * speed * Time.deltaTime;

        // 循环：走到头回到起点
        if (transform.position.z >= tunnelLength - 5f)
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
}
