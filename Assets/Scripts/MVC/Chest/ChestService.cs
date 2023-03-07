using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChestService : MonoGenericSingleton<ChestService>
{
    private ChestController[] chests;

    [SerializeField] private ChestView chestPrefab;
    
    public GameObject chestSlotGroup;

    [SerializeField] private ChestTypeList chestTypeList;
    
    [SerializeField] private int numberOfChests;

    public int noOfChestCanUnlock;
    private ChestView chestToUnlock;
    private List<ChestView> chestUnlockingList;
    [SerializeField] private Button claimChestButton;

    public int chestCounter;
    public bool isChestTimerRunning { get; set; }

    protected override void Awake()
    {
        base.Awake();
        chestUnlockingList = new List<ChestView>();
        claimChestButton.onClick.AddListener(SpawnChest);
        
    }

    private void SpawnChest()
    {      
        chests = new ChestController[numberOfChests];
        int randomNum = Random.Range(0, chestTypeList.chestsTypeList.Length);
        
        
        if (chestCounter < chests.Length)
        {
            chests[chestCounter] = CreateChest(chestTypeList.chestsTypeList[randomNum]);
            chestCounter++;
        }
        else
        {
            string msg = " Chest slots are full";
            string header = " No Space ";
            PromptManager.Instance.DisplayMessage(header, msg);
        }
        
    }
    private ChestController CreateChest(ChestTypeSo chestTypeSo)
    {
        
        ChestModel chestModel = new ChestModel(chestTypeSo);
        ChestController chestController = new ChestController(chestModel, chestPrefab);
        return chestController;
    }

    public void SetChestView(ChestView view)
    {
        chestToUnlock = view;
    }

    public void OpenChestWithGem()
    {
        chestToUnlock.chestController.UnlockUsingGems();
    }
    
    public void UnlockChest()
    {
        chestUnlockingList.Add(chestToUnlock);
        chestToUnlock.chestController.currentState = ChestStates.Unlocking;
        
        isChestTimerRunning = true;
    }

    public void UnlockNextChest(ChestView chestView)
    {
        chestUnlockingList.Remove(chestView);
        if (chestUnlockingList.Count > 0)
        {
            isChestTimerRunning = true;
            chestUnlockingList[0].chestController.currentState = ChestStates.Unlocking;
        }
    }

    public void AddChestToUnlockList()
    {
        string msg;
        string header;
        if (isChestTimerRunning && noOfChestCanUnlock+1 == chestUnlockingList.Count)
        {
            header = "No Space";
            msg = " Can't unlock more chest ";
            PromptManager.Instance.DisplayMessage(header, msg);
        }
        else
        {
            header = " Unlock Chest ";
            msg = " Chest added to the list ";
            PromptManager.Instance.DisplayMessage(header, msg);
            chestUnlockingList.Add(chestToUnlock);
        }
    }
}

