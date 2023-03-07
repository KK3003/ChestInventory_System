using UnityEngine;


public class ChestController
{
    private ChestModel chestModel { get; }
    private ChestView chestView { get; }

    public ChestStates currentState { get; set; }

    public ChestController(ChestModel chestModel, ChestView chestView)
    {
        this.chestModel = chestModel;
        this.chestView = Object.Instantiate(chestView);

        this.chestView.Initialize(this, this.chestModel.unlockTime);
        currentState = ChestStates.Locked;
        PromptManager.Instance.DisplayChestInfo(this.chestModel.ChestType.ToString(), this.chestModel.coinsRange, this.chestModel.gemsRange);
        this.chestView.ShowChest(this.chestModel.unlockTime, this.chestModel.ChestType, this.chestModel.chestLockedSprite, this.chestModel.chestUnlockedSprite);
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        chestView.OnChestButtonPressed += ChestButtonPressed;
    }

    private void UnSubscribeEvents()
    {
        chestView.OnChestButtonPressed -= ChestButtonPressed;
    }

    private void ChestButtonPressed()
    {    
        string msg;
        string header;
        
        switch (currentState)
        {
            case ChestStates.Locked:
                msg = "How would you like to open chest";
                header = "* Unlock Chest *";
                ChestService.Instance.SetChestView(chestView);
                PromptManager.Instance.DisplayMessageWithButton(header, msg, chestModel.GemsToUnlock, currentState);
                break;
            
            case ChestStates.Unlocking:
                msg = $"You can use {chestModel.GemsToUnlock} gems to unclock now";
                header = "* Unlocking *";
                PromptManager.Instance.DisplayMessageWithButton(header, msg, chestModel.GemsToUnlock, currentState);
                break;

            case ChestStates.Unlocked:
            default:
                PlayerItems.Instance.UpdatePlayerInventory(chestModel.coins, chestModel.gems);
                msg = $"{chestModel.coins} coins and {chestModel.gems} gems added ";
                header = "Congratulations!";
                PromptManager.Instance.DisplayMessage(header, msg);
                UnSubscribeEvents();
                chestView.DestroyChest();
                break;
        }
    }

    public void UnlockUsingGems()
    {
        bool canUnlock = PlayerItems.Instance.ReduceGems(chestModel.GemsToUnlock);
        if (canUnlock)
        {
            ChestUnlocked();
        }
        else
        { 
            string msg = "Don't have enough gems ";
            string header = "* Oops *";
            PromptManager.Instance.DisplayMessage(header, msg);
        }
    }

    public void ChestUnlocked()
    {
        currentState = ChestStates.Unlocked;
        chestModel.unlockTime = 0;
        chestModel.GemsToUnlock = 0;
        ChestService.Instance.isChestTimerRunning = false;
        chestView.ShowChest(chestModel.unlockTime, chestModel.ChestType, chestModel.chestLockedSprite, chestModel.chestUnlockedSprite);
        ChestService.Instance.UnlockNextChest(chestView);
    }
}

