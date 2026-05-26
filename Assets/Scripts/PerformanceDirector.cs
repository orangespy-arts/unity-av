using UnityEngine;

public class PerformanceDirector : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SceneStateManager.Instance.SetState(VisualState.Organic);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SceneStateManager.Instance.SetState(VisualState.Architecture);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SceneStateManager.Instance.SetState(VisualState.Cosmos);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SceneStateManager.Instance.SetState(VisualState.Chaos);
    }
}
