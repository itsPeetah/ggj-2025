using UnityEngine;
using UnityEngine.Events;

namespace SPNK.Game.Events
{
    public class ThreeArgEventChannelSO<T0, T1, T2> : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField, TextArea] private string _description = "";
#endif

        public UnityAction<T0, T1, T2> OnEventRaised;

        public virtual void RaiseEvent(T0 arg0, T1 arg1, T2 arg2)
        {
            if (OnEventRaised != null) OnEventRaised.Invoke(arg0, arg1, arg2);
        }
    }
}
