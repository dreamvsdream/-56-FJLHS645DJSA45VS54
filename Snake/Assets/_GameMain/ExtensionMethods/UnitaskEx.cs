using UnityEngine;
using UniRx.Async;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameMain
{
	public static class UnitaskEx
	{
		public static UniTask<T> ToMyUnitask<T>(this IAsyncOperation<T> t)
		{
			var utcs = new UniTaskCompletionSource<T>();
			_WrapperAsync();
			return utcs.Task;

			async void _WrapperAsync()
			{
				await t;
				if (t.IsDone)
				{
					utcs.TrySetResult(t.Result);
				}
				else if (t.IsValid)
				{
					utcs.TrySetCanceled();
					utcs.TrySetException(new System.Exception("IsValid"));
				}
			}
		}

		public static UniTask<T> ToMyUnitask<T>(this ResourceRequest t) where T : class
		{
			var utcs = new UniTaskCompletionSource<T>();
			_WrapperAsync();
			return utcs.Task;

			async void _WrapperAsync()
			{
				await t;
				if (t.isDone)
				{
					utcs.TrySetResult(t.asset as T);
				}
			}
		}

		public static UniTask<AssetBundle> ToMyUnitask(this AssetBundleCreateRequest t)
		{
			var utcs = new UniTaskCompletionSource<AssetBundle>();
			_WrapperAsync();
			return utcs.Task;

			async void _WrapperAsync()
			{
				await t;
				if (t.isDone)
				{
					utcs.TrySetResult(t.assetBundle);
				}
			}
		}

		public static UniTask<T> ToMyUnitask<T>(this AssetBundleRequest t) where T : class
		{
			var utcs = new UniTaskCompletionSource<T>();
			_WrapperAsync();
			return utcs.Task;

			async void _WrapperAsync()
			{
				await t;
				if (t.isDone)
				{
					utcs.TrySetResult(t.asset as T);
				}
			}
		}
	}
}