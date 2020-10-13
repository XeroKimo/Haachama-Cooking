using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//V0.1
public class RecipeUI : MonoBehaviour
{
    Recipe recipe;
    [SerializeField]
    Image image;

    [SerializeField]
    Button button;

    public void SetRecipe(Recipe recipe)
    {
        this.recipe = recipe;
        image.sprite = recipe.food.sprite;
    }

    public Recipe GetRecipe() { return recipe; }
    public Button GetButton() { return button; }
}
