using GameMain;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ETEditor
{
    //[InitializeOnLoad]
    public class Startup
    {
		//private const string ScriptAssembliesDir = "Library/ScriptAssemblies";
		//private const string CodeDir = "Assets/Res/Code/";
		//private const string HotfixDll = "DyncDll.dll";
		//private const string HotfixPdb = "DyncDll.pdb";

		//     static Startup()
		//     {
		//         File.Copy(Path.Combine(ScriptAssembliesDir, HotfixDll), Path.Combine(CodeDir, "DyncDll.dll.bytes"), true);
		//         File.Copy(Path.Combine(ScriptAssembliesDir, HotfixPdb), Path.Combine(CodeDir, "DyncDll.pdb.bytes"), true);

		//File.Copy(Path.Combine(ScriptAssembliesDir, HotfixDll), Path.Combine(CodeDir, "DyncDll.dll"), true);
		//File.Copy(Path.Combine(ScriptAssembliesDir, HotfixPdb), Path.Combine(CodeDir, "DyncDll.pdb"), true);
		//Debug.Log($"复制GameHotfix.dll, GameHotfix.pdb到Res/Code完成");
		//         AssetDatabase.Refresh ();
		//     }
		//[UnityEditor.Callbacks.PostProcessScene]
		[UnityEditor.MenuItem("XIL/Copy Dll Editor Selection _&h", false, 2)]
		public static void Copy()
		{
			File.Copy(ResourcesPath.dyncDllPath, Path.Combine(ResourcesPath.CodeDir, "DyncDll.dll.bytes"), true);
			File.Copy(ResourcesPath.dyncPdbPath, Path.Combine(ResourcesPath.CodeDir, "DyncDll.pdb.bytes"), true);

			//File.Copy(Path.Combine(ScriptAssembliesDir, HotfixDll), Path.Combine(CodeDir, "DyncDll.dll"), true);
			//File.Copy(Path.Combine(ScriptAssembliesDir, HotfixPdb), Path.Combine(CodeDir, "DyncDll.pdb"), true);
			Debug.Log($"复制DyncDll.dll, DyncDll.pdb到Res/Code完成");
			AssetDatabase.Refresh();
		}
    }
}