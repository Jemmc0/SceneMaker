using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;
using System;

public class Windows : EditorWindow
{

    HeroConfig heroScriptble;

    public GameObject prefab;

    bool show = false;
    int amount;
    string[] inventory = new string[0];

    Texture2D headerSectionTexture;
   
    Texture2D warriorSectionTexture;

 
    Texture2D warriorTexture;
 

    Color headerSectioinColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);

    Rect headerSection;
    
    Rect warriorSection;
  
   
    Rect warriorIconSection;
   
    GUISkin skin;

 
    static WarriorData warriorData;
 

 
    public WarriorData WarriorInfo { get { return warriorData; } }
  

    float iconSize = 80;

    
    public static void OpenWindow()
    {
        Windows Window = (Windows)GetWindow(typeof(Windows));
        Window.wantsMouseMove = true;
        Window.Show();
        Window.minSize = new Vector2(100f, 100f);

    }

    void OnGUI()
    {

        skin = Resources.Load<GUISkin>("guiStyles/SceneMakerSkin");
        InitTexture();
        InitData();

        DrawLayouts();
        DrawHeader();
       
        DrawWarriorSettings();

        
    }

    public static void InitData()
    {
      
        warriorData = (WarriorData)ScriptableObject.CreateInstance(typeof(WarriorData));
       
    }


    void InitTexture()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectioinColor);
        headerSectionTexture.Apply();

      
        warriorSectionTexture = Resources.Load<Texture2D>("icons/violet");
        
    }


    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 50;

       

        warriorSection.x = Screen.width / 200f;
        warriorSection.y = 50;
        warriorSection.width = Screen.width / 1f;
        warriorSection.height = Screen.width - 0.1f;

        warriorIconSection.x = (warriorSection.x + warriorSection.width / 2f) - iconSize / 2f;
        warriorIconSection.y = warriorSection.y + 8;
        warriorIconSection.width = iconSize;
        warriorIconSection.height = iconSize;

        

        GUI.DrawTexture(headerSection, headerSectionTexture);
        
        GUI.DrawTexture(warriorSection, warriorSectionTexture);
        
      
        GUI.DrawTexture(warriorIconSection, warriorTexture);
       
    }
    void DrawHeader()
    {


        GUILayout.BeginArea(headerSection);
                if (GUILayout.Button("Path")) Pathfinding();
        GUILayout.Label("Character creator", skin.GetStyle("Header1"));

        GUILayout.EndArea();
    }

   void Pathfinding() 
    {
    
    }

    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warriorSection);




        GUILayout.Space(iconSize + 8);

        

        if (GUILayout.Button("Crear Prefab"))
        {
            var myObject = prefab;
            string path = "Assets/" + "MyPrefab.prefab"; // importante poner correctamente la extencion 

            PrefabUtility.SaveAsPrefabAssetAndConnect(Instantiate(myObject), path, InteractionMode.AutomatedAction);



            Save();
            
        }

        if (GUILayout.Button("Crear Scripteable"))
        {
            var scriptable = ScriptableObject.CreateInstance<HeroConfig>(); // para crear la instancia
            var path = "Assets/" + "HeroScriptable.asset";


            path = AssetDatabase.GenerateUniqueAssetPath(path);

            AssetDatabase.CreateAsset(scriptable, path);

            Save();
        }


        heroScriptble = (HeroConfig)EditorGUILayout.ObjectField("Character", heroScriptble, typeof(HeroConfig), false);

        if (heroScriptble != null)
        {
            EditorGUILayout.Space();
            heroScriptble.characterName = EditorGUILayout.TextField("Name", heroScriptble.characterName);
            heroScriptble.hp = EditorGUILayout.IntField("Life", heroScriptble.hp);
            heroScriptble.speed = EditorGUILayout.IntField("Speed", heroScriptble.speed);
            heroScriptble.godMode = EditorGUILayout.Toggle("Is on GodMode ?", heroScriptble.godMode);

            #region Edito el array
            show = EditorGUILayout.Foldout(show, "inventory");
            if (show)
            {
                amount = EditorGUILayout.IntField(amount);
                var aux = new string[amount];
                for (int i = 0; i < inventory.Length; i++)
                {
                    if (i >= aux.Length) break;
                    aux[i] = inventory[i];
                }
                inventory = new string[amount];
                for (int i = 0; i < inventory.Length; i++)
                {
                    inventory[i] = aux[i];
                }
                for (int i = 0; i < inventory.Length; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.Space();
                    inventory[i] = EditorGUILayout.TextField(inventory[i]);
                    EditorGUILayout.EndHorizontal();
                }
                heroScriptble.inventory = inventory;
            }
            #endregion

            EditorUtility.SetDirty(heroScriptble); //Para guardar
        }

        GUILayout.EndArea();
    }

   

    private void Save()
    {
        //Guardamos los assets en disco
        AssetDatabase.SaveAssets();

        //Importa / recarga los archivos nuevos, modificados, para que puedan verse en el editor de unity.
        AssetDatabase.Refresh();
    }




}



