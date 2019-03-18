using System.Collections;
using UnityEngine;
using System;
using System.Threading;
using UniRx.Async;
using GameHotfix;

namespace GameMain
{
	public class Init : MonoBehaviour
	{
		ILBehaviourTest temp;
		private void Start()
		{
			this.StartAsync();
		}

		private async void StartAsync()
		{
			try
			{
				SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);

				await Game.Hotfix.LoadHotfixAssembly();

				Game.Hotfix.GotoHotfix();

				DontDestroyOnLoad(gameObject);

				temp = Game.Hotfix.CreateInstance<ILBehaviourTest>(this.gameObject);
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		private void Update()
		{
			OneThreadSynchronizationContext.Instance.Update();
			Game.Hotfix.Update?.Invoke();
			if (temp != null)
			{
				temp.Update();
			}
			else
			{
				Debug.Log("Nu");
			}
			//Game.EventSystem.Update();
		}

		private void LateUpdate()
		{
			Game.Hotfix.LateUpdate?.Invoke();
			//Game.EventSystem.LateUpdate();
		}

		private void OnApplicationQuit()
		{
			Game.Hotfix.OnApplicationQuit?.Invoke();
			Game.Close();
		}
	}
}

