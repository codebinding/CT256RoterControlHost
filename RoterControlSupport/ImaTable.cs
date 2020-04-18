using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace RoterControlSupport {
    public class ImaTable {

        public int StartingMa {
            get { return _StartingMa; }
        }

        public int PeakMa {
            get { return _PeakMa; }
        }

        public int AverageMa {
            get { return _AverageMa; }
        }

        public int MinMa {
            get { return _MinMa; }
        }

        public int PhaseMinus {
            get { return _PhaseMinus; }
        }

        public int PhasePlus {
            get { return _PhasePlus; }
        }

        public int TrevQuarter {
            get { return _TrevQuarter; }
        }

        public int Tup {
            get { return _Tup; }
        }

        public bool UseECGIma {
            get { return _UseECGIma; }
        }

        public int NTheta {
            get { return _NTheta; }
        }

        public int GantryHomeAngleOffset {
            get { return _GantryHomeAngelOffset; }
        }

        private int _StartingMa;
        private int _PeakMa;
        private int _AverageMa;
        private int _MinMa;
        private int _PhaseMinus;
        private int _PhasePlus;
        private int _TrevQuarter;
        private int _Tup;
        private bool _UseECGIma;
        private int _NTheta = 64;     // number of ma values per line in ImaMapFile.
        private int _GantryHomeAngelOffset;

        public List<int> MaValues = new List<int>();

        public void ParseMapFile(string imaMapFilePath) {

            MaValues.Clear();

            // old version: start_pos = 300mm; step = -9.28mm; total_steps = 34; mA = 200;
            // new version: starting_ma=30;peak_ma=234

            using (StreamReader myFile = new StreamReader(imaMapFilePath)) {

                // Parse first line
                string myLine = myFile.ReadLine();

                string myStartingMa = myLine.Replace(" ", string.Empty).Split(';').Select(pair => pair.Split('=')).FirstOrDefault(left => left.First().ToLower() == "starting_ma").Last();
                string myPeakMa = myLine.Replace(" ", string.Empty).Split(';').Select(pair => pair.Split('=')).FirstOrDefault(left => left.First().ToLower() == "peak_ma").Last();
                string myAverageMa = myLine.Replace(" ", string.Empty).Split(';').Select(pair => pair.Split('=')).FirstOrDefault(left => left.First().ToLower() == "average_ma").Last();
                string myMinMa = myLine.Replace(" ", string.Empty).Split(';').Select(pair => pair.Split('=')).FirstOrDefault(left => left.First().ToLower() == "min_ma").Last();
                string myPhaseMinus = myLine.Replace(" ", string.Empty).Split(';').Select(pair => pair.Split('=')).FirstOrDefault(left => left.First().ToLower() == "phase_minus").Last();
                string myPhasePlus = myLine.Replace(" ", string.Empty).Split(';').Select(pair => pair.Split('=')).FirstOrDefault(left => left.First().ToLower() == "phase_plus").Last();
                string myTrevQuarter = myLine.Replace(" ", string.Empty).Split(';').Select(pair => pair.Split('=')).FirstOrDefault(left => left.First().ToLower() == "trev_quarter").Last();
                string myTup = myLine.Replace(" ", string.Empty).Split(';').Select(pair => pair.Split('=')).FirstOrDefault(left => left.First().ToLower() == "t_up").Last();
                string myUseECGIma = myLine.Replace(" ", string.Empty).Split(';').Select(pair => pair.Split('=')).FirstOrDefault(left => left.First().ToLower() == "use_ecg_ima").Last();
                string myNTheta = myLine.Replace(" ", string.Empty).Split(';').Select(pair => pair.Split('=')).FirstOrDefault(left => left.First().ToLower() == "n_theta").Last();
                string myGantryHomeAngelOffset = myLine.Replace(" ", string.Empty).Split(';').Select(pair => pair.Split('=')).FirstOrDefault(left => left.First().ToLower() == "gantry_home_angle_offset").Last();

                _StartingMa = Convert.ToInt32(myStartingMa);
                _PeakMa = Convert.ToInt32(myPeakMa);
                _AverageMa = Convert.ToInt32(myAverageMa);
                _MinMa = Convert.ToInt32(myMinMa);
                _PhaseMinus = Convert.ToInt32(myPhaseMinus);
                _PhasePlus = Convert.ToInt32(myPhasePlus);
                _TrevQuarter = Convert.ToInt32(myTrevQuarter);
                _Tup = Convert.ToInt32(myTup);
                _UseECGIma = myUseECGIma.ToLower() == "yes";
                _NTheta = Convert.ToInt32(myNTheta);
                _GantryHomeAngelOffset = Convert.ToInt32(myGantryHomeAngelOffset);

                // Parse rest of the lines
                while ((myLine = myFile.ReadLine()) != null) {

                    List<int> myPoints = myLine.Replace(" ", string.Empty).Split(',').Select(str => Convert.ToInt32(str)).ToList<Int32>();

                    if (myPoints.Count != _NTheta) {

                        throw new Exception($"Every line must have {_NTheta} values;");
                    }

                    foreach (int myPoint in myPoints) {

                        MaValues.Add(myPoint);
                    }
                }
            }
        }
    }
}
