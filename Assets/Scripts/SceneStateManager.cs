using System;
using UnityEngine;

public enum VisualState { Organic, Architecture, Cosmos, Chaos }

public class SceneStateManager : MonoBehaviour
{
    public static SceneStateManager Instance { get; private set; }

    [SerializeField] float transitionDuration = 2f;

    public VisualState CurrentState  { get; private set; }
    public VisualState PreviousState { get; private set; }
    public float TransitionProgress  { get; private set; } = 1f;
    public bool IsTransitioning => TransitionProgress < 1f;

    public event Action<VisualState> OnStateChanged;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        TransitionProgress = 1f;
    }

    public void SetState(VisualState newState)
    {
        if (newState == CurrentState) return;
        PreviousState = CurrentState;
        CurrentState = newState;
        TransitionProgress = 0f;
        OnStateChanged?.Invoke(CurrentState);
        Debug.Log($"[SceneStateManager] {PreviousState} → {CurrentState}");
    }

    void Update()
    {
        if (TransitionProgress < 1f)
            TransitionProgress = Mathf.MoveTowards(TransitionProgress, 1f, Time.deltaTime / transitionDuration);
    }

    // 返回某个状态当前的混合权重 (0-1)
    public float GetStateWeight(VisualState state)
    {
        if (state == CurrentState)  return TransitionProgress;
        if (state == PreviousState) return 1f - TransitionProgress;
        return 0f;
    }
}
