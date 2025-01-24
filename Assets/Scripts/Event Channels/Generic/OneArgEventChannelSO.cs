using UnityEngine;
using UnityEngine.Events;

namespace SPNK.Game.Events
{
    public class OneArgEventChannelSO<T0> : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField, TextArea] private string _description = "";
#endif

        public UnityAction<T0> OnEventRaised;

        public virtual void RaiseEvent(T0 arg0)
        {
            if (OnEventRaised != null) OnEventRaised.Invoke(arg0);
        }
    }
}
