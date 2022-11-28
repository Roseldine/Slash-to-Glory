
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Used in conjunction with an Animator Component. Detects animation events based on event ID
    /// </summary>
    public class AnimationEventDetector : MonoBehaviour
    {
        [SerializeField] bool debug;

        public readonly GameEvent<int> OnAnimEventINT = new GameEvent<int>();
        public readonly GameEvent<string> OnAnimEventSTRING = new GameEvent<string>();

        public void AnimEventInt(int eventID)
        {
            if (debug)
                Debug.LogFormat("INT Anim event: {0}, detected!", eventID);

            OnAnimEventINT?.Invoke(eventID);
        }

        public void AnimEventString(string eventString)
        {
            if (debug)
                Debug.LogFormat("STRING Anim event: {0}, detected!", eventString);

            OnAnimEventSTRING?.Invoke(eventString);
        }
    }
}
