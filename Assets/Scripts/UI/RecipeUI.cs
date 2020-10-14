using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
//V0.11
public class RecipeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Recipe recipe;
    [SerializeField]
    Image image;

    [SerializeField]
    Button button;

    public event Action<PointerEventData, RecipeUI> PointerEnter;
    public event Action<PointerEventData, RecipeUI> PointerExit;

    public void SetRecipe(Recipe recipe)
    {
        this.recipe = recipe;
        image.sprite = recipe.food.sprite;
    }

    public Recipe GetRecipe() { return recipe; }
    public Button GetButton() { return button; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter?.Invoke(eventData, this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit?.Invoke(eventData, this);
    }
}
