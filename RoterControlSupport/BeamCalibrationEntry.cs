using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoterControlSupport {
    public class BeamCalibrationEntry {

        public int Kv { get; set; }
        public int Ma { get; set; }
        public string Fss { get; set; }
        public int Position { get; set; }
        public string Direction { get; set; }
        public int Offset { get; set; }

        public BeamCalibrationEntry() {

            Kv = 0;
            Ma = 0;
            Fss = "";
            Position = 100; // 100 = Global Offset
            Direction = "";
            Offset = 0;
        }
    }
}
