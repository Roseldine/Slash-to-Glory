

namespace StardropTools
{
    /// <summary>
    /// Base component from which most of Stardrop Tools game object scripts derive from
    /// <para> Contains important methods such as Initialize(), StartTick() and Tick(), which makes starting component behaviour more modular, providing flexible alternatives to Awake()/Start() and Update(). </para>
    /// <para> It is recommended to use StartTick() when you want to initialize an Update, and place your update logic inside the Tick() method. Both methods invoke events and the Tick() is invoked/updated by the LoopManager every frame. If you want to stop the update, use StopTick(). </para>
    /// </summary>
    public class BaseComponent : UnityEngine.MonoBehaviour
    {
        [UnityEngine.Header("Base Component")]
        [UnityEngine.SerializeField] protected BaseComponentData baseData;

        protected bool StopUpdateOnDisable { get => baseData.stopUpdateOnDisable; set => baseData.stopUpdateOnDisable = value; }

        public bool IsInitialized { get; protected set; }
        public bool IsLateInitialized { get; protected set; }

        public bool IsUpdating { get; protected set; }
        public bool IsFixedUpdating { get; protected set; }
        public bool IsLateUpdating { get; protected set; }


        #region Events

        /// <summary>
        /// Event fired when Initialize() is called
        /// </summary>
        public readonly GameEvent OnInitialize = new GameEvent();

        /// <summary>
        /// Event fired when LateInitialize() is called
        /// </summary>
        public readonly GameEvent OnLateInitialize = new GameEvent();

        /// <summary>
        /// Event fired when HandleUpdate() is called
        /// </summary>
        public readonly GameEvent OnUpdate = new GameEvent();

        /// <summary>
        /// Event fired when HandFixedUpdate() is called
        /// </summary>
        public readonly GameEvent OnFixedUpdate = new GameEvent();

        /// <summary>
        /// Event fired when HandleLateUpdate() is called
        /// </summary>
        public readonly GameEvent OnLateUpdate = new GameEvent();

        /// <summary>
        /// Event fired when object is Enabled, OnEnable()
        /// </summary>
        public readonly GameEvent OnEnabled = new GameEvent();

        /// <summary>
        /// Event fired when object is Disabled, OnDisable()
        /// </summary>
        public readonly GameEvent OnDisabled = new GameEvent();

        /// <summary>
        /// Event fired when ResetObject() is called
        /// </summary>
        public readonly GameEvent OnResetObject = new GameEvent();

        #endregion // events

        #region Print & Debug.log
        /// <summary>
        /// substitute to Debug.Log();
        /// </summary>
        public static void Print(object message) => UnityEngine.Debug.Log(message);

        /// <summary>
        /// substitute to Debug.LogWarning();
        /// </summary>
        public static void PrintWarning(object message) => UnityEngine.Debug.LogWarning(message);
        #endregion // print

        /// <summary>
        /// Method that substitutes Awake() or Start(), so that it can be called by other classes when you want, instead of it being automatic. IT CAN ONLY BE CALLED ONCE!
        /// <para>The strength in this is that you can implement the class Setup here and invoke it when needed, and not only when the Game Object first apears in the hierarquy</para>
        /// <para>Optionaly, you can chose to call this on Awake() or Start() in the Inspector</para>
        /// </summary>
        public virtual void Initialize()
        {
            if (IsInitialized)
                return;

            IsInitialized = true;
            OnInitialize?.Invoke();
        }


        /// <summary>
        /// Method that substitutes Start(), so that it can be called by other classes when you want, instead of it being automatic. IT CAN ONLY BE CALLED ONCE!
        /// <para>The strength in this is that you can implement the class Setup here and invoke it when needed, and not only when the Game Object first apears in the hierarquy</para>
        /// <para>Optionaly, you can chose to call this on Awake() or Start() in the Inspector</para>
        /// </summary>
        public virtual void LateInitialize()
        {
            if (IsLateInitialized)
                return;

            IsLateInitialized = true;
            OnLateInitialize?.Invoke();
        }


        // Start Update

        /// <summary>
        /// Subcribes Tick() to the LoopManager, invoking it every frame update
        /// <para>Best used to invoke behaviour that needs setup before being updated every Tick()</para>
        /// </summary>
        public virtual void StartUpdate()
        {
            if (IsUpdating)
                return;

            LoopManager.OnUpdate.AddListener(HandleUpdate);
            IsUpdating = true;
        }


        /// <summary>
        /// Subcribes FixedTick() to the LoopManager, invoking it every fixed frame update
        /// <para>Best used to invoke behaviour that needs setup before being updated every FixedTick()</para>
        /// </summary>
        public virtual void StartFixedTick()
        {
            if (IsFixedUpdating)
                return;

            LoopManager.OnUpdate.AddListener(HandleFixedUpdate);
            IsFixedUpdating = true;
        }


        /// <summary>
        /// Subcribes LateTick() to the LoopManager, invoking it every late frame update
        /// <para>Best used to invoke behaviour that needs setup before being updated every LateTick()</para>
        /// </summary>
        public virtual void StartLateUpdate()
        {
            if (IsLateUpdating)
                return;

            LoopManager.OnUpdate.AddListener(HandleLateTick);
            IsLateUpdating = true;
        }


        // Update
        /// <summary>
        /// Method that invokes every frame after you call StartUpdate(). It's the objects Update()
        /// <para>Can also be used to implement logic you wish to be updated by other classes like Authorative Managers without being invoked by the LoopManager</para>
        /// </summary>
        public virtual void HandleUpdate()
            => OnUpdate?.Invoke();

        /// <summary>
        /// Method that invokes every fixed frame after you call StartFixedUpdate()
        /// <para>Best used when invoked by the LoopManager, but can also be used to implement logic you wish to be fixed updated by other classes like Authorative Managers</para>
        /// </summary>
        public virtual void HandleFixedUpdate()
            => OnFixedUpdate?.Invoke();

        /// <summary>
        /// Method that invokes every late frame after you call StartLateUpdate()
        /// <para>Best used when invoked by the LoopManager, but can also be used to implement logic you wish to be late updated by other classes like Authorative Managers</para>
        /// </summary>
        public virtual void HandleLateTick()
            => OnLateUpdate?.Invoke();


        // Stop Update

        /// <summary>
        /// Removes HandleUpdate() subscription from the LoopManager
        /// <para>Best used to implement logic when you want the class to Stop Updating</para>
        /// </summary>
        public virtual void StopUpdate()
        {
            if (IsUpdating == false)
                return;

            LoopManager.OnUpdate.RemoveListener(HandleUpdate);
            IsUpdating = false;
        }

        /// <summary>
        /// Removes HandleFixedUpdate() subscription from the LoopManager
        /// <para>Best used to implement logic when you want the class to Stop Updating</para>
        /// </summary>
        public virtual void StopFixedUpdate()
        {
            if (IsFixedUpdating == false)
                return;

            LoopManager.OnUpdate.RemoveListener(HandleFixedUpdate);
            IsFixedUpdating = false;
        }

        /// <summary>
        /// Removes HandleLateUpdate() subscription from the LoopManager
        /// <para>Best used to implement logic when you want the class to Stop Updating</para>
        /// </summary>
        public virtual void StopLateUpdate()
        {
            if (IsLateUpdating == false)
                return;

            LoopManager.OnUpdate.RemoveListener(HandleLateTick);
            IsLateUpdating = false;
        }


        /// <summary>
        /// Method reserved for Resets. Ex: restore initial values
        /// </summary>
        public virtual void ResetObject()
        {
            OnResetObject?.Invoke();
        }

        // These if statements work in relation to options in the BaseData class that can be accessed via the editors' Inspector
        protected virtual void Awake()
        {
            if (baseData.InitializationAt == BaseInitialization.awake)
                Initialize();

            if (baseData.LateInitializationAt == BaseInitialization.awake)
                LateInitialize();
        }

        // These if statements work in relation to options in the BaseData class that can be accessed via the editors' Inspector
        protected virtual void Start()
        {
            if (baseData.InitializationAt == BaseInitialization.start)
                Initialize();

            if (baseData.LateInitializationAt == BaseInitialization.start)
                LateInitialize();
        }

        protected virtual void OnEnable()
        {
            OnEnabled.Invoke();
        }

        protected virtual void OnDisable()
        {
            if (StopUpdateOnDisable)
            {
                StopUpdate();
                StopFixedUpdate();
                StopLateUpdate();
            }

            OnDisabled?.Invoke();
        }

        public virtual void Reset()
        {
            IsInitialized = false;
            OnResetObject?.Invoke();
        }
    }
}