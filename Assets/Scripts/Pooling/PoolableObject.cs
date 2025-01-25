using UnityEngine;

// This should be an interface...
public abstract class PoolableObject : MonoBehaviour
{
    [HideInInspector] public bool isActive = true;
    //public float repoolAfter = 5;
    //private float depooledAt = 0;

    private void Update()
    {
       //if (Time.time - depooledAt >= repoolAfter)
       //{
       //    Disable();
       //}
    }

    public virtual void Enable()
    {
        isActive = true;
        gameObject.SetActive(true);
        //depooledAt = Time.time;
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
        isActive = false;
    }
}
