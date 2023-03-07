using UnityEngine;
    
public class ChestModel
{
    public ChestType ChestType { get; }
    public int coins { get; }
    public string coinsRange { get; }
    public int gems { get;   }
    public string gemsRange { get; }
    
    public float unlockTime { get; set; }
    public int GemsToUnlock { get; set; }
    
    public Sprite chestLockedSprite { get; }
    public Sprite chestUnlockedSprite { get; }

    public ChestModel(ChestTypeSo chestTypeSo)
    {
        chestUnlockedSprite = chestTypeSo.unlockedChestSprite;
        chestLockedSprite = chestTypeSo.lockedChestSprite;
        ChestType = chestTypeSo.chestType;
        unlockTime = chestTypeSo.chestUnlockTime;
        GemsToUnlock = chestTypeSo.gemsToUnlock;
        
        coins = Random.Range(chestTypeSo.coinValue.min, chestTypeSo.coinValue.max);
        gems = Random.Range(chestTypeSo.gemValue.min, chestTypeSo.gemValue.max);
        
        coinsRange = $"{chestTypeSo.coinValue.min}-{chestTypeSo.coinValue.max}";
        gemsRange = $"{chestTypeSo.gemValue.min}-{chestTypeSo.gemValue.max}";
    }
}
