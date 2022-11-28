
namespace StardropTools
{
    /// <summary>
    /// Class that finds all child objects with the IManager interface and calls its methods in the following order : InitializeManager(), LateInitializeManager()
    /// <para> Note : NOT a singleton </para>
    /// </summary>
    public class ManagerInitializer : BaseComponent
    {
        IManager[] managers;

        public override void Initialize()
        {
            base.Initialize();
            InitializeManagers();
        }

        protected virtual void InitializeManagers()
        {
            managers = Utilities.GetItems<IManager>(transform).ToArray();

            Utilities.InitializeManagers(managers);
            Utilities.LateInitializeManagers(managers);
        }
    }
}