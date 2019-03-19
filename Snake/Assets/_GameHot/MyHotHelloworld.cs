#if USE_HOT
using wxb;
#pragma warning disable 169
#pragma warning disable 649

namespace hot
{
	[AutoInitAndRelease]
	[ReplaceType(typeof(MyHelloWorld))]
	public static class MyHotHelloworld
	{
		//static Hotfix __Hotfix_TestOne_0;
		static Hotfix __Hotfix_Start; // 保存回调自身数据
		static Hotfix __Hotfix_TestOne; // 保存回调自身数据
		[ReplaceFunction()]
		static void Start(MyHelloWorld world)
		{
			//__Hotfix_TestOne_0.Invoke(world);
			RefType refType = new RefType((object)world);
			refType.SetField("str", "this is bad");

			__Hotfix_Start.Invoke(world);
		}

		[ReplaceFunction()]
		static void TestOne(MyHelloWorld world)
		{
			RefType refType = new RefType((object)world);
			refType.SetField("str", "this is bad");

			__Hotfix_TestOne.Invoke(world);
			wxb.L.Log("From hot test one");
			
		}

	}

	[AutoInitAndRelease]
	[ReplaceType(typeof(ShowInfo))]
	public static class HotShowinfor
	{
		static Hotfix __Hotfix_ShowMyInfo; // 保存回调自身数据
		[ReplaceFunction()]
		static void ShowMyInfo(ShowInfo showInfo)
		{
			RefType refType = new RefType(showInfo);
			refType.SetField("info", "this is goodd 5");
			__Hotfix_ShowMyInfo.Invoke(showInfo);
		}
	}
}
#endif