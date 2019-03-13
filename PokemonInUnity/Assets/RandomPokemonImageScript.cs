using Assets.Scripts.Content;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RandomPokemonImageScript : MonoBehaviour
{

    Image pokemonImage;

    // Start is called before the first frame update
    void Start()
    {

        int pokedexNumber = UnityEngine.Random.Range(0, 152);
        Sprite frontSprite = ContentManager.LoadTextureFrom2DAtlas(Path.Combine("Graphics", "PokemonGen1"), "PokemonGen1_" + ((pokedexNumber - 1) * 2).ToString());
        pokemonImage = this.gameObject.GetComponent<Image>();
        pokemonImage.sprite = frontSprite;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
