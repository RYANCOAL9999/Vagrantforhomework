using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Koocell.Unity.Editor {

	[InitializeOnLoad]
	public class KoocellEditorUtilities : MonoBehaviour {

		static KoocellEditorUtilities () {
			Initialize();
		}

		static void Initialize () {
//			defaultMix = EditorPrefs.GetFloat(DEFAULT_MIX_KEY, 0.2f);
//
//			DirectoryInfo rootDir = new DirectoryInfo(Application.dataPath);
//			FileInfo[] files = rootDir.GetFiles("SpineEditorUtilities.cs", SearchOption.AllDirectories);
//			editorPath = Path.GetDirectoryName(files[0].FullName.Replace("\\", "/").Replace(Application.dataPath, "Assets"));
//			editorGUIPath = editorPath + "/GUI";
//
//			Icons.Initialize();
//
//			assetsImportedInWrongState = new HashSet<string>();
//			skeletonRendererTable = new Dictionary<int, GameObject>();
//			skeletonUtilityBoneTable = new Dictionary<int, SkeletonUtilityBone>();
//			boundingBoxFollowerTable = new Dictionary<int, BoundingBoxFollower>();
//
//			EditorApplication.hierarchyWindowChanged += HierarchyWindowChanged;
//			EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
//
//			HierarchyWindowChanged();
//			initialized = true;
		}
	}
}