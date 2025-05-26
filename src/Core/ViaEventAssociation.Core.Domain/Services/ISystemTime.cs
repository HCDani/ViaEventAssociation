using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViaEventAssociation.Core.Domain.Services {
    public interface ISystemTime {
        public DateTime GetCurrentDateTime();
    }
    public class SystemTimeHolder {
        public static ISystemTime SystemTime { get; private set; } = new SystemTime();

        public static void SetSystemTime(ISystemTime systemTime) {
            SystemTime = systemTime ?? throw new ArgumentNullException(nameof(systemTime), "System time cannot be null");
        }
    }
}
