using ViaEventAssociation.Core.Tools.SystemTime;
using System.Threading;

namespace UnitTests.Features.Tools.Fakes {
    public class FakeSystemTime : ISystemTime {

        private static FakeSystemTime singleton = new FakeSystemTime() { DateTime = DateTime.Now };
        public static Mutex FakeSystemTimeMutex { get; } = new();

        public DateTime DateTime { get; private set;  }

        public DateTime GetCurrentDateTime() {
            return DateTime;
        }
        
        public static void SetSystemTime(DateTime currentTime) {
            singleton.DateTime = currentTime;
            SystemTimeHolder.SetSystemTime(singleton);
        }

    }
}
