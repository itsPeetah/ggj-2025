using UnityEngine;
using UnityEngine.Events;

namespace SPNK.Game.Events
{
    [CreateAssetMenu(menuName = "Event Channels/Primitive/Void")]
    public class VoidEventChannelSO : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField, TextArea] private string description = "";
#endif
        public UnityAction OnEventRaised;

        public void RaiseEvent()
        {
            if(OnEventRaised != null) OnEventRaised.Invoke();
        }
    }
}
