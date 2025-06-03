using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.SystemTime;

namespace UnitTests.Features.Tools.Fakes {
    public class FakeSystemTime(DateTime currentTime) : ISystemTime {
        public DateTime DateTime { get; set; } = currentTime;
        public DateTime GetCurrentDateTime() {
            return DateTime;
        }
    }
}
