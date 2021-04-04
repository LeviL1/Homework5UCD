using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;

public class EnemyDesignerWindow : EditorWindow
{
  Texture2D headerSectionTexture;
  Texture2D mageSectionTexture;
  Texture2D warriorSectionTexture;
  Texture2D rogueSectionTexture;
  Texture2D mageTexture;
  Texture2D warriorTexure;
  Texture2D rogueTexture;

  Color headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);
  
  Rect headerSection;
  Rect mageSection;
  Rect warriorSection;
  Rect rogueSection;
  Rect mageIconSection;
  Rect warriorIconSection;
  Rect rogueIconSection;

  static MageData mageData;
  static WarriorData warriorData;
  static RogueData rogueData;
  GUISkin skin;
  
  public static MageData MageInfo { get { return mageData; } }
  public static WarriorData Warriorinfo { get { return warriorData; } }
  public static RogueData RogueInfo { get { return rogueData; } }
  float iconSize = 80;

  [MenuItem("Window/EnemyDesigner")]
 static void OpenWindow()
  {
    EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
    window.minSize = new Vector2(600, 300);
    window.Show();
  }

  //similiar to start()
  void OnEnable()
  {
    InitTextures();
    InitData();
    skin = Resources.Load<GUISkin>("guiStyles/EnemyDesignerSkin");
  }
  public static void InitData()
  {
    mageData = (MageData)ScriptableObject.CreateInstance(typeof(MageData));
    warriorData = (WarriorData)ScriptableObject.CreateInstance(typeof(WarriorData));
    rogueData = (RogueData)ScriptableObject.CreateInstance(typeof(RogueData));
  }
  //intialize texture values 
  void InitTextures()
  {
    headerSectionTexture = new Texture2D(1, 1);
    headerSectionTexture.SetPixel(0, 0, headerSectionColor);
    headerSectionTexture.Apply();


    mageSectionTexture = Resources.Load<Texture2D>("Icons/editor_mage_gradient");
    warriorSectionTexture = Resources.Load<Texture2D>("Icons/editor_warrior_gradient");
    rogueSectionTexture = Resources.Load<Texture2D>("Icons/editor_rogue_gradient");

    mageTexture = Resources.Load<Texture2D>("Icons/WandIcon");
    warriorTexure = Resources.Load<Texture2D>("Icons/warriorIcon");
    rogueTexture = Resources.Load<Texture2D>("Icon/rogue_icon");
    
  }
  //called on interactions
  void OnGUI()
  {
    DrawLayouts();
    DrawHeader();
    DrawMageSettings();
    DrawWarriorSettings();
    DrawRogueSettings();
  }
  //Define rect vals and paint textures 
  void DrawLayouts()
  {
    
    //Size of header 
    headerSection.x = 0;
    headerSection.y = 0;
    headerSection.width = Screen.width;
    headerSection.height = 50;

    //Size of mage section
    mageSection.x = 0;
    mageSection.y = 50;
    mageSection.width = Screen.width / 3f;
    mageSection.height = Screen.height - 50f;

    mageIconSection.x = (mageSection.x + mageSection.width / 2f) - iconSize/ 2f;
    mageIconSection.y = mageSection.y + 8;
    mageIconSection.width = iconSize;
    mageIconSection.height = iconSize;

    //size of warrior section
    warriorSection.x = Screen.width / 3f;
    warriorSection.y = 50;
    warriorSection.width = Screen.width / 3f;
    warriorSection.height = Screen.height - 50f;

    warriorIconSection.x = (warriorSection.x + warriorSection.width / 2f) - iconSize / 2f;
    warriorIconSection.y = warriorSection.y + 8;
    warriorIconSection.width = iconSize;
    warriorIconSection.height = iconSize;

    //size of rogue section
    rogueSection.x = (Screen.width / 3f) * 2;
    rogueSection.y = 50;
    rogueSection.width = Screen.width / 3f;
    rogueSection.height = Screen.height - 50f;

    rogueIconSection.x = (rogueSection.x + rogueSection.width / 2f) - iconSize / 2f;
    rogueIconSection.y = rogueSection.y + 8;
    rogueIconSection.width = iconSize;
    rogueIconSection.height = iconSize;

    //Draw sections 
    GUI.DrawTexture(headerSection, headerSectionTexture);
    GUI.DrawTexture(mageSection, mageSectionTexture);
    
    GUI.DrawTexture(warriorSection, warriorSectionTexture);
    GUI.DrawTexture(rogueSection, rogueSectionTexture);

    GUI.DrawTexture(mageIconSection, mageTexture);
    GUI.DrawTexture(warriorIconSection, warriorTexure);
    GUI.DrawTexture(rogueIconSection, mageTexture);
    
    // it tried this and kept getting a null texure
    // was passed to GUI.DrawTexture but the texture shouldn't be null
    //GUI.DrawTexture(rogueIconSection, rogueTexture);
  }
  void DrawHeader()
  {

    GUILayout.BeginArea(headerSection);
    GUILayout.Label("Enemy Designer", skin.GetStyle("Header1"));
    GUILayout.EndArea();
  }
  void DrawMageSettings()
  {
    GUILayout.BeginArea(mageSection);
    GUILayout.Space(iconSize + 8);
    GUILayout.Label("Mage", skin.GetStyle("MageHeader"));
    GUILayout.BeginHorizontal();
    GUILayout.Label("Damage", skin.GetStyle("MageField"));
    mageData.dmgType = (MageDmgType)EditorGUILayout.EnumPopup(mageData.dmgType, skin.GetStyle("MageHeader"));
    GUILayout.EndHorizontal();
    GUILayout.BeginHorizontal();
    GUILayout.Label("Weapon Type", skin.GetStyle("MageField"));
    mageData.wpnType = (MageWpnType)EditorGUILayout.EnumPopup(mageData.wpnType, skin.GetStyle("MageHeader"));
    GUILayout.EndHorizontal();

    if (GUILayout.Button("Create!", skin.GetStyle("MageHeader"), GUILayout.Height(40)))
    {
      GeneralSettings.OpenWindow(GeneralSettings.SettingsType.MAGE);
    }
    GUILayout.EndArea();
  }
  void DrawWarriorSettings()
  {
    GUILayout.BeginArea(warriorSection);
    GUILayout.Space(iconSize + 8);
    GUILayout.Label("Warrior", skin.GetStyle("WarriorHeader"));
    GUILayout.BeginHorizontal();
    GUILayout.Label("Warrior Class", skin.GetStyle("WarriorField"));
    warriorData.classType = (WarriorClassType)EditorGUILayout.EnumPopup(warriorData.classType, skin.GetStyle("WarriorHeader"));
    GUILayout.EndHorizontal();
    GUILayout.BeginHorizontal();
    GUILayout.Label("Warrior Weapon", skin.GetStyle("WarriorField"));
    warriorData.wpnType = (WarriorWpnType)EditorGUILayout.EnumPopup(warriorData.wpnType, skin.GetStyle("WarriorHeader"));
    GUILayout.EndHorizontal();
    if(GUILayout.Button("Create!", skin.GetStyle("WarriorHeader"), GUILayout.Height(40)))
    {
      GeneralSettings.OpenWindow(GeneralSettings.SettingsType.WARRIOR);
    }
    GUILayout.EndArea();
  }
  void DrawRogueSettings()
  {
    GUILayout.BeginArea(rogueSection);
    GUILayout.Space(iconSize +  8);
    GUILayout.Label("Rogue", skin.GetStyle("RogueHeader"));
    GUILayout.BeginHorizontal();
    GUILayout.Label("Strategy Type", skin.GetStyle("RogueField"));
    rogueData.strategyType = (RogueStrategyType)EditorGUILayout.EnumPopup(rogueData.strategyType, skin.GetStyle("RogueHeader"));
    GUILayout.EndHorizontal();
    GUILayout.BeginHorizontal();
    GUILayout.Label("Weapon Type", skin.GetStyle("RogueField"));
    rogueData.wpnType = (RogueWpnType)EditorGUILayout.EnumPopup(rogueData.wpnType, skin.GetStyle("RogueHeader"));
    GUILayout.EndHorizontal();

    if(GUILayout.Button("Create!", skin.GetStyle("RogueHeader"), GUILayout.Height(40)))
    {
      GeneralSettings.OpenWindow(GeneralSettings.SettingsType.ROGUE);
    }
    GUILayout.EndArea();
  }
}
public class GeneralSettings : EditorWindow
{
  public enum SettingsType
  {
    MAGE,
    WARRIOR,
    ROGUE
  }
  static SettingsType dataSetting;
  static GeneralSettings window;

  public static void OpenWindow(SettingsType setting)
  {
    dataSetting = setting;
    window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
    window.minSize = new Vector2(250, 200);
    window.Show();
  }
  void OnGUI()
  {
    switch (dataSetting)
    {
      case SettingsType.MAGE:
        DrawSettings((CharacterData)EnemyDesignerWindow.MageInfo);
        break;
      case SettingsType.WARRIOR:
        DrawSettings((CharacterData)EnemyDesignerWindow.Warriorinfo);
        break;
      case SettingsType.ROGUE:
        DrawSettings((CharacterData)EnemyDesignerWindow.RogueInfo);
        break;
    }
  }
  void DrawSettings(CharacterData charData)
  {
    EditorGUILayout.BeginHorizontal();
    GUILayout.Label("Prefab");
    charData.prefab = (GameObject)EditorGUILayout.ObjectField(charData.prefab, typeof(GameObject), false);
    EditorGUILayout.EndHorizontal();

    EditorGUILayout.BeginHorizontal();
    GUILayout.Label("Max Health");
    charData.maxHealth = EditorGUILayout.FloatField(charData.maxHealth);
    EditorGUILayout.EndHorizontal();

    EditorGUILayout.BeginHorizontal();
    GUILayout.Label("Max Energy");
    charData.maxEnergy = EditorGUILayout.FloatField(charData.maxEnergy);
    EditorGUILayout.EndHorizontal();

    

    EditorGUILayout.BeginHorizontal();
    GUILayout.Label("Power");
    charData.power = EditorGUILayout.Slider(charData.power, 0, 100);
    EditorGUILayout.EndHorizontal();

    EditorGUILayout.BeginHorizontal();
    GUILayout.Label("% crit chance");
    charData.critChance = EditorGUILayout.Slider(charData.critChance, 0, charData.power);
    EditorGUILayout.EndHorizontal();

    EditorGUILayout.BeginHorizontal();
    GUILayout.Label("Name");
    charData.name = EditorGUILayout.TextField(charData.name);
    EditorGUILayout.EndHorizontal();

    if(charData.prefab == null)
    {
      EditorGUILayout.HelpBox("This enemy needs a [Prefab] before it can be created.", MessageType.Warning);
    }
    else if (charData.name == null || charData.name.Length < 1)
    {
      EditorGUILayout.HelpBox("This enemy needs a [Name] before it can be created.", MessageType.Warning);
    }
    else if (GUILayout.Button("Finish and Save", GUILayout.Height(30)))
    {
      SaveCharacterData();
      window.Close();
    }
  }
  void SaveCharacterData()
  {
    string prefabPath;
    string newPrefabPath = "Assets/Prefabs/characters/";
    string dataPath = "Assets/Resources/characterData/data";

    switch (dataSetting)
    {
      case SettingsType.MAGE:

        //create .asset file
        dataPath += "/mage/" + EnemyDesignerWindow.MageInfo.name + ".asset";
        AssetDatabase.CreateAsset(EnemyDesignerWindow.MageInfo, dataPath);

        newPrefabPath += "/mage/" + EnemyDesignerWindow.MageInfo.name + ".prefab";
        prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.MageInfo.prefab);
        AssetDatabase.CopyAsset(prefabPath, newPrefabPath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        GameObject magePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
        if (!magePrefab.GetComponent<Mage>())
          magePrefab.AddComponent(typeof(Mage));

        magePrefab.GetComponent<Mage>().mageData = EnemyDesignerWindow.MageInfo;
        break;
      case SettingsType.WARRIOR:
        dataPath += "/warrior/" + EnemyDesignerWindow.Warriorinfo.name + ".asset";
        AssetDatabase.CreateAsset(EnemyDesignerWindow.Warriorinfo, dataPath);

        newPrefabPath += "/warrior/" + EnemyDesignerWindow.Warriorinfo.name + ".prefab";
        prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.Warriorinfo.prefab);
        AssetDatabase.CopyAsset(prefabPath, newPrefabPath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        GameObject WarriorPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
        if (!WarriorPrefab.GetComponent<Warrior>())
          WarriorPrefab.AddComponent(typeof(Warrior));

        WarriorPrefab.GetComponent<Warrior>().warriorData = EnemyDesignerWindow.Warriorinfo;
        break;
      case SettingsType.ROGUE:
        dataPath += "/rogue/" + EnemyDesignerWindow.RogueInfo.name + ".asset";
        AssetDatabase.CreateAsset(EnemyDesignerWindow.RogueInfo, dataPath);

        newPrefabPath += "/rogue/" + EnemyDesignerWindow.RogueInfo.name + ".prefab";
        prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.RogueInfo.prefab);
        AssetDatabase.CopyAsset(prefabPath, newPrefabPath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        GameObject roguePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
        if (!roguePrefab.GetComponent<Rogue>())
          roguePrefab.AddComponent(typeof(Rogue));

        roguePrefab.GetComponent<Rogue>().rogueData = EnemyDesignerWindow.RogueInfo;
        break;
    }
  }
}

