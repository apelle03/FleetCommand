using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet_Command.Utils {
    class ByDrawOrder<T> : IComparer<T> where T : DGC {

        public int Compare(T t1, T t2) {
            return t1.DrawOrder - t2.DrawOrder;
        }
    }
}
