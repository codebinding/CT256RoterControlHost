using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoterControlSupport {
    public class BeamCalibrationEntry {

        public int Kv;
        public int Ma;
        public string Fss;
        public int Position;
        public string Direction;
        public int Offset;

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
