using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu( menuName = "Scriptables" )]
public static class ScriptablesMenu  {


    [MenuItem( "Assets/Create/Scriptables/Build")]
    /// <summary>
    /// Add build scriptable item
    /// in the scriptables menu.
    /// </summary>
    public static void AddBuildScriptableObject() {

        var asset = ScriptableObject.CreateInstance<BuildInfo>();

        // if needs preconfiguration, add here.

        var path = AssetDatabase.GetAssetPath( Selection.activeObject );
        path += "/NewBuild.asset";

        ProjectWindowUtil.CreateAsset( asset, path );
    }

    [MenuItem( "Assets/Create/Scriptables/PlayerStats")]
    /// <summary>
    /// Add build scriptable item
    /// in the scriptables menu.
    /// </summary>
    public static void AddPlayerStatsScriptableObject() {

        var asset = ScriptableObject.CreateInstance<PlayerStats>();

        // if needs preconfiguration, add here.

        var path = AssetDatabase.GetAssetPath( Selection.activeObject );
        path += "/NewPlayerStats.asset";

        ProjectWindowUtil.CreateAsset( asset, path );
    }

    [MenuItem( "Assets/Create/Scriptables/PlayerSkill")]
    /// <summary>
    /// Add build scriptable item
    /// in the scriptables menu.
    /// </summary>
    public static void AddPlayerSkillScriptableObject() {

        var asset = ScriptableObject.CreateInstance<PlayerSkill>();

        // if needs preconfiguration, add here.

        var path = AssetDatabase.GetAssetPath( Selection.activeObject );
        path += "/newPlayerSkill.asset";

        ProjectWindowUtil.CreateAsset( asset, path );
    }
}
