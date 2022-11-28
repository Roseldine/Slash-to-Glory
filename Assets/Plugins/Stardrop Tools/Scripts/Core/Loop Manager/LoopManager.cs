
/// <summary>
/// Class that runs all Monobehaviour Updates. Minimize Update calls by subscribing other class's Update Handlers to the LoopManager Events:
/// <para> OnUpdate, OnLateUpdate, OnFixedUpdate </para>
/// <para> Can also stop or resume updating by calling PauseLoop() or ResumeLoop(), respectively </para>
/// </summary>
public class LoopManager : Singleton<LoopManager>
{
    public bool IsInitialized { get; private set; }

    public static readonly GameEvent OnAwake = new GameEvent();
    public static readonly GameEvent OnStart = new GameEvent();
    public static readonly GameEvent OnUpdate = new GameEvent();
    public static readonly GameEvent OnLateUpdate = new GameEvent();
    public static readonly GameEvent OnFixedUpdate = new GameEvent();

    public static readonly GameEvent OnLoopPause = new GameEvent();
    public static readonly GameEvent OnLoopResume = new GameEvent();


    public static UnityEngine.Transform Transform;

    public bool IsPaused { get; private set; }

    public void Initialize()
    {
        if (IsInitialized)
            return;

        Transform = transform;

        IsInitialized = true;
    }


    protected override void Awake()
    {
        base.Awake();
        OnAwake?.Invoke();
    }


    private void Start() => OnStart?.Invoke();

    private void Update()
    {
        if (IsPaused)
            return;

        OnUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        if (IsPaused)
            return;

        OnFixedUpdate?.Invoke();
    }

    public void PauseLoop()
    {
        if (IsPaused == true)
            return;

        IsPaused = true;
        OnLoopPause?.Invoke();
    }

    public void ResumeLoop()
    {
        if (IsPaused == false)
            return;

        IsPaused = false;
        OnLoopResume?.Invoke();
    }
}
