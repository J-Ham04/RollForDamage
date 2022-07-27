using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    // FIELDS
    [Header("Sprites")]
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite fullHeart;

    [Space]

    [SerializeField] private float heartSize;

    [Header("Scene Objects")]
    [SerializeField] private Transform heartContainer;
    [SerializeField] private Transform heartTemplate;

    private List<Image> hearts = new List<Image>();
    private PlayerHealth player;
    private HitFlash heartFlash;

    // PROPERTIES
    private int health
    {
        get
        {
            return player.health;
        }
    }
    private int maxHealth
    {
        get
        {
            return player.maxHealth;
        }
    }

    // METHODS
    private void Awake()
    {
        player = FindObjectOfType<PlayerHealth>();
        RefreshHealthBarSize();
        RefreshHealthBarDamage();
    }

    public void RefreshHealthBarDamage()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i + 1 <= health)
            {
                hearts[i].sprite = fullHeart;
            }else
            {
                if (hearts[i].sprite == fullHeart)
                {
                    heartFlash = new HitFlash(this, hearts[i]);
                    heartFlash.Flash();
                }

                hearts[i].sprite = emptyHeart;
            }
        }
    }
    public void RefreshHealthBarSize()
    {
        while (hearts.Count < maxHealth)
        {
            AddHeart();
        }

        while (hearts.Count > maxHealth)
        {
            RemoveHeart();
        }
    }
    
    private void AddHeart()
    {
        RectTransform healthCellRectTransform = Instantiate(heartTemplate, heartContainer).GetComponent<RectTransform>();
        healthCellRectTransform.gameObject.SetActive(true);
        healthCellRectTransform.anchoredPosition = new Vector2(hearts.Count * heartSize, 0 * -heartSize);
        hearts.Add(healthCellRectTransform.gameObject.GetComponent<Image>());
    }

    private void RemoveHeart()
    {
        Destroy(hearts[hearts.Count - 1]);
        hearts.RemoveAt(hearts.Count - 1);
    }
}
