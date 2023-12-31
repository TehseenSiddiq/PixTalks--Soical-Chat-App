using DG.Tweening;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public RectTransform menu;
    public RectTransform LostPanel;
    public Button respawnButton;
    public PlayerBehavior photonPlayer;
    public TMP_Text portionsCount;
    public Slider healthBar;
    public Button portionButton;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        respawnButton.onClick.AddListener(() => {
            photonPlayer.Respawn();
            LostPanel.DOScale(0, 0.1f).SetEase(Ease.InBack);
        }
        
        );
        healthBar.maxValue = photonPlayer.maxHealth;
        InvokeRepeating(nameof(UpdatePlayerStates), 1, 1);
        portionButton.onClick.AddListener(() => photonPlayer.Heal(20)) ;
    }
    void UpdatePlayerStates()
    {
        healthBar.value = photonPlayer.health;
        portionsCount.text = photonPlayer.portions.ToString();
    }
    public void GameOver()
    {
        //  Time.timeScale = 0;
        LostPanel.DOScale(1, 0.05f).SetEase(Ease.InBack);
    }
    public void Respawn()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
