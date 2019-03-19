#if USE_HOT
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace wxb
{
	public partial class GenHotfixDelegate
	{

		static GenHotfixDelegate()
		{
			TypeToNames = new Dictionary<System.Type, string>();
			TypeToNames.Add(typeof(object), "object");
			TypeToNames.Add(typeof(void), "void");
			TypeToNames.Add(typeof(string), "string");
			TypeToNames.Add(typeof(int), "int");
			TypeToNames.Add(typeof(uint), "uint");

			TypeToNames.Add(typeof(long), "long");
			TypeToNames.Add(typeof(ulong), "ulong");

			TypeToNames.Add(typeof(short), "short");
			TypeToNames.Add(typeof(ushort), "ushort");

			TypeToNames.Add(typeof(byte), "byte");
			TypeToNames.Add(typeof(sbyte), "sbyte");

			TypeToNames.Add(typeof(char), "char");

			TypeToNames.Add(typeof(float), "float");
			TypeToNames.Add(typeof(double), "double");
			TypeToNames.Add(typeof(bool), "bool");

		}

		static Dictionary<System.Type, string> TypeToNames;

		public enum ParamType
		{
			Normal, // 正常的参数1`
			Out, // out参数
		}
		// 参数
		public struct Param
		{
			public Param(System.Type realType)
			{
				this.realType = realType;
				this.type = ParamType.Normal;
			}

			public Param(System.Type realType, ParamType type)
			{
				this.realType = realType;
				this.type = type;

				if (type == ParamType.Normal)
				{
					if (realType.IsArray)
					{

					}
					else
					{
						if (!realType.IsValueType && !realType.IsByRef)
							this.realType = typeof(System.Object);
					}
				}
			}

			public System.Type realType { get; private set; }
			public ParamType type { get; private set; }

			public string oid { get { return realType.FullName + type; } }

			public wxb.Generator.Parameter Gen(string name)
			{
				return new wxb.Generator.Parameter(
					realType.IsByRef ? (type == ParamType.Out ? Generator.Parameter.Type.Out : Generator.Parameter.Type.Ref) : Generator.Parameter.Type.Normal,
					name,
					GetTypeName(realType));
			}

			public string toString()
			{
				if (realType.IsByRef)
				{
					if (type == ParamType.Out)
						return "out " + GetTypeName(realType);
					return "ref " + GetTypeName(realType);
				}

				return GetTypeName(realType);
			}
		}
		// 函数的原形
		public class Funs
		{
			// 参数列表
			public List<Param> Params { get; private set; }
			public Param Return { get; private set; }
			// 引用到的方法
			public HashSet<MethodBase> methods = new HashSet<MethodBase>();

			public Funs(MethodInfo info, bool isAdd)
			{
				var rp = info.ReturnParameter;
				Return = new Param(rp.ParameterType);

				Params = new List<Param>();
				if (isAdd)
					Params.Add(new Param(typeof(object), ParamType.Normal));
				foreach (var ator in info.GetParameters())
				{
					if (ator.IsDefined(typeof(System.ParamArrayAttribute), false))
					{
						Params.Add(new Param(ator.ParameterType));
					}
					else
					{
						Params.Add(new Param(ator.ParameterType, ator.IsOut ? ParamType.Out : ParamType.Normal));
					}
				}

				methods.Add(info);
			}

			static bool isEditorClass(System.Type type)
			{
				if (type.IsArray)
					return isEditorClass(type.GetElementType());

#if UNITY_EDITOR
				if (type.GetCustomAttributes(typeof(EditorClass), true).Length != 0)
					return true;
#endif
				if (type.IsNested)
					return isEditorClass(type.DeclaringType);
				return false;
			}

			static bool isPublic(System.Type type)
			{
				if (type.IsArray)
					return isPublic(type.GetElementType());

				if (type.FullName.EndsWith("&"))
					return isPublic(type.Assembly.GetType(type.FullName.Substring(0, type.FullName.Length - 1)));

				if (type.IsNested)
					return type.IsNestedPublic && isPublic(type.DeclaringType);

				return type.IsPublic;
			}

			public bool isExport
			{
				get
				{
					if (!isPublic(Return.realType) || isEditorClass(Return.realType))
						return false;

					for (int i = 0; i < Params.Count; ++i)
					{
						if (!isPublic(Params[i].realType) || isEditorClass(Return.realType))
							return false;
					}

					return true;
				}
			}

			public Funs(ConstructorInfo info, bool isAdd)
			{
				Return = new Param(typeof(void), ParamType.Normal);

				Params = new List<Param>();
				if (isAdd)
					Params.Add(new Param(typeof(object), ParamType.Normal));

				foreach (var ator in info.GetParameters())
				{
					Params.Add(new Param(ator.ParameterType, ator.IsOut ? ParamType.Out : ParamType.Normal));
				}
				methods.Add(info);
			}

			public Funs(System.Type delegat) : this(delegat.GetMethod("Invoke"), false)
			{

			}

			public string oid
			{
				get
				{
					System.Text.StringBuilder sb = new System.Text.StringBuilder();
					sb.Append(Return.oid);
					sb.Append(Params.Count);
					for (int i = 0; i < Params.Count; ++i)
						sb.Append(Params[i].oid);
					return sb.ToString();
				}
			}

			void GenInfo(MethodBase method, System.Text.StringBuilder sb)
			{
				if (method.IsConstructor || !(method is MethodInfo))
				{
					sb.Append("void");
					sb.Append(" ");
					sb.Append(GetTypeName(method.ReflectedType));
				}
				else
				{
					MethodInfo info = (MethodInfo)method;
					sb.Append(GetTypeName(info.ReturnType));
					sb.Append(" ");

					sb.Append(GetTypeName(info.ReflectedType));
					sb.Append(".");
					sb.Append(method.Name);
				}

				sb.Append("(");
				var parameters = method.GetParameters();
				for (int i = 0; i < parameters.Length; ++i)
				{
					var type = parameters[i].ParameterType;
					string name = GetTypeName(type);
					string suffix = "";
					if (parameters[i].IsOut)
					{
						if (type.IsByRef)
							suffix = "ref ";
						else
							suffix = "out ";
					}

					sb.Append(suffix + name);
					if (i != parameters.Length - 1)
						sb.Append(", ");
				}
				sb.Append(");");
			}

			public void toInfo(string suffix, System.Text.StringBuilder sb)
			{
				return;
				if (methods.Count > 30)
					sb.AppendLine(string.Format("{0}//{1}", suffix, methods.Count));
				foreach (var ator in methods)
				{
					sb.Append(suffix);
					sb.Append("//");
					GenInfo(ator, sb);
					sb.AppendLine();
				}
			}

			public string toString(string name, out int paramCount)
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				List<Generator.Parameter> parameters = new List<Generator.Parameter>();
				sb.AppendFormat("public {0} {1}", GetTypeName(Return.realType), name);
				if (Params.Count == 0)
				{
					sb.AppendFormat("()");
				}
				else
				{
					sb.AppendFormat("({0} p0", Params[0].toString());
					parameters.Add(Params[0].Gen("p0"));
					for (int i = 1; i < Params.Count; ++i)
					{
						sb.AppendFormat(", {0} p{1}", Params[i].toString(), i.ToString());
						parameters.Add(Params[i].Gen("p" + i.ToString()));
					}

					sb.Append(")");
				}

				sb.AppendLine();
				sb.Append("        {");

				sb.AppendLine();
				Generator.InILCode(sb, name, GetTypeName(Return.realType), parameters, out paramCount);

				sb.Append("        }");

				return sb.ToString();
			}
		}
		public static string GetTypeName(System.Type type)
		{
			string name;
			if (TypeToNames.TryGetValue(type, out name))
				return name;

			string realClsName;
			bool isByRef;
			GetClassName(type, out name, out realClsName, out isByRef, false);

			TypeToNames.Add(type, realClsName);
			return realClsName;
		}

		static void GetClassName(System.Type type, out string clsName, out string realClsName, out bool isByRef, bool simpleClassName = false)
		{
			isByRef = type.IsByRef;
			if (isByRef)
				type = type.GetElementType();
			bool isArray = type.IsArray;
			if (isArray)
				type = type.GetElementType();
			string realNamespace = null;
			bool isNestedGeneric = false;
			if (type.IsNested)
			{
				string bClsName, bRealClsName;
				bool tmp;
				var rt = type.ReflectedType;
				if (rt.IsGenericType && rt.IsGenericTypeDefinition)
				{
					if (type.IsGenericType)
					{
						rt = rt.MakeGenericType(type.GetGenericArguments());
						isNestedGeneric = true;
					}
				}
				if (rt == type)
				{
					wxb.L.LogErrorFormat("type:{0} -> {1}", type.Name, rt.Name);
				}

				GetClassName(rt, out bClsName, out bRealClsName, out tmp);
				clsName = simpleClassName ? "" : bClsName + "_";
				realNamespace = bRealClsName + ".";
			}
			else
			{
				clsName = simpleClassName ? "" : (!string.IsNullOrEmpty(type.Namespace) ? type.Namespace.Replace(".", "_") + "_" : "");
				realNamespace = !string.IsNullOrEmpty(type.Namespace) ? type.Namespace + "." : null;
			}
			clsName = clsName + type.Name.Replace(".", "_").Replace("`", "_").Replace("<", "_").Replace(">", "_");
			bool isGeneric = false;
			string ga = null;
			if (type.IsGenericType && !isNestedGeneric)
			{
				isGeneric = true;
				clsName += "_";
				ga = "<";
				var args = type.GetGenericArguments();
				bool first = true;
				foreach (var j in args)
				{
					if (first)
						first = false;
					else
					{
						clsName += "_";
						ga += ", ";
					}
					string a, b;
					bool tmp;
					if (type == j)
					{
						wxb.L.LogErrorFormat("type:{0} -> {1}", type.Name, j.Name);
					}

					GetClassName(j, out a, out b, out tmp, true);
					clsName += a;
					ga += b;
				}
				ga += ">";
			}
			if (!simpleClassName)
				clsName += "_Binding";
			if (isArray)
				clsName += "_Array";

			realClsName = realNamespace;
			if (isGeneric)
			{
				int idx = type.Name.IndexOf("`");
				if (idx > 0)
				{
					realClsName += type.Name.Substring(0, idx);
					realClsName += ga;
				}
				else
					realClsName += type.Name;
			}
			else
				realClsName += type.Name;

			if (isArray)
				realClsName += "[]";
		}
	}
}
#endif