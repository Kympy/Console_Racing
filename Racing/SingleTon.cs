using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing
{
    public class SingleTon<T> where T : class, new() // 제한 범위 : class 이면서  new()로 생성이 가능함.
    {
        private static T instance = null;
        private static object _lock = new object();

        public static T Instance
        {
            get
            {
                lock(_lock) // 멀티 safe
                {
                    if (instance == null) // 없으면
                    {
                        instance = new T(); // 생성
                    }
                    return instance;
                }
            }
        }
    }
}
