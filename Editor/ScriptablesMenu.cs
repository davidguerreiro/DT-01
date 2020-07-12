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
}
