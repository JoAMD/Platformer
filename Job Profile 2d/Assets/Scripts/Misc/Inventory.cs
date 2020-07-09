using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Item[] items;
    private int currItemCount = 0;

    private void Start()
    {
        items = new Item[15];
    }

    public bool HasKey(int keyId, out int keyInvIdx)
    {
        for (int i = 0; i < items.Length && i < currItemCount; i++)
        {
            //if(items[i] != null)
            {
                if (items[i]?.id == keyId)
                {
                    keyInvIdx = i;
                    return true;
                }
            }
        }
        keyInvIdx = -1;
        return false;
    }

    public void RemoveKey(int idx)
    {
        //items[idx].NullifyItem();
        // or....
        items[idx] = null;
    }

    public void AddItem(Item item)
    {
        items[currItemCount] = item;
        currItemCount++;
    }
}
