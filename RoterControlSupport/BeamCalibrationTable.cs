using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RoterControlSupport {
    public class BeamCalibrationTable :INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int globalXOffset;
        private int globalZOffset;

        public int GlobalXOffset {
            get { return globalXOffset; }
            set { globalXOffset = value; OnPropertyChanged("GlobalXOffset"); }
        }
        public int GlobalZOffset {
            get { return globalZOffset; }
            set { globalZOffset = value; OnPropertyChanged("GlobalZOffset"); }
        }
        public List<BeamCalibrationEntry> CustomerTable;

        public BeamCalibrationTable() {

            GlobalXOffset = 0;
            GlobalZOffset = 0;
            CustomerTable = new List<BeamCalibrationEntry>();
        }

        public void Reset() {

            GlobalXOffset = 0;
            GlobalZOffset = 0;
            CustomerTable.Clear();
        }

        public void AddGlobalEntry(int p_x_offset, int p_z_offset) {

            GlobalXOffset = p_x_offset;
            GlobalZOffset = p_z_offset;
        }

        public void AddCustomerEntry(BeamCalibrationEntry p_entry) {

            if (p_entry.Fss != "S" && p_entry.Fss != "M" && p_entry.Fss != "L") {

                throw new Exception("focal spot size has incorrect value");
            }

            if (p_entry.Direction != "X" && p_entry.Direction != "Z") {

                throw new Exception("direction has incorrect value");
            }

            CustomerTable.Add(p_entry);
        }

        public void AddCustomerEntry(int p_kv, int p_ma, string p_fss, int p_position, string p_direction, int p_offset) {

            if (p_fss != "S" && p_fss != "M" && p_fss != "L" && p_fss != "G") {

                throw new Exception("focal spot size has incorrect value");
            }

            if (p_direction!= "X" && p_direction != "Z") {

                throw new Exception("direction has incorrect value");
            }

            BeamCalibrationEntry entry = new BeamCalibrationEntry();
            entry.Kv = p_kv;
            entry.Ma = p_ma;
            entry.Fss = p_fss;
            entry.Position = p_position;
            entry.Direction = p_direction;
            entry.Offset = p_offset;

            CustomerTable.Add(entry);
        }
    }
}
