using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteImporter : AssetPostprocessor {
    int pixelsPerUnit = 128;

	void OnPostprocessTexture(Texture2D texture)
    {
        TextureImporter ti = (assetImporter as TextureImporter);
        ti.spritePixelsPerUnit = pixelsPerUnit;

        TextureImporterSettings importerSettings = new TextureImporterSettings();
        ti.ReadTextureSettings(importerSettings);

        importerSettings.spritePixelsPerUnit = pixelsPerUnit;

        ti.SetTextureSettings(importerSettings);
        Debug.Log("Set PPU for " + ti.assetPath);
    }
}
