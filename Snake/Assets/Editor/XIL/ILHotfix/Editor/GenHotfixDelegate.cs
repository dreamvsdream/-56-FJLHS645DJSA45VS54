#if USE_HOT
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using static wxb.GenHotfixDelegate;

namespace wxb
{
	public partial class GenHotfixDelegate2
	{

		const string file_format = @"#if USE_HOT && {0}
namespace IL
{{
    public partial class DelegateBridge
    {{
{1}
    }}
}}
";
		//[MenuItem("XIL/测试")]
		//static void GenX()
		//{
		//    var flag = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly;
		//    var type = typeof(HelloWorld);
		//    var methods = type.GetConstructors(flag);
		//    wxb.L.LogFormat(methods.Length.ToString());
		//}

		[MenuItem("XIL/一键生成")]
		public static void GenOne()
		{
			Gen();
			AutoRegILType.Build();
		}

		[MenuItem("XIL/一键清除")]
		public static void GenClear()
		{
			Clear();
			AutoRegILType.Clear();
		}

		[MenuItem("XIL/注册需要热更的类")]
		static void Gen()
		{
			AutoCode(GenAutoExport.FixMarkIL());
		}

		[MenuItem("XIL/清除需要热更的类")]
		static void Clear()
		{
			AutoCode(new List<string>());
		}

		static bool IsUnityObjectType(System.Type type)
		{
			if (type == typeof(UnityEngine.Object))
				return true;
			if (type.BaseType == null)
				return false;

			return IsUnityObjectType(type.BaseType);
		}

		static bool IsEditorAttribute(MemberInfo minfo)
		{
			if (minfo.GetCustomAttributes(typeof(EditorField), true).Length != 0)
				return true;

			if (minfo is MethodBase)
			{
				var info = minfo as MethodBase;
				if (info.IsSpecialName && (info.Name.StartsWith("get_") || info.Name.StartsWith("set_")))
				{
					var flag = BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
					var propertys = info.ReflectedType.GetProperties(flag);
					string name = info.Name.Substring(4);
					foreach (var ator in propertys)
					{
						if (ator.Name == name)
						{
							if (ator.GetCustomAttributes(typeof(EditorField), true).Length != 0)
								return true;
						}
					}
				}
			}

			return false;
		}

		public static void AutoCode(List<string> classes)
		{
			// 自动生成所有需要委托
			Dictionary<string, Funs> allfuns = new Dictionary<string, Funs>(); // 所有的需要导出的函数原形列表

			var flag = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly;
			var alls = new List<System.Type>();

			HashSet<int> ObjectsParamCount = new HashSet<int>();
			HashSet<string> exports = new HashSet<string>(classes);
			foreach (var ator in System.AppDomain.CurrentDomain.GetAssemblies())
			{
				if (ator.FullName.StartsWith("System"))
					continue;

				foreach (var type in ator.GetTypes())
				{
					if (exports.Contains(type.FullName.Replace('+', '/')) || type.GetCustomAttributes(typeof(HotfixAttribute), false).Length != 0)
					{
						//wxb.L.LogFormat("type:{0}", type.FullName);
						if (type.IsGenericType)
							continue;

						foreach (var method in type.GetMethods(flag))
						{
							if (method.IsGenericMethod)
								continue;

							if (IsEditorAttribute(method))
								continue;

							Funs fun = new Funs(method, method.IsStatic ? false : true);
							if (!fun.isExport)
								continue;

							string key = fun.oid;
							Funs rs;
							if (allfuns.TryGetValue(key, out rs))
							{
								rs.methods.Add(method);
								continue;
							}

							allfuns.Add(key, fun);
						}

						// MonoBehaviour类型不重载构造函数
						if (IsUnityObjectType(type))
							continue;

						foreach (var ctor in type.GetConstructors(flag))
						{
							if (ctor.IsGenericMethod)
								continue;

							if (IsEditorAttribute(ctor))
								continue;

							Funs fun = new Funs(ctor, ctor.IsStatic ? false : true);
							if (!fun.isExport)
								continue;

							string key = fun.oid;
							Funs rs;
							if (allfuns.TryGetValue(key, out rs))
							{
								rs.methods.Add(ctor);
								continue;
							}

							allfuns.Add(key, fun);
						}
					}
				}
			}

			string marco, suffix;
			AutoRegILType.GetPlatform(out marco, out suffix);
			string file = string.Format("Assets/_GameMain/XIL/Auto/GenDelegateBridge_{0}.cs", suffix);

			System.IO.Directory.CreateDirectory(file.Substring(0, file.LastIndexOf('/')));

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			string name = "__Gen_Delegate_Imp";
			int index = 0;
			foreach (var ator in allfuns)
			{
				ator.Value.toInfo("        ", sb);
				int paramCount = 0;
				sb.AppendFormat("        {0}", ator.Value.toString(name + (++index), out paramCount));
				sb.AppendLine();
				if (paramCount != 0)
					ObjectsParamCount.Add(paramCount);
			}

			System.IO.File.WriteAllText(file, string.Format(file_format + "#endif", marco, sb.ToString()));
			wxb.L.LogFormat("count:{0}", allfuns.Count);

			sb.Length = 0;
			sb.AppendLine(string.Format("countType:{0}", classes.Count));
			foreach (var ator in classes)
				sb.AppendLine(ator);
			wxb.L.LogFormat(sb.ToString());

			GeneratorObjects.Gen(ObjectsParamCount);
			AssetDatabase.Refresh();
		}
	}
}
#endif