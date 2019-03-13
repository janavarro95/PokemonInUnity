using Assets.Scripts.Content;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RandomPokemonImageScript : MonoBehaviour
{

    public Image pokemonImage;
    public AudioClip cry;

    // Start is called before the first frame update
    void Start()
    {

        int pokedexNumber = UnityEngine.Random.Range(0, 152);
        Sprite frontSprite = ContentManager.LoadTextureFrom2DAtlas(Path.Combine("Graphics", "PokemonGen1"), "PokemonGen1_" + ((pokedexNumber - 1) * 2).ToString());
        pokemonImage = this.gameObject.GetComponent<Image>();
        pokemonImage.sprite = frontSprite;
        this.cry=loadCry(pokedexNumber);

    }

    // Update is called once per frame
    void Update()
    {

    }
    private AudioClip loadCry(int pokedexNumber)
    {
        string name = "";
        if (pokedexNumber < 10)
        {
            name = "00" + pokedexNumber.ToString() + "Cry";
        }
        else if (pokedexNumber < 100)
        {
            name = "0" + pokedexNumber.ToString() + "Cry";
        }
        else
        {
            name = pokedexNumber.ToString() + "Cry";
        }


        AudioClip cry = Resources.Load<AudioClip>(Path.Combine("Audio", "PokemonCries", name));
        return cry;
    }
}
