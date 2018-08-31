using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HassBotDTOs {

    public enum HassioRelease {
        Beta,
        Stable
    }

    public class HassIOVersion {
        public string HomeAssistant { get; set; }
        public string Supervisor { get; set; }
        public string HassOS { get; set; }
    }

    public class HomeAssistantVersion {
        public string Stable { get; set; }
        public string Beta { get; set; }
    }
}
