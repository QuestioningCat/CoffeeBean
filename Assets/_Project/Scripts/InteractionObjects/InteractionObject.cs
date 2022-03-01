using CoffeeBean.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoffeeBean
{

    [RequireComponent(typeof(BoxCollider))]
    public class InteractionObject : MonoBehaviour
    {
        [Header("Scriptable Object")]
        [Tooltip("The scriptable object template")]
        [SerializeField] private InteractionObject_OS interactionItemData_SO;

        [Header("Recipe")]
        [Tooltip("A Recipe this Item can be used to make")]
        [SerializeField] private OneComponentRecipes_OS recipe;

        [Header("Events")]
        [SerializeField] private InteractionObjectEvent onNewInteractionObjectCreated;


        public bool GetTag(string tag)
        {
            if(tag == "" || tag == null)
                return false;

            foreach(Tag_SO t in interactionItemData_SO.Tags)
            {
                if(t == null)
                    continue;
                if(t.name == tag)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks see if this Interaction Object has any single component recipes that the used Item can be used in.
        /// </summary>
        /// <param name="usedItem">The Item we want to use in the recipe</param>
        /// <returns> Returns the recipes these 2 items belong too </returns>
        public OneComponentRecipes_OS IsValidRecipeCombination(Item usedItem)
        {
            if(usedItem == null)
                return null;

            if(usedItem.HasTag(recipe.ComponentOne.name) && recipe.ComponentOneStartState == usedItem.GetItemStateIndex())
            {
                return recipe;
            }

            return null;
        }

        private void Start()
        {
            onNewInteractionObjectCreated.Raise(this);
        }

    }

}