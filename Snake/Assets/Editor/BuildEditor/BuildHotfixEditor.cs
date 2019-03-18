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
        private const string ScriptAssembliesDir = "Library/ScriptAssemblies";
        private const string CodeDir = "Assets/Res/Code/";
        private const string HotfixDll = "GameHotfix.dll";
        private const string HotfixPdb = "GameHotfix.pdb";

        static Startup()
        {
            File.Copy(Path.Combine(ScriptAssembliesDir, HotfixDll), Path.Combine(CodeDir, "GameHotfix.dll.bytes"), true);
            File.Copy(Path.Combine(ScriptAssembliesDir, HotfixPdb), Path.Combine(CodeDir, "GameHotfix.pdb.bytes"), true);
            Debug.Log($"复制GameHotfix.dll, GameHotfix.pdb到Res/Code完成");
            AssetDatabase.Refresh ();
        }

		[MenuItem("Tools/ILRuntime/Copy .Dll")]
		public static void Copy()
		{
			File.Copy(Path.Combine(ScriptAssembliesDir, HotfixDll), Path.Combine(CodeDir, "GameHotfix.dll.bytes"), true);
			File.Copy(Path.Combine(ScriptAssembliesDir, HotfixPdb), Path.Combine(CodeDir, "GameHotfix.pdb.bytes"), true);
			Debug.Log($"复制GameHotfix.dll, GameHotfix.pdb到Res/Code完成");
			AssetDatabase.Refresh();
		}
    }
}