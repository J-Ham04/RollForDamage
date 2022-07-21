using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    // FIELDS
    [SerializeField] private List<Image> cells = new List<Image>();

    [SerializeField] private Sprite[] cellSprites;

    private PlayerHealth player;

    // PROPERTIES
    private Sprite FULL_CELL
    {
        get { return cellSprites[0]; }
    }
    private Sprite TRANSITION_CELL
    {
        get { return cellSprites[1]; }
    }
    private Sprite EMPTY_CELL
    {
        get { return cellSprites[2]; }
    }

    // METHODS
    private void Awake()
    {
        player = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        SetHealthBarLength();

        for (int i = 0; i < cells.Count; i++)
        {
            if (player.health - 1 > i)
            {
                cells[i].sprite = FULL_CELL;
            }

            if (player.health - 1 < i)
            {
                cells[i].sprite = EMPTY_CELL;
            }

            if (player.health  == i)
            {
                cells[i].sprite = TRANSITION_CELL;
            }
        }
    }

    public void SetHealthBarLength()
    {
        while (cells.Count < player.maxHealth)
        {
            AddCell();
        }

        while (cells.Count > player.maxHealth)
        {
            RemoveCell();
        }
    }

    void AddCell()
    {

    }

    void RemoveCell()
    {

    }
}
