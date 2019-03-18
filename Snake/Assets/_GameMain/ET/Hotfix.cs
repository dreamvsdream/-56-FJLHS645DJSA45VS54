using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using UniRx.Async;
#if !ILRuntime
using System.Reflection;
#endif


namespace GameMain
{
	public sealed class Hotfix
	{
		private ILRuntime.Runtime.Enviorment.AppDomain appDomain;
#if ILRuntime
		private MemoryStream dllStream;
		private MemoryStream pdbStream;
#else
		private Assembly assembly;
#endif

		private IStaticMethod start;
		private List<Type> hotfixTypes;

		public Action Update;
		public Action LateUpdate;
		public Action OnApplicationQuit;

		public void GotoHotfix()
		{
#if ILRuntime
			ILHelper.InitILRuntime(this.appDomain);
#endif
			this.start.Run();
		}

		public List<Type> GetHotfixTypes()
		{
			return this.hotfixTypes;
		}

		public async UniTask LoadHotfixAssembly()
		{
			var dll = Loader.LoadAssetAsync<TextAsset>("GameHotfix.dll");
			var pdb = Loader.LoadAssetAsync<TextAsset>("GameHotfix.pdb");
			await dll;
			await pdb;

			byte[] assBytes = dll.Result.bytes;
			byte[] pdbBytes = pdb.Result.bytes;

#if ILRuntime
			Log.Debug($"当前使用的是ILRuntime模式");
			this.appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();

			this.dllStream = new MemoryStream(assBytes);
			this.pdbStream = new MemoryStream(pdbBytes);
			this.appDomain.LoadAssembly(this.dllStream, this.pdbStream, new Mono.Cecil.Pdb.PdbReaderProvider());

			this.start = new ILStaticMethod(this.appDomain, "GameHotfix.Init", "Start", 0);
			//this.start = new ILStaticMethod(this.appDomain, "GameHotfix.TestHotFix", "Test", 0);
			
			this.hotfixTypes = this.appDomain.LoadedTypes.Values.Select(x => x.ReflectionType).ToList();
#else
			Log.Debug($"当前使用的是Mono模式");

			this.assembly = Assembly.Load(assBytes, pdbBytes);

			Type hotfixInit = this.assembly.GetType("GameHotfix.Init");
			//Type hotfixInit = this.assembly.GetType("GameHotfix.TestHotFix");
			this.start = new MonoStaticMethod(hotfixInit, "Start");

			this.hotfixTypes = this.assembly.GetTypes().ToList();
#endif
			await new UniTask();
		}
		
		T CreateInstance<T>(string typeName) where T:class
		{
			if (Define.IsILRuntime)
			{
				return appDomain.Instantiate("TypeName") as T;
			}
			else
			{
				Type t = Type.GetType("TypeName");
				return Activator.CreateInstance(t) as T;
			}
		}

#if ILRuntime
		private static void InitILRuntime(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
		{
			//// 注册重定向函数

			//// 注册委托
			appdomain.DelegateManager.RegisterMethodDelegate<List<object>>();
			appdomain.DelegateManager.RegisterMethodDelegate<byte[], int, int>();
			//appdomain.DelegateManager.RegisterMethodDelegate<AChannel, System.Net.Sockets.SocketError>();
			//appdomain.DelegateManager.RegisterMethodDelegate<IResponse>();
			//appdomain.DelegateManager.RegisterMethodDelegate<Session, object>();
			//appdomain.DelegateManager.RegisterMethodDelegate<Session, ushort, MemoryStream>();
			//appdomain.DelegateManager.RegisterMethodDelegate<Session>();
			//appdomain.DelegateManager.RegisterMethodDelegate<ILTypeInstance>();
			//appdomain.DelegateManager.RegisterFunctionDelegate<Google.Protobuf.Adapt_IMessage.Adaptor>();
			//appdomain.DelegateManager.RegisterMethodDelegate<Google.Protobuf.Adapt_IMessage.Adaptor>();

			//CLRBindings.Initialize(appdomain);

			// 注册适配器
			//Assembly assembly = typeof(Init).Assembly;
			//foreach (Type type in assembly.GetTypes())
			//{
			//	object[] attrs = type.GetCustomAttributes(typeof(ILAdapterAttribute), false);
			//	if (attrs.Length == 0)
			//	{
			//		continue;
			//	}
			//	object obj = Activator.CreateInstance(type);
			//	CrossBindingAdaptor adaptor = obj as CrossBindingAdaptor;
			//	if (adaptor == null)
			//	{
			//		continue;
			//	}
			//	appdomain.RegisterCrossBindingAdaptor(adaptor);
			//}

			LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appdomain);
		}
#endif
	}

	public abstract class IStaticMethod
	{
		public abstract void Run();
		public abstract void Run(object a);
		public abstract void Run(object a, object b);
		public abstract void Run(object a, object b, object c);
	}

	public class ILStaticMethod : IStaticMethod
	{
		private readonly ILRuntime.Runtime.Enviorment.AppDomain appDomain;
		private readonly IMethod method;
		private readonly object[] param;

		public ILStaticMethod(ILRuntime.Runtime.Enviorment.AppDomain appDomain, string typeName, string methodName, int paramsCount)
		{
			this.appDomain = appDomain;
			this.method = appDomain.GetType(typeName).GetMethod(methodName, paramsCount);
			this.param = new object[paramsCount];
		}

		public override void Run()
		{
			this.appDomain.Invoke(this.method, null, this.param);
		}

		public override void Run(object a)
		{
			this.param[0] = a;
			this.appDomain.Invoke(this.method, null, param);
		}

		public override void Run(object a, object b)
		{
			this.param[0] = a;
			this.param[1] = b;
			this.appDomain.Invoke(this.method, null, param);
		}

		public override void Run(object a, object b, object c)
		{
			this.param[0] = a;
			this.param[1] = b;
			this.param[2] = c;
			this.appDomain.Invoke(this.method, null, param);
		}
	}

	public class MonoStaticMethod : IStaticMethod
	{
		private readonly MethodInfo methodInfo;

		private readonly object[] param;

		public MonoStaticMethod(Type type, string methodName)
		{
			this.methodInfo = type.GetMethod(methodName);
			this.param = new object[this.methodInfo.GetParameters().Length];
		}

		public override void Run()
		{
			this.methodInfo.Invoke(null, param);
		}

		public override void Run(object a)
		{
			this.param[0] = a;
			this.methodInfo.Invoke(null, param);
		}

		public override void Run(object a, object b)
		{
			this.param[0] = a;
			this.param[1] = b;
			this.methodInfo.Invoke(null, param);
		}

		public override void Run(object a, object b, object c)
		{
			this.param[0] = a;
			this.param[1] = b;
			this.param[2] = c;
			this.methodInfo.Invoke(null, param);
		}
	}
}

