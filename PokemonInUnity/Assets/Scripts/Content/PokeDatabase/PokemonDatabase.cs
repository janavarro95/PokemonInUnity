using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokeAPI;
using UnityEngine;

using LitJson;

namespace Assets.Scripts.Content.PokeDatabase
{
    public static class PokemonDatabase
    {
        public static Dictionary<int, PokemonSpecies> PokemonSpeciesByDex = new Dictionary<int, PokemonSpecies>();
        public static Dictionary<string, PokemonSpecies> PokemonSpeciesByName = new Dictionary<string, PokemonSpecies>();

        public static Dictionary<int, Pokemon> PokemonByDex = new Dictionary<int, Pokemon>();
        public static Dictionary<string, Pokemon> PokemonByName = new Dictionary<string, Pokemon>();

        /// <summary>
        /// Scrapes all of the species info for a pokemon!
        /// </summary>
        public static async void ScrapePokemonSpecies()
        {
            string pokePath = Path.Combine(Application.streamingAssetsPath, "JSON", "PokemonDatabase", "PokemonSpecies");
            Directory.CreateDirectory(pokePath);
            string [] allFilesAtPath= Directory.GetFiles(pokePath,"*.json");
            

            foreach(string path in allFilesAtPath)
            {
                string fileName = Path.GetFileNameWithoutExtension(path);
                int dexNumber =Convert.ToInt32(fileName.Split('_')[0]);


                StringBuilder builder = new StringBuilder();

                string[] strings = File.ReadAllLines(path);
                foreach(string line in strings)
                {
                    builder.Append(line);
                }
                PokemonSpecies deserialized = JsonMapper.ToObject<PokemonSpecies>(builder.ToString());

                

                PokemonSpeciesByDex.Add(dexNumber, deserialized);
                PokemonSpeciesByName.Add(deserialized.Name, deserialized);
                //Debug.Log("Add in serialized pokemon: " + deserialized.Name);


            }

            for ( int i=1; i<=386; i++)
            {
                if (PokemonSpeciesByDex.Keys.Contains(i)) continue;
                PokemonSpecies poke = await getPokemonSpecies(i);
                Debug.Log("Pokemon is! " + poke.Name);
                PokemonSpeciesByDex.Add(i, poke);
                PokemonSpeciesByName.Add(poke.Name, poke);
                SerializePokemonSpecies(poke);
                
            }

            Debug.Log("Done scraping Pokemon Species!");
        }

        /// <summary>
        /// Scrapes all of the pokemons stats/moves/etc
        /// </summary>
        public static async void ScrapePokemon()
        {
            string pokePath = Path.Combine(Application.streamingAssetsPath, "JSON", "PokemonDatabase", "Pokemon");
            Directory.CreateDirectory(pokePath);
            string[] allFilesAtPath = Directory.GetFiles(pokePath, "*.json");


            foreach (string path in allFilesAtPath)
            {
                string fileName = Path.GetFileNameWithoutExtension(path);
                int dexNumber = Convert.ToInt32(fileName.Split('_')[0]);


                StringBuilder builder = new StringBuilder();

                string[] strings = File.ReadAllLines(path);
                foreach (string line in strings)
                {
                    builder.Append(line);
                }
                Pokemon deserialized = JsonMapper.ToObject<Pokemon>(builder.ToString());



                PokemonByDex.Add(dexNumber, deserialized);
                PokemonByName.Add(deserialized.Name, deserialized);

                Debug.Log("Add in serialized pokemon: " + deserialized.Name);


            }

            for (int i = 1; i <= 386; i++)
            {
                if (PokemonByDex.Keys.Contains(i)) continue;
                Pokemon poke = await getPokemon(i);
                Debug.Log("Pokemon is! " + poke.Name);
                PokemonByDex.Add(i, poke);
                PokemonByName.Add(poke.Name, poke);
                SerializePokemon(poke);

            }

            Debug.Log("Done scraping Pokemon!");
        }


        async static Task<PokeAPI.PokemonSpecies> getPokemonSpecies(int id)
        {

            Debug.Log("Scraping pokemon with DEX number: " + id);
            await Task.Delay(3000);
            PokemonSpecies p=await DataFetcher.GetApiObject<PokemonSpecies>(id);

            return p;
        }

        async static Task<PokeAPI.Pokemon> getPokemon(int id)
        {

            Debug.Log("Scraping pokemon with DEX number: " + id);
            await Task.Delay(3000);

            Pokemon p = await DataFetcher.GetApiObject<Pokemon>(id);

            return p;
        }

        private static void SerializePokemonSpecies(PokemonSpecies poke)
        {
            int index = -1;

            
            foreach(PokemonSpeciesDexEntry entry in poke.PokedexNumbers)
            {
                if(entry.Pokedex.Name== "national")
                {
                    index = entry.EntryNumber;
                    
                }
            }
            
            



            string path = Path.Combine(Application.streamingAssetsPath, "JSON", "PokemonDatabase", "PokemonSpecies", index + "_" + poke.Name + ".json");

            FileStream fStream = new FileStream(path,FileMode.CreateNew,FileAccess.ReadWrite,FileShare.None);
            StreamWriter writer = new StreamWriter(fStream,Encoding.Unicode);

            JsonWriter jsonWriter = new JsonWriter(writer);
            jsonWriter.PrettyPrint = true; // 한줄로 JsonText를 생성하지않고 사람이 읽기 쉽게 출력
            jsonWriter.IndentValue = 2; // 들여쓰기
            JsonMapper.ToJson(poke, jsonWriter);
            jsonWriter.TextWriter.Close();

            fStream.Close();



        }

        private static void SerializePokemon(Pokemon poke)
        {
            int index = -1;


           
            foreach (PokemonSpeciesDexEntry entry in PokemonSpeciesByName[poke.Name].PokedexNumbers)
            {
                if (entry.Pokedex.Name == "national")
                {
                    index = entry.EntryNumber;
                }
              
            }
            
            string path = Path.Combine(Application.streamingAssetsPath, "JSON", "PokemonDatabase", "Pokemon", index + "_" + poke.Name + ".json");

            FileStream fStream = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);
            StreamWriter writer = new StreamWriter(fStream, Encoding.Unicode);

            JsonWriter jsonWriter = new JsonWriter(writer);
            jsonWriter.PrettyPrint = true; // 한줄로 JsonText를 생성하지않고 사람이 읽기 쉽게 출력
            jsonWriter.IndentValue = 2; // 들여쓰기
            JsonMapper.ToJson(poke, jsonWriter);
            jsonWriter.TextWriter.Close();

            fStream.Close();



        }


    }
}
