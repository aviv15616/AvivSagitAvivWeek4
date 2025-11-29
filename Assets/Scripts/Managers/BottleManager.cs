using UnityEngine;
using System.Linq;

public class BottleManager : MonoBehaviour
{
    private Bottle[] bottles;
    public Bottle[] Bottles => bottles;

    private void Awake()
    {
        bottles = GetComponentsInChildren<Bottle>(true);
    }

    public int CountFallen()
    {
        if (bottles == null)
        {
            Debug.LogWarning($"[BottleManager.CountFallen] bottles is null on {gameObject.name} (id={GetInstanceID()})");
            return 0;
        }

        for (int i = 0; i < bottles.Length; i++)
        {
           Debug.Log($"[BottleManager.CountFallen] {gameObject.name} bottle {i} fallen: {bottles[i].IsFallen()}, up={bottles[i].transform.up}");
        }
        return bottles.Count(b => b.IsFallen());
    }

    public void RemoveFallen()
    {
        foreach (var bottle in bottles)
        {
            if (bottle.IsFallen())
                bottle.gameObject.SetActive(false);
        }
    }

    public bool AllCleared()
    {
        return bottles.All(b => !b.gameObject.activeSelf);
    }
}
