using UnityEngine;
using UnityEngine.Events;

namespace SPNK.Game.Events
{
    public class TwoArgEventChannelSO<T0, T1> : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField, TextArea] private string _description = "";
#endif

        public UnityAction<T0, T1> OnEventRaised;

        public virtual void RaiseEvent(T0 arg0, T1 arg1)
        {
            if (OnEventRaised != null) OnEventRaised.Invoke(arg0, arg1);
        }
    }
}
