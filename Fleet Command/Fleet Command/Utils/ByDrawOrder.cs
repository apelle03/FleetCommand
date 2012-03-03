using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet_Command.Utils {
    class ByDrawOrder<T> : IComparer<T> where T : DGC {

        public int Compare(T t1, T t2) {
            int orderDiff = t1.DrawOrder - t2.DrawOrder;
            if (orderDiff != 0) {
                return orderDiff;
            } else {
                if (t1.Equals(t2)) {
                    return 0;
                } else {
                    return 1;
                }
            }
        }
    }
}
