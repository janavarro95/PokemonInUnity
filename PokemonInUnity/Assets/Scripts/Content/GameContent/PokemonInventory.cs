using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Content.GameContent
{
    public class PokemonInventory
    {

        public List<Pokemon> pokemon;
        /// <summary>
        /// The capacity of pokemon.
        /// </summary>
        public int capacity;
        /// <summary>
        /// Checks if the storage is full.
        /// </summary>
        public bool IsFull
        {
            get
            {
                return pokemon.Count == capacity;
            }
        }

        public PokemonInventory()
        {
            
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Capacity"></param>
        public PokemonInventory(int Capacity)
        {
            pokemon = new List<Pokemon>();
        }

        /// <summary>
        /// Checks if a pokemon can be added to this inventory.
        /// </summary>
        /// <returns></returns>
        public virtual bool canAddPokemon()
        {
            return IsFull;
        }

        /// <summary>
        /// Adds a pokemon to the inventory if possible.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public virtual bool addPokemon(Pokemon p)
        {
            if (canAddPokemon())
            {
                pokemon.Add(p);
                return true;
            }
            Debug.Log("Can't add pokemon =(");
            return false;
        }

        /// <summary>
        /// Gets the pokemon at a given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual Pokemon getPokemonAtIndex(int index)
        {
            return this.pokemon[index];
        }

        /// <summary>
        /// Removes a given pokemon at an index from the party PERMANETLY.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual void removePokemonFromParty(int index)
        {

            this.pokemon.RemoveAt(index);

            /*
            if (this.pokemon[index] != null)
            {
                this.pokemon[index] = null;
                return true;
            }
            return false;
            */
        }

        /// <summary>
        /// Removes a given pokemon from the party PERMANETLY.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public virtual bool removePokemonFromParty(Pokemon p)
        {
            return this.pokemon.Remove(p);
        }

        /// <summary>
        /// Transfers a pokemon from one inventory into another.
        /// </summary>
        /// <param name="pokeToTransfer"></param>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public virtual bool transferPokemonToOther(Pokemon pokeToTransfer, PokemonInventory inventory)
        {
            if (inventory.canAddPokemon())
            {
                inventory.addPokemon(pokeToTransfer);
                this.removePokemonFromParty(pokeToTransfer);
                return true;
            }
            return false;
        }

    }
}
