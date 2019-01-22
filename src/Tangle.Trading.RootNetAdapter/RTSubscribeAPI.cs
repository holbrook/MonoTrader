using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Tangle.Trading.RootNetAdapter
{
    sealed class RTSubscAPI : IDisposable
    {
        #region Unmanaged declaration

        // 表示非托管的事件处理器的委托
        delegate void RTSubscAPIEventHandler(IntPtr param);

        // 引入 DLL 中的函数，注意指定字符集和调用约定
        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPICreate")]
        static extern uint Create();

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPIDispose")]
        static extern int Dispose(uint handle);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPISetOptInfo")]
        static extern int SetOptInfo(uint handle, string optId, string optPwd, string optInfo);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPIAddSubscribeAcct")]
        static extern int AddSubscribeAcct(uint handle, string acctId, string tradePwd,
            string subType, string clientId);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPISubscribe")]
        static extern int Subscribe(uint handle);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPISubscribe")]
        static extern int Unsubscribe(uint handle);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPIGetFirstMessage")]
        static extern int GetFirstMessage(uint handle);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPIGetRecord")]
        static extern int GetRecord(uint handle);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPIGetValue")]
        static extern int GetValue(uint handle, short sRecNo, string strFieldName, StringBuilder outBuffer);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPIGetFunctionCode")]
        static extern int GetFunctionCode(uint handle, StringBuilder outBuffer);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPIRebuild")]
        static extern int Rebuild(uint handle);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPIGetRebuildFirstMessage")]
        static extern int GetRebuildFirstMessage(uint handle);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPIGetRebuildRecord")]
        static extern int GetRebuildRecord(uint handle);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPIGetRebuildValue")]
        static extern int GetRebuildValue(uint handle, short sRecNo, string strFieldName, StringBuilder outBuffer);

        // 注意指定封送类型
        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPISetMessageHandler")]
        static extern void SetMessageHandler(uint handle,
            [MarshalAs(UnmanagedType.FunctionPtr)]RTSubscAPIEventHandler handler, IntPtr param);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPISetMQExceptionHandler")]
        static extern void SetMQExceptionHandler(uint handle,
            [MarshalAs(UnmanagedType.FunctionPtr)]RTSubscAPIEventHandler handler, IntPtr param);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPISetHeartbeatTimeoutHandler")]
        static extern void SetHeartbeatTimeoutHandler(uint handle,
            [MarshalAs(UnmanagedType.FunctionPtr)]RTSubscAPIEventHandler handler, IntPtr param);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPISetRebuildMessageHandler")]
        static extern void SetRebuildMessageHandler(uint handle,
            [MarshalAs(UnmanagedType.FunctionPtr)]RTSubscAPIEventHandler handler, IntPtr param);

        [DllImport("RTSubscApi.dll", CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "RTSubscAPIInitialize")]
        static extern bool Initialize();

        #endregion

        uint handle; // 保存非托管 API 对象的句柄

        #region Throw helpers

        void ThrowIfHandleIsZero(string message) // 检查句柄有效性
        {
            if (handle == 0u)
                throw new InvalidOperationException(message);
        }

        void ThrowIfHandleIsZero()
        {
            ThrowIfHandleIsZero("Object disposed.");
        }

        #endregion

        #region Events

        // 托管代码中的事件处理委托
        public delegate void Handler(RTSubscAPI sender);

        // 事件
        public event Handler NewMessage;
        public event Handler MQException;
        public event Handler HeartbeatTimeout;
        public event Handler RebuildMessage;

        #endregion

        #region Delegate interop with unmanaged code

        // 这个方法将生成一个委托，用于把非托管代码
        // 中的事件处理转发到托管代码
        void OnNewMessage(IntPtr param)
        {
            if (NewMessage != null)
                NewMessage.Invoke(this);
        }

        void OnMQException(IntPtr param)
        {
            if (MQException != null)
                MQException.Invoke(this);
        }

        void OnHeartbeatTimeout(IntPtr param)
        {
            if (HeartbeatTimeout != null)
                HeartbeatTimeout.Invoke(this);
        }

        void OnRebuildMessage(IntPtr param)
        {
            if (RebuildMessage != null)
                RebuildMessage.Invoke(this);
        }

        // 保存这些委托的 GC 句柄，防止它们被垃圾收集
        GCHandle OnNewMessageHandle, OnMQExceptionHandle, OnHeartbeatTimeoutHandle, OnRebuildMessageHandle;

        #endregion

        #region Interface implementation

        public void Dispose()
        {
            if (handle != 0u)
            {
                Dispose(handle); // 清除非托管资源
                handle = 0u;
                OnNewMessageHandle.Free(); // 释放事件处理委托的 GC 句柄
                OnMQExceptionHandle.Free();
                OnHeartbeatTimeoutHandle.Free();
                OnRebuildMessageHandle.Free();
            }
        }

        #endregion

        public RTSubscAPI() // 构造函数
        {
            // 创建 API 对象并检查和保存句柄
            handle = Create();
            ThrowIfHandleIsZero("Failed to create unmanaged API object.");
            // 创建事件处理转发方法的委托
            RTSubscAPIEventHandler OnNewMessageDelegate = this.OnNewMessage;
            RTSubscAPIEventHandler OnMQExceptionDelegate = this.OnMQException;
            RTSubscAPIEventHandler OnHeartbeatTimeoutDelegate = this.OnHeartbeatTimeout;
            RTSubscAPIEventHandler OnRebuildMessageDelegate = this.OnRebuildMessage;
            // 将它们设置为 API 对象的事件处理器，第三个参数是不必要的
            // 因为每个不同的 RTSubscAPI 对象的这些委托是不同的
            SetMessageHandler(handle, OnNewMessageDelegate, IntPtr.Zero);
            SetMQExceptionHandler(handle, OnMQExceptionDelegate, IntPtr.Zero);
            SetHeartbeatTimeoutHandler(handle, OnHeartbeatTimeoutDelegate, IntPtr.Zero);
            SetRebuildMessageHandler(handle, OnRebuildMessageDelegate, IntPtr.Zero);
            // 创建这些委托的 GC 句柄并保存起来，防止它们被垃圾回收
            OnNewMessageHandle = GCHandle.Alloc(OnNewMessageDelegate);
            OnMQExceptionHandle = GCHandle.Alloc(OnMQExceptionDelegate);
            OnHeartbeatTimeoutHandle = GCHandle.Alloc(OnHeartbeatTimeoutDelegate);
            OnRebuildMessageHandle = GCHandle.Alloc(OnRebuildMessageDelegate);
        }

        // 静态构造器，用于初始化 DLL 并检查其成功与否
        static RTSubscAPI()
        {
            if (!Initialize())
                throw new TypeInitializationException(typeof(RTSubscAPI).FullName, null);
        }

        // 析构器，调用 Dispose 方法释放资源
        ~RTSubscAPI()
        {
            Dispose();
        }

        // 设置柜员信息，只是简单地检查句柄有效性、把参数封送并转发到非托管代码中
        public void SetOptInfo(string optId, string optPwd, string optInfo)
        {
            ThrowIfHandleIsZero();
            SetOptInfo(handle, optId, optPwd, optInfo);
        }

        public void AddSubscribeAcct(string acctId, string tradePwd, string subType, string clientId)
        {
            ThrowIfHandleIsZero();
            AddSubscribeAcct(handle, acctId, tradePwd, subType, clientId);
        }

        public bool Subscribe()
        {
            ThrowIfHandleIsZero();
            return Subscribe(handle) == 0;
        }

        public bool Unsubscribe()
        {
            ThrowIfHandleIsZero();
            return Unsubscribe(handle) == 0;
        }

        public bool GetFirstMessage()
        {
            ThrowIfHandleIsZero();
            return GetFirstMessage(handle) == 0;
        }

        public int GetRecord()
        {
            ThrowIfHandleIsZero();
            return GetRecord(handle);
        }

        // 读取“当前消息”的指定域的指定字段
        // 通过封送和两次调用非托管代码中的 RTSubscAPIGetValue 让方法使用起来更加方便
        public string GetValue(short sRecNo, string strFieldName)
        {
            ThrowIfHandleIsZero();
            StringBuilder sb = new StringBuilder(GetValue(handle, sRecNo, strFieldName, null) + 10);
            GetValue(handle, sRecNo, strFieldName, sb);
            return sb.ToString();
        }

        public string GetFunctionCode()
        {
            ThrowIfHandleIsZero();
            StringBuilder sb = new StringBuilder(10);
            GetFunctionCode(handle, sb);
            return sb.ToString();
        }

        public bool Rebuild()
        {
            ThrowIfHandleIsZero();
            return Rebuild(handle) == 0;
        }

        public bool GetRebuildFirstMessage()
        {
            ThrowIfHandleIsZero();
            return GetRebuildFirstMessage(handle) == 0;
        }

        public int GetRebuildRecord()
        {
            ThrowIfHandleIsZero();
            return GetRebuildRecord(handle);
        }

        public string GetRebuildValue(short sRecNo, string strFieldName)
        {
            ThrowIfHandleIsZero();
            StringBuilder sb = new StringBuilder(GetRebuildValue(handle, sRecNo, strFieldName, null) + 10);
            GetRebuildValue(handle, sRecNo, strFieldName, sb);
            return sb.ToString();
        }

    }
}
