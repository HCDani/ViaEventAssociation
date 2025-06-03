using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViaEventAssociation.Core.Tools.SystemTime {
    public class SystemTime : ISystemTime {
        public DateTime GetCurrentDateTime() {
            return DateTime.Now;
        }
    }
}
