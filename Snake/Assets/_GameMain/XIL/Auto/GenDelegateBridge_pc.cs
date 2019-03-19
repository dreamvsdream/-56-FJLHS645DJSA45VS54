#if USE_HOT && UNITY_STANDALONE_WIN
namespace IL
{
    public partial class DelegateBridge
    {
        public void __Gen_Delegate_Imp1(object p0)
        {
            using (var pObjs = new Objects(p0))
            {
                methodInfo.Invoke(null, pObjs.objs);
            }
        }
        public void __Gen_Delegate_Imp2(int p0, int p1, int p2, int p3, object p4, object p5, object p6, object p7, UnityEngine.Vector3 p8, UnityEngine.Vector2 p9, UnityEngine.Vector4 p10)
        {
            using (var pObjs = new Objects(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10))
            {
                methodInfo.Invoke(null, pObjs.objs);
            }
        }
        public void __Gen_Delegate_Imp3()
        {
            using (var pObjs = new EmptyObjs())
            {
                methodInfo.Invoke(null, pObjs.objs);
            }
        }
        public int __Gen_Delegate_Imp4()
        {
            using (var pObjs = new EmptyObjs())
            {
                int result = default(int);
                result = (int)methodInfo.Invoke(null, pObjs.objs);
                return result;
            }
        }
        public void __Gen_Delegate_Imp5(object p0, ref System.Int32 p1, out System.Int32 p2)
        {
            p2 = default(System.Int32);
            using (var pObjs = new Objects(p0, new RefOutParam<System.Int32>(p1), new RefOutParam<System.Int32>(p2)))
            {
                var param = pObjs.objs;
                methodInfo.Invoke(null, param);
                p1 = ((RefOutParam<System.Int32>)param[1]).value;
                p2 = ((RefOutParam<System.Int32>)param[2]).value;
            }
        }
        public int __Gen_Delegate_Imp6(object p0)
        {
            using (var pObjs = new Objects(p0))
            {
                int result = default(int);
                result = (int)methodInfo.Invoke(null, pObjs.objs);
                return result;
            }
        }
        public void __Gen_Delegate_Imp7(object p0, object p1)
        {
            using (var pObjs = new Objects(p0, p1))
            {
                methodInfo.Invoke(null, pObjs.objs);
            }
        }
        public void __Gen_Delegate_Imp8(object p0, int p1, long p2)
        {
            using (var pObjs = new Objects(p0, p1, p2))
            {
                methodInfo.Invoke(null, pObjs.objs);
            }
        }
        public void __Gen_Delegate_Imp9(object p0, int p1, object p2)
        {
            using (var pObjs = new Objects(p0, p1, p2))
            {
                methodInfo.Invoke(null, pObjs.objs);
            }
        }
        public string __Gen_Delegate_Imp10()
        {
            using (var pObjs = new EmptyObjs())
            {
                string result = default(string);
                result = (string)methodInfo.Invoke(null, pObjs.objs);
                return result;
            }
        }
        public bool __Gen_Delegate_Imp11(float p0, float p1, float p2)
        {
            using (var pObjs = new Objects(p0, p1, p2))
            {
                bool result = default(bool);
                result = (bool)methodInfo.Invoke(null, pObjs.objs);
                return result;
            }
        }
        public UnityEngine.Rect __Gen_Delegate_Imp12(object p0)
        {
            using (var pObjs = new Objects(p0))
            {
                UnityEngine.Rect result = default(UnityEngine.Rect);
                result = (UnityEngine.Rect)methodInfo.Invoke(null, pObjs.objs);
                return result;
            }
        }
        public UnityEngine.Rect __Gen_Delegate_Imp13(object p0, System.Nullable<System.Single> p1)
        {
            using (var pObjs = new Objects(p0, p1))
            {
                UnityEngine.Rect result = default(UnityEngine.Rect);
                result = (UnityEngine.Rect)methodInfo.Invoke(null, pObjs.objs);
                return result;
            }
        }
        public void __Gen_Delegate_Imp14(int p0)
        {
            using (var pObjs = new Objects(p0))
            {
                methodInfo.Invoke(null, pObjs.objs);
            }
        }
        public UnityEngine.ResourceManagement.AsyncOperations.IAsyncOperation<UnityEngine.SceneManagement.Scene> __Gen_Delegate_Imp15(object p0, UnityEngine.SceneManagement.LoadSceneMode p1)
        {
            using (var pObjs = new Objects(p0, p1))
            {
                UnityEngine.ResourceManagement.AsyncOperations.IAsyncOperation<UnityEngine.SceneManagement.Scene> result = default(UnityEngine.ResourceManagement.AsyncOperations.IAsyncOperation<UnityEngine.SceneManagement.Scene>);
                result = (UnityEngine.ResourceManagement.AsyncOperations.IAsyncOperation<UnityEngine.SceneManagement.Scene>)methodInfo.Invoke(null, pObjs.objs);
                return result;
            }
        }
        public UnityEngine.ResourceManagement.AsyncOperations.IAsyncOperation<UnityEngine.GameObject> __Gen_Delegate_Imp16(object p0, object p1, bool p2)
        {
            using (var pObjs = new Objects(p0, p1, p2))
            {
                UnityEngine.ResourceManagement.AsyncOperations.IAsyncOperation<UnityEngine.GameObject> result = default(UnityEngine.ResourceManagement.AsyncOperations.IAsyncOperation<UnityEngine.GameObject>);
                result = (UnityEngine.ResourceManagement.AsyncOperations.IAsyncOperation<UnityEngine.GameObject>)methodInfo.Invoke(null, pObjs.objs);
                return result;
            }
        }
        public UnityEngine.ResourceManagement.AsyncOperations.IAsyncOperation<UnityEngine.GameObject> __Gen_Delegate_Imp17(object p0, UnityEngine.Vector3 p1, UnityEngine.Quaternion p2, object p3)
        {
            using (var pObjs = new Objects(p0, p1, p2, p3))
            {
                UnityEngine.ResourceManagement.AsyncOperations.IAsyncOperation<UnityEngine.GameObject> result = default(UnityEngine.ResourceManagement.AsyncOperations.IAsyncOperation<UnityEngine.GameObject>);
                result = (UnityEngine.ResourceManagement.AsyncOperations.IAsyncOperation<UnityEngine.GameObject>)methodInfo.Invoke(null, pObjs.objs);
                return result;
            }
        }
        public void __Gen_Delegate_Imp18(object p0, float p1)
        {
            using (var pObjs = new Objects(p0, p1))
            {
                methodInfo.Invoke(null, pObjs.objs);
            }
        }
        public void __Gen_Delegate_Imp19(object p0, int p1)
        {
            using (var pObjs = new Objects(p0, p1))
            {
                methodInfo.Invoke(null, pObjs.objs);
            }
        }
        public string __Gen_Delegate_Imp20(object p0)
        {
            using (var pObjs = new Objects(p0))
            {
                string result = default(string);
                result = (string)methodInfo.Invoke(null, pObjs.objs);
                return result;
            }
        }
        public UniRx.Async.UniTask<GameMain.Net.IResponse> __Gen_Delegate_Imp21(object p0, object p1)
        {
            using (var pObjs = new Objects(p0, p1))
            {
                UniRx.Async.UniTask<GameMain.Net.IResponse> result = default(UniRx.Async.UniTask<GameMain.Net.IResponse>);
                result = (UniRx.Async.UniTask<GameMain.Net.IResponse>)methodInfo.Invoke(null, pObjs.objs);
                return result;
            }
        }
        public UniRx.Async.UniTask<GameMain.Net.IResponse> __Gen_Delegate_Imp22(object p0, object p1, System.Threading.CancellationToken p2)
        {
            using (var pObjs = new Objects(p0, p1, p2))
            {
                UniRx.Async.UniTask<GameMain.Net.IResponse> result = default(UniRx.Async.UniTask<GameMain.Net.IResponse>);
                result = (UniRx.Async.UniTask<GameMain.Net.IResponse>)methodInfo.Invoke(null, pObjs.objs);
                return result;
            }
        }
        public UniRx.Async.UniTask<GameMain.Net.IResponse> __Gen_Delegate_Imp23(object p0, int p1)
        {
            using (var pObjs = new Objects(p0, p1))
            {
                UniRx.Async.UniTask<GameMain.Net.IResponse> result = default(UniRx.Async.UniTask<GameMain.Net.IResponse>);
                result = (UniRx.Async.UniTask<GameMain.Net.IResponse>)methodInfo.Invoke(null, pObjs.objs);
                return result;
            }
        }
        public UniRx.Async.UniTask<GameMain.Net.IResponse> __Gen_Delegate_Imp24(object p0, int p1, System.Threading.CancellationToken p2)
        {
            using (var pObjs = new Objects(p0, p1, p2))
            {
                UniRx.Async.UniTask<GameMain.Net.IResponse> result = default(UniRx.Async.UniTask<GameMain.Net.IResponse>);
                result = (UniRx.Async.UniTask<GameMain.Net.IResponse>)methodInfo.Invoke(null, pObjs.objs);
                return result;
            }
        }

    }
}
#endif