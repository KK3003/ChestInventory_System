using System;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    public ChestController chestController;
    private float chestLocaltime;

    [SerializeField] private Text timerText;
    [SerializeField] private Text chestStatusText;
    [SerializeField] private Text chestTypeText;
    [SerializeField] private Image chestSpriteSlot;
    [SerializeField] private Button chestButton; 
    public event Action OnChestButtonPressed;


    private void Awake()
    {
        chestButton.onClick.AddListener(ChestButtonPressed);
    }

    private void Start()
    {
        SetParent();
    }

    private void Update()
    {
        if (chestController.currentState == ChestStates.Unlocking)
        {
            DecreaseTime();
            chestStatusText.text = chestController.currentState.ToString();
            if (IsTimeOver())
            {
                timerText.text = " READY ";
                chestController.ChestUnlocked();
            }
        }
    }
    
    private void SetParent()
    {
        transform.SetParent(ChestService.Instance.chestSlotGroup.transform);
    }

    public void Initialize(ChestController controller, float time)
    {
        chestController = controller;
        chestLocaltime = time;
    }

    public void ShowChest(float chestTime, ChestType chestType, Sprite lockedChestSprite, Sprite unlockedChestSprite)
    {
        chestLocaltime = chestTime;
        timerText.text = ConvertTimeToString(chestLocaltime);
        chestTypeText.text = chestType.ToString();
        chestStatusText.text = chestController.currentState.ToString();

        if (chestController.currentState == ChestStates.Locked || chestController.currentState == ChestStates.Unlocking)
        {
            chestSpriteSlot.sprite = lockedChestSprite;
        }
        else
        {
            chestSpriteSlot.sprite = unlockedChestSprite;
        }

    }

    private void DecreaseTime()
    {
        chestLocaltime -= Time.deltaTime;
        timerText.text = ConvertTimeToString(chestLocaltime);
    }

    private string ConvertTimeToString(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        string timeString = time.ToString(@"hh\:mm\:ss");
        return timeString;
    }

    private bool IsTimeOver() => chestLocaltime <= 0;
    
    private void ChestButtonPressed()
    {
        OnChestButtonPressed?.Invoke();
    }

    public void DestroyChest()
    {
        Destroy(gameObject);
        ChestService.Instance.chestCounter--;
    }
}

