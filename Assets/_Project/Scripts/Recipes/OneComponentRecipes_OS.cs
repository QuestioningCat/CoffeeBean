using UnityEngine;

namespace CoffeeBean
{
    /// <summary>
    /// Is a Scriptable Object that takes in 1 component parts and results in a new Item
    /// </summary>
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Coffee/Recipes/1 Component Recipe")]
    public class OneComponentRecipes_OS : ScriptableObject
    {
        // can this recipe be performed in a machine or the players hands?
        public bool HandRecipe;

        // the types of Items needed for this recipe;
        public Tag_SO ComponentOne;
        // States the Components before crafting is allowed - Negative value means ingore
        public int ComponentOneStartState;
        // States of the components after crafting - Negative value means ignore
        public int ComponentOneResultState;



    }
}