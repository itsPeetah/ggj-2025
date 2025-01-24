using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize;
    private PoolableObject[] pool;
    private int currentHead = 0;

    private void Start()
    {
        pool = new PoolableObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(prefab).GetComponent<PoolableObject>();
            pool[i].Disable();
        }
    }

    public PoolableObject GetNext()
    {
        int h = currentHead;
        currentHead = (currentHead + 1) % poolSize;
        if (pool[h].isActive) pool[h].Disable();
        return pool[h];
    }
}
