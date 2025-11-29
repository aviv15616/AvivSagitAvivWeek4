using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levelLayouts;
    private GameObject currentLayoutInstance;
    private BottleManager bottleManager;
    public BottleManager BottleManager => bottleManager;

    public int CurrentLevelIndex { get; private set; } = 0;

    public void LoadLevel()
    {
        Debug.Log($"LevelManager.LoadLevel: Loading level {CurrentLevelIndex}");
        if (currentLayoutInstance != null)
            Destroy(currentLayoutInstance);

        currentLayoutInstance = Instantiate(levelLayouts[CurrentLevelIndex]);

        bottleManager = currentLayoutInstance.GetComponentInChildren<BottleManager>(true);

        if (bottleManager == null)
            Debug.LogError($"LevelManager.LoadLevel: no BottleManager found in layout {currentLayoutInstance.name}");
    }

    public void NextLevel()
    {
        CurrentLevelIndex = (CurrentLevelIndex + 1) % levelLayouts.Length;
    }
}
