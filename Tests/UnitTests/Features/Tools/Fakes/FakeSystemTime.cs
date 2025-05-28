using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Services;

namespace UnitTests.Features.Tools.Fakes {
    public class FakeSystemTime : ISystemTime {
        public DateTime DateTime { get; set; } = DateTime.Now;
        public DateTime GetCurrentDateTime() {
            return DateTime;
        }
    }
}
