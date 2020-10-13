using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBookUI : MonoBehaviour
{
    [SerializeField]
    RecipeUI recipeUIPrefab;

    [SerializeField]
    Transform container;

    private void Start()
    {
        List<Recipe> recipes = GameState.instance.GetRestaurant().GetMenu();

        foreach(Recipe recipe in recipes)
        {
            RecipeUI obj = Instantiate(recipeUIPrefab, container);
            obj.SetRecipe(recipe);

            obj.GetButton().onClick.AddListener(() => PrepareRecipe(obj));
        }
    }

    void PrepareRecipe(RecipeUI recipe)
    {
        GameState.instance.GetRestaurant().PrepareFood(recipe.GetRecipe());
    }
}
