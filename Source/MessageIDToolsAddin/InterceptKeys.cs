using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MessageIDToolsAddin
{
    public class InterceptKeys : IDisposable
    {
        public delegate int LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private readonly LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        private const int WH_KEYBOARD = 2;
        private const int HC_ACTION = 0;

        public InterceptKeys()
        {
            _proc = HookCallback;
        }

        public void SetHook()
        {
            _hookID = SetWindowsHookEx(WH_KEYBOARD, _proc, IntPtr.Zero, (uint) AppDomain.GetCurrentThreadId());
        }

        public void ReleaseHook()
        {
            lock (this)
            {
                if (_hookID == IntPtr.Zero)
                {
                    return;
                }
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
            }
        }

        public event EventHandler<InterceptKeysEventArgs> KeyIntercepted;

        void OnKeyIntercepted(Keys keyData, bool isShiftKeyDown, bool isCtrlKeyDown)
        {
            if (KeyIntercepted == null)
            {
                return;
            }
            KeyIntercepted(this, new InterceptKeysEventArgs(keyData, isShiftKeyDown, isCtrlKeyDown));
        }


        private bool _isControlKeyDown;
        private bool _isShiftKeyDown;
        private int HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
            }
            else
            {
                if (nCode == HC_ACTION)
                {
                    Keys keyData = (Keys)wParam;
                    //if (keyData == Keys.F4 && (lParam.ToInt32() & 0x80000000) == 0 && _isControlKeyDown && _isShiftKeyDown)
                    //{
                    //    //Method you want to  call...
                    //    MessageBox.Show("Test");
                    //}
                    if (keyData == Keys.ShiftKey && (lParam.ToInt32() & 0x80000000) == 0)
                    {
                        _isShiftKeyDown = true;
                    }
                    if (keyData == Keys.ControlKey && (lParam.ToInt32() & 0x80000000) == 0)
                    {
                        _isControlKeyDown = true;
                    }
                    if (keyData == Keys.ShiftKey && (lParam.ToInt32() & 0x80000000) != 0)
                    {
                        _isShiftKeyDown = false;
                    }
                    if (keyData == Keys.ControlKey && (lParam.ToInt32() & 0x80000000) != 0)
                    {
                        _isControlKeyDown = false;
                    }
    
                    OnKeyIntercepted(keyData, _isShiftKeyDown, _isControlKeyDown);
                }
                return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        public void Dispose()
        {
            ReleaseHook();
        }
    }

    public class InterceptKeysEventArgs : EventArgs
    {
        public InterceptKeysEventArgs(Keys keyData, bool isShiftKeyDown, bool isCtrlKeyDown)
        {
            KeyData = keyData;
            IsShiftKeyDown = isShiftKeyDown;
            IsCtrlKeyDown = isCtrlKeyDown;
        }
        public Keys KeyData { get; private set; }
        public bool IsShiftKeyDown { get; private set; }
        public bool IsCtrlKeyDown { get; private set; }
    }
}