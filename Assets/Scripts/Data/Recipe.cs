using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//V0.1
[System.Serializable]
public struct Food
{
    public string FoodName;
    public Sprite sprite;

    public override bool Equals(object obj)
    {
        return obj is Food food &&
               FoodName == food.FoodName &&
               EqualityComparer<Sprite>.Default.Equals(sprite, food.sprite);
    }

    public override int GetHashCode()
    {
        int hashCode = 1561273614;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FoodName);
        hashCode = hashCode * -1521134295 + EqualityComparer<Sprite>.Default.GetHashCode(sprite);
        return hashCode;
    }

    public static bool operator ==(Food lh, Food rh)
    {
        return lh.FoodName == rh.FoodName && lh.sprite == rh.sprite;
    }
    public static bool operator !=(Food lh, Food rh)
    {
        return !(lh == rh);
    }
}

//V0.1
[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    public Food food;
    public float timeToPrepare;
}
