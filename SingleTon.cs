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
                lock(_lock)
                {
                    if (instance == null)
                    {
                        instance = new T(); // 제네릭으로 매니저 사용
                    }
                    return instance;
                }
            }
        }
    }
}
