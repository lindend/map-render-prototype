using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Assets.Scripts.Util
{
    public static class DebugProfile
    {
        public static void Time(string name, Action action)
        {
            Time(name, () => { action(); return 0; });
        }

        public static T Time<T>(string name, Func<T> action)
        {
            var sw = new Stopwatch();
            sw.Start();
            var result = action();
            UnityEngine.Debug.Log(string.Format("PROF - {0}: {1}ms", name, sw.Elapsed.TotalMilliseconds));
            sw.Stop();
            return result;
        }

        public static T Time<T>(Expression<Func<T>> action)
        {
            return Time(action.Body.ToString(), action.Compile());
        }
    }
}
