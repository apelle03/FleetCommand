using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet_Command.Utils {
    class ByUpdateOrder<T> : IComparer<T> where T : DGC {
        public int Compare(T t1, T t2) {
            return t1.UpdateOrder - t2.UpdateOrder;
        }
    }
}
