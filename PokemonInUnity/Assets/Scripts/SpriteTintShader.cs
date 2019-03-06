using Assets.Scripts.GameInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
/// <summary>
/// Taken from the tutorial and used in conjunction with the modified PixelEmissionShader based on the sprite outline shader at: https://nielson.io/2016/04/2d-sprite-outlines-in-unity
/// </summary>
public class SpriteTintShader : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private TilemapRenderer mapRenderer;

    private MapManager map;

    private void OnEnable()
    {
        Update();
    }
    private void OnDisable()
    {
        UpdateTint(false);
    }

    /// <summary>
    /// Runs when the component is enabled.
    /// </summary>
    void Start()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (GetComponent<TilemapRenderer>() != null)
        {
            mapRenderer = GetComponent<TilemapRenderer>();
        }
        if (mapRenderer == null && spriteRenderer==null)
        {
            throw new System.Exception("No map renderer or sprite renderer attatched.");
        }

        UpdateTint(true);
    }

    /// <summary>
    /// Only runs if the component is disabled.
    /// </summary>
    void Update()
    {
        UpdateTint(true);
    }

    /// <summary>
    /// Update the sprite emission outline.
    /// </summary>
    /// <param name="outline"></param>
    void UpdateTint(bool outline)
    {
        if (outline == true)
        {
            if (Assets.Scripts.GameInformation.GameManager.Manager != null)
            {
                if (GameManager.Manager.currentMap == null)
                {
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.sharedMaterial.color = Color.white;

                    }
                    else if (mapRenderer != null)
                    {
                        mapRenderer.sharedMaterial.color = Color.white;
                    }
                }
                else
                {
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.material.color = Assets.Scripts.GameInformation.GameManager.Manager.currentMap.mapColor;

                    }
                    else if (mapRenderer != null)
                    {
                        mapRenderer.material.color = Assets.Scripts.GameInformation.GameManager.Manager.currentMap.mapColor;
                    }
                }
            }
            else
            {

                if (map == null)
                {
                    this.map = GameObject.Find("MapManager").GetComponent<MapManager>();
                }
                if (spriteRenderer != null)
                {
                    spriteRenderer.sharedMaterial.color = map.mapColor;

                }
                else if (mapRenderer != null)
                {
                    mapRenderer.sharedMaterial.color = map.mapColor;
                }

            }
        }
        else
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.sharedMaterial.color = Color.white;

            }
            else if (mapRenderer != null)
            {
                mapRenderer.sharedMaterial.color = Color.white;
            }
        }
    }
}
