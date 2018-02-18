using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HassBotDTOs {

    // Away From Keyboard
    public class AFKDTO {
        public ulong Id { get; set; }
        public string AwayUser { get; set; }
        public string AwayMessage { get; set; }
        public DateTime AwayTime { get; set; }
    }

    // Comparer class for AFKDTO
    public class AFKDTOComparer : IEqualityComparer<AFKDTO> {
        public bool Equals(AFKDTO x, AFKDTO y) {
            return (x.AwayUser == y.AwayUser) ? true : false;
        }

        public int GetHashCode(AFKDTO obj) {
            return base.GetHashCode();
        }
    }
}