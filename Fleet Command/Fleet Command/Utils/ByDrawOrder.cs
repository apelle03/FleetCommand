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
                return Math.Sign(t1.ID - t2.ID);
            }
        }
    }
}
