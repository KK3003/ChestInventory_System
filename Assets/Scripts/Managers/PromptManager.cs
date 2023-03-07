using UnityEngine;
using UnityEngine.UI;

public class PromptManager: MonoGenericSingleton<PromptManager>
{
    [SerializeField] private GameObject popUPMessage;
    [SerializeField] private Text messageText;
    [SerializeField] private Button timerButton;
    [SerializeField] private Button gemsButton;
    [SerializeField] private Text headerText;
    [SerializeField] private Text timerButtonText;
    [SerializeField] private Text gemsButtonText;
        
    public void OnGemsBtnClicked()
    {
        popUPMessage.SetActive(false);
            
        ChestService.Instance.OpenChestWithGem();
    }
        
    public void OnTimerBtnClicked()
    {
        popUPMessage.SetActive(false);
            
        if (ChestService.Instance.isChestTimerRunning)
        {
            ChestService.Instance.AddChestToUnlockList();
        }
        else
        {
            ChestService.Instance.UnlockChest();
        }
    }

    public async void DisplayChestInfo(string chestType, string coinsRange, string gemsRange)
    {
        popUPMessage.SetActive(true);
        timerButton.gameObject.SetActive(false);
        gemsButton.gameObject.SetActive(false);
        headerText.text = "New Chest!";
        messageText.text = $"You got a {chestType} chest.\nCoins: {coinsRange}\nGems: {gemsRange}";

        await new WaitForSeconds(3f);
        popUPMessage.SetActive(false);
    }
        
    public void DisplayMessageWithButton(string header, string msg, int gems, ChestStates state)
    {
        popUPMessage.SetActive(true);
        timerButton.gameObject.SetActive(true);
        gemsButton.gameObject.SetActive(true);
            
        string message;
        if (ChestService.Instance.isChestTimerRunning && ChestService.Instance.noOfChestCanUnlock > 1)
        {
            message = "Add Chest!";
            timerButtonText.text = message;
        }
        else
        {
            message = "Start timer";
            timerButtonText.text = message;
        }

        headerText.text = header;
        messageText.text = msg;
        gemsButtonText.text = gems.ToString();

        IsChestAdded(state);
    }
        
    private void IsChestAdded(ChestStates state)
    {
        timerButton.gameObject.SetActive(state != ChestStates.Unlocking);    
        gemsButton.gameObject.SetActive(true);
    }
        
    public void DisplayMessage(string header, string msg)
    {
        popUPMessage.SetActive(true);
        headerText.text = header;
        messageText.text = msg;
        timerButton.gameObject.SetActive(false);
        gemsButton.gameObject.SetActive(false);
        DisableMessage();
    }

    private async void DisableMessage()
    {
        await new WaitForSeconds(2f);
        popUPMessage.gameObject.SetActive(false);
    }
}
