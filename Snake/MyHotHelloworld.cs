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
		[ReplaceFunction()]
		static void Start(MyHelloWorld world)
		{
			//__Hotfix_TestOne_0.Invoke(world);
			RefType refType = new RefType((object)world);
			refType.SetField("str","this is bad");

			__Hotfix_Start.Invoke(world);
		}

		[ReplaceFunction()]
		static void TestOne(MyHelloWorld world)
		{
			//__Hotfix_TestOne_0.Invoke(world);
			wxb.L.Log("From hot test one");
		}
	}

	[AutoInitAndRelease]
	[ReplaceType(typeof(Player))]
	public static class MyHotPlayer
	{
		[ReplaceFunction()]
		static void TestOne(Player world)
		{
			wxb.L.Log("From hot test one Player!!!");
		}
	}
}
#endif