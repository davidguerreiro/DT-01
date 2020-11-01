using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu( menuName = "Scriptables" )]
public static class ScriptablesMenu  {


    [MenuItem( "Assets/Create/System/Build")]
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

    [MenuItem( "Assets/Create/Player/PlayerStats")]
    /// <summary>
    /// Add Player State scriptable item
    /// in the scriptables menu.
    /// </summary>
    public static void AddPlayerStatsScriptableObject() {

        var asset = ScriptableObject.CreateInstance<PlayerStats>();

        // if needs preconfiguration, add here.

        var path = AssetDatabase.GetAssetPath( Selection.activeObject );
        path += "/NewPlayerStats.asset";

        ProjectWindowUtil.CreateAsset( asset, path );
    }

    [MenuItem( "Assets/Create/Player/PlayerSkill")]
    /// <summary>
    /// Add Player Skill scriptable item
    /// in the scriptables menu.
    /// </summary>
    public static void AddPlayerSkillScriptableObject() {

        var asset = ScriptableObject.CreateInstance<PlayerSkill>();

        // if needs preconfiguration, add here.

        var path = AssetDatabase.GetAssetPath( Selection.activeObject );
        path += "/newPlayerSkill.asset";

        ProjectWindowUtil.CreateAsset( asset, path );
    }

    [MenuItem( "Assets/Create/Weapons/PlasmaGun")]
    /// <summary>
    /// Add PlasmaGun scriptable item
    /// in the scriptables menu.
    /// </summary>
    public static void AddPlasmaGunScriptableObject() {

        var asset = ScriptableObject.CreateInstance<PlasmaGun>();

        // if needs preconfiguration, add here.

        var path = AssetDatabase.GetAssetPath( Selection.activeObject );
        path += "/newPlasmaGun.asset";

        ProjectWindowUtil.CreateAsset( asset, path );
    }

    [MenuItem( "Assets/Create/Player/Lintern")]
    /// <summary>
    /// Add Lintern scriptable item
    /// in the scriptables menu.
    /// </summary>
    public static void AddLinternScriptableObject() {

        var asset = ScriptableObject.CreateInstance<Lintern>();

        // if needs preconfiguration. add here.

        var path = AssetDatabase.GetAssetPath( Selection.activeObject );
        path += "/newLintern.asset";

        ProjectWindowUtil.CreateAsset( asset, path );
    }

    [MenuItem( "Assets/Create/Enemies/EnemyDataObject")]
    /// <summary>
    /// Add Enemy data object scriptable item
    /// in the scriptables menu.
    /// </summary>
    public static void AddEnemyDataObjectScriptableObject() {

        var asset = ScriptableObject.CreateInstance<EnemyData>();

        // if needs preconfiguration. add here.

        var path = AssetDatabase.GetAssetPath( Selection.activeObject );
        path += "/newEnemyData.asset";

        ProjectWindowUtil.CreateAsset( asset, path );
    }

    [MenuItem( "Assets/Create/Enemies/EnemyAttack")]
    /// <summary>
    /// Add Enemy attack object scriptable item
    /// in the scriptables menu.
    /// </summary>
    public static void AddEnemyAttackObjectScriptableObject() {

        var asset = ScriptableObject.CreateInstance<EnemyAttack>();

        // if needs preconfiguration. add here.

        var path = AssetDatabase.GetAssetPath( Selection.activeObject );
        path += "/newEnemyAttack.asset";

        ProjectWindowUtil.CreateAsset( asset, path );
    }

    [MenuItem( "Assets/Create/Enemies/EnemyType")]
    /// <summary>
    /// Add Enemy type object scriptable item
    /// in the scriptables menu.
    /// </summary>
    public static void AddEnemyTypeObjectScriptableObject() {

        var asset = ScriptableObject.CreateInstance<EnemyType>();

        // if needs preconfiguration. add here.

        var path = AssetDatabase.GetAssetPath( Selection.activeObject );
        path += "/newEnemyType.asset";

        ProjectWindowUtil.CreateAsset( asset, path );
    }

    [MenuItem( "Assets/Create/Items/Generic")]
    /// <summary>
    /// Add Item generic object scriptable item
    /// in the scriptables menu.
    /// </summary>
    public static void AddItemGenericTypeObjectScriptableObject() {

        var asset = ScriptableObject.CreateInstance<ItemData>();

        // if needs preconfiguration. add here.

        var path = AssetDatabase.GetAssetPath( Selection.activeObject );
        path += "/newGenericItem.asset";

        ProjectWindowUtil.CreateAsset( asset, path );
    }

    [MenuItem( "Assets/Create/Items/HealthKit")]
    /// <summary>
    /// Add Health kit generic object scriptable item
    /// in the scriptables menu.
    /// </summary>
    public static void AddHealthKitTypeObjectScriptableObject() {

        var asset = ScriptableObject.CreateInstance<HealthKitData>();

        // if needs preconfiguration. add here.

        var path = AssetDatabase.GetAssetPath( Selection.activeObject );
        path += "/newHealthKit.asset";

        ProjectWindowUtil.CreateAsset( asset, path );
    }

    [MenuItem( "Assets/Create/Player/Inventory")]
    /// <summary>
    /// Add Inventory scriptable item
    /// in the player scriptables menu.
    /// </summary>
    public static void AddInventoryScriptableObject() {

        var asset = ScriptableObject.CreateInstance<Inventory>();

        // if needs preconfiguration. add here.

        var path = AssetDatabase.GetAssetPath( Selection.activeObject );
        path += "/newInventory.asset";

        ProjectWindowUtil.CreateAsset( asset, path );
    }
}
