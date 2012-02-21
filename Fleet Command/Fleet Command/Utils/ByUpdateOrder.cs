using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet_Command.Utils {
    class ByUpdateOrder : IComparer<DGC> {
        public int Compare(DGC dgc1, DGC dgc2) {
            return dgc1.UpdateOrder - dgc2.UpdateOrder;
        }
    }
}
