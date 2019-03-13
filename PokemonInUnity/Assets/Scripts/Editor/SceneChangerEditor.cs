using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof(Warp))]
    public class SceneChangerEditor:UnityEditor.Editor
    {

        public List<string> scenes;


        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Warp myWarp = (Warp)target;
            if(GUILayout.Button("Load Scene"))
            {
                try
                {
                    scenes = new List<string>();

                    ProcessDirectory(Path.Combine(Application.dataPath, "Scenes"));

                    string sceneFile = scenes.Find(scene => Path.GetFileNameWithoutExtension(scene) == myWarp.sceneToWarpTo);

                    //https://forum.unity.com/threads/moving-scene-view-camera-from-editor-script.64920/
                    EditorSceneManager.SaveOpenScenes();

                    Scene loaded=EditorSceneManager.OpenScene(sceneFile);
      
                    SceneView.GetAllSceneCameras()[0].transform.position=new Vector3(myWarp.warpLocation.x,myWarp.warpLocation.y,-10);
                    SceneView.lastActiveSceneView.Repaint();
                }
                catch(Exception err)
                {
                    Debug.Log(err);
                }
            }
        }

        //https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getfiles?view=netframework-4.7.2

        // Process all files in the directory passed in, recurse on any directories 
        // that are found, and process the files they contain.
        private void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        // Insert logic for processing found files here.
        private void ProcessFile(string path)
        {
            if (Path.GetExtension(path) == ".unity")
            {
                scenes.Add(path);
            }
        }

    }
}
