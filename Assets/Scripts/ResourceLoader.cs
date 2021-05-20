using UnityEngine;

public static class ResourceLoader
{
    public static GameObject GetMonsterPrefab()
    {
        return monsterPrefab;
    }
    
    public static GameObject GetTargetPrefab()
    {
        return targetPrefab;
    }

    public static void Initialize()
    {
        monsterPrefab = Resources.Load<GameObject>("Prefabs/Units/Monster");
        targetPrefab = Resources.Load<GameObject>("Prefabs/Units/target");
    }


    private static GameObject monsterPrefab;
    private static GameObject targetPrefab;
}