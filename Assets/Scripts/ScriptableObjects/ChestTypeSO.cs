using UnityEngine;

[CreateAssetMenu(fileName = "ChestType", menuName = "ScriptableObjects/Chest")]
public class ChestTypeSo : ScriptableObject
{
    public ChestType chestType;
        
    public int chestUnlockTime;
    public int gemsToUnlock;
      
    public Range coinValue;

    public Range gemValue;
        
    public Sprite lockedChestSprite;
    public Sprite unlockedChestSprite;
}

[System.Serializable]
public class Range
{
    public int min;
    public int max;
}
