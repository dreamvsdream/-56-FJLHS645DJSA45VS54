using System;
using GameMain;

namespace GameHotfix
{
	public static class Init
	{
		public static void Start()
		{
#if ILRuntime
			if (!Define.IsILRuntime)
			{
				Log.Error("mono层是mono模式, 但是Hotfix层是ILRuntime模式");
			}
#else
			if (Define.IsILRuntime)
			{
				Log.Error("mono层是ILRuntime模式, Hotfix层是mono模式");
			}
#endif
			
			try
			{
				// 注册热更层回调
				GameMain.Game.Hotfix.Update = () => { Update(); };
				GameMain.Game.Hotfix.LateUpdate = () => { LateUpdate(); };
				GameMain.Game.Hotfix.OnApplicationQuit = () => { OnApplicationQuit(); };

				temp = new ILBehaviourTest();

			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		static ILBehaviourTest temp;

		public static void Update()
		{
			try
			{
				temp?.Update();
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static void LateUpdate()
		{
			try
			{
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static void OnApplicationQuit()
		{

		}
	}
}