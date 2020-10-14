using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//V0.11
public class RecipeBookUI : MonoBehaviour
{
    [SerializeField]
    RecipeUI recipeUIPrefab;

    [SerializeField]
    Transform container;

    [SerializeField]
    GameObject infoObject;
    [SerializeField]
    TextMeshProUGUI recipeText;
    [SerializeField]
    TextMeshProUGUI timeText;


    private void Start()
    {
        List<Recipe> recipes = GameState.instance.GetRestaurant().GetMenu();

        foreach(Recipe recipe in recipes)
        {
            RecipeUI obj = Instantiate(recipeUIPrefab, container);
            obj.SetRecipe(recipe);

            obj.GetButton().onClick.AddListener(() => PrepareRecipe(obj));
            obj.PointerEnter += Obj_PointerEnter;
            obj.PointerExit += Obj_PointerExit;
        }
    }

    private void Obj_PointerExit(UnityEngine.EventSystems.PointerEventData obj, RecipeUI recipe)
    {
        infoObject.SetActive(false);
    }

    private void Obj_PointerEnter(UnityEngine.EventSystems.PointerEventData obj, RecipeUI recipe)
    {
        infoObject.SetActive(true);
        recipeText.text = recipe.GetRecipe().food.foodName;
        timeText.text = recipe.GetRecipe().timeToPrepare.ToString("F0");
    }

    void PrepareRecipe(RecipeUI recipe)
    {
        GameState.instance.GetRestaurant().PrepareFood(recipe.GetRecipe());
    }
}
