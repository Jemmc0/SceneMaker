using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class WindowPrincipal : EditorWindow
{
    private GUIStyle _labelStyle;

    static WindowPrincipal w;

    HeroConfig heroScriptble;

    //Para poder editar un array
    bool show = false;
    int amount;
    string[] inventory = new string[0];

    [MenuItem("SceneMaker/New Scene")]
    public static void OpenWindow()
    {
        WindowPrincipal myWindow = (WindowPrincipal)GetWindow(typeof(WindowPrincipal));
        myWindow.wantsMouseMove = true; //necesario para que podamos detectar el mouse
        myWindow.Show();
    }
    private void OnGUI() //aca adentro vamos a hacer todo
    {
        VentanaPrincipal();
        DrawTextAndTextures();
    }



    private void VentanaPrincipal()
    {
        //Estilos de letras
        _labelStyle = new GUIStyle();
        _labelStyle.fontStyle = FontStyle.BoldAndItalic;
        _labelStyle.alignment = TextAnchor.MiddleCenter;
        _labelStyle.fontSize = 24;

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Titulo de ventana

        EditorGUILayout.LabelField("Scene Maker", _labelStyle);

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Botones de ventana

        /*if (GUILayout.Button("Crear Prefab")) //para crear un prefab
        {
            var myObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

            string path = "Assets/" + "MyPrefab.prefab"; // importante poner correctamente la extencion 

            PrefabUtility.SaveAsPrefabAssetAndConnect(myObject, path, InteractionMode.AutomatedAction);

            Save();
        }*/

        /*if (GUILayout.Button("Crear Character"))
        {
            var scriptable = ScriptableObject.CreateInstance<HeroConfig>(); // para crear la instancia
            var path = "Assets/" + "HeroScriptable.asset"; 

             
            path = AssetDatabase.GenerateUniqueAssetPath(path);

            AssetDatabase.CreateAsset(scriptable, path);

            Save();
        }
        */

        if (GUILayout.Button("Character creator"))
        {
            GetWindow(typeof(Windows)).Show();
        }

        if (GUILayout.Button("Pathfinding"))
        {
            GetWindow(typeof(Pathfinding)).Show();
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        //Obtengo mi scriptable
        //heroScriptble = (HeroConfig)EditorGUILayout.ObjectField("Character", heroScriptble, typeof(HeroConfig), false);

        /*if (heroScriptble != null)
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
        }*/



        //GUILayout.Button("Materiales");
        //GUILayout.Button("Particulas");
        //EditorGUILayout.Space();
        //EditorGUILayout.Space();
        GUILayout.Button("Nuevo apartado de componentes");
        if (GUILayout.Button("Cerrar ventana"))
            Close();

    }
    private void DrawTextAndTextures()
    {
        //textura de fondo
        //GUI.DrawTexture(GUILayoutUtility.GetRect(500, 500), (Texture2D)Resources.Load("asdasd"), ScaleMode.ScaleToFit);
    }

    private void Save()
    {
        //Guardamos los assets en disco
        AssetDatabase.SaveAssets();

        //Importa / recarga los archivos nuevos, modificados, para que puedan verse en el editor de unity.
        AssetDatabase.Refresh();
    }
}
