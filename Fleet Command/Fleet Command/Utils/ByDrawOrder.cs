using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet_Command.Utils {
    class ByDrawOrder : IComparer<DGC> {

        public int Compare(DGC dgc1, DGC dgc2) {
            return dgc1.DrawOrder - dgc2.DrawOrder;
        }
    }
}
