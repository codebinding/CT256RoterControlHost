﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoterControlSupport {

    public class SeriesParameter {

        // #0
        public byte Kv;   // 80kV, 100kV, 120kV, 140kV
        public ushort Ma;   // 0mA ~ 833mA
        public byte Fss;  // 0 = small, 1 = medium, 2 = large
        public byte ApertureMode;
        public byte FilterMode;
        public byte RowNumber;
        public int PhasePercentage;

        // #1
        public uint ShotTimeInMSec;  // 18-bit, 0 ~ 100,000mS
        public byte NumberOfShots;         // 8-bit, 1 ~ 64
        public int SeriesTimeInMSec;     // 18-bit, 0 ~ 100,000mS
        public int DelayBeforeNextSeries;   // 20-bit, 0 ~ 1,048,575mS

        // #2
        public int TriggerPosition;
        public int TriggerMode;
        public int ScanType;           // 0 = Normal, 1 = Dose Modulation
        public byte DitherType;
        public int IsRelative;
        public short TicksPerRotation;
        public byte CardiacScan;
        public int EmergencyScan;

        // #3
        public int ErrorRegisterReset;
        public byte EncoderSource;
        public uint IntegrationTime;     // 24-bit
        public byte StartingSlice;
        public byte EndingSlice;
        public byte DataSource;
        public byte InputSource;
        public byte SampleMode;
        public byte Decimation;
        public byte ClockSpeed;
        public byte Range;
        public byte PostConversionShutdown;

        // #4
        public byte IntegrationAveraging; // 8-bit
        public byte DetectorDataSource;
        public uint IntegrationLimit;    // 32-bit
        public ushort OffsetIntegrationLimit;   // 12-bit
        public ushort TimePerRotationInMSec;    // 12-bit

        // #5
        public ushort StartingMa;
        public ushort PeakMa;
        public ushort AverageMa;
        public ushort MinMa;

        // #6
        public ushort PhaseMinus;
        public ushort PhasePlus;
        public ushort TreQuater;
        public ushort Tup;
        public ushort Segments;
        public ushort TubeHomePosition;

        public SeriesParameter() {

            // #0
            Kv = 80;
            Ma = 0;
            Fss = 2;
            ApertureMode = 0;
            FilterMode = 0;
            RowNumber = 0;
            PhasePercentage = 0;

            // #1
            ShotTimeInMSec = 0;
            NumberOfShots = 0;
            SeriesTimeInMSec = 0;
            DelayBeforeNextSeries = 0;

            // #2
            TriggerPosition = 0;
            TriggerMode = 0;
            ScanType = 0;
            DitherType = 0;
            IsRelative = 0;
            TicksPerRotation = 0;
            CardiacScan = 0;

            // #3
            ErrorRegisterReset = 0;
            EncoderSource = 0;
            IntegrationTime = 0;
            StartingSlice = 0;
            EndingSlice = 0;
            DataSource = 0;
            InputSource = 0;
            SampleMode = 0;
            Decimation = 0;
            ClockSpeed = 0;
            Range = 0;
            PostConversionShutdown = 0;

            // #4
            IntegrationAveraging = 0;
            DetectorDataSource = 0;
            IntegrationLimit = 0;
            OffsetIntegrationLimit = 0;

            // #5
            StartingMa = 0;
            PeakMa = 0;
            AverageMa = 0;
            MinMa = 0;

            // #6
            PhaseMinus = 0;
            PhasePlus = 0;
            TreQuater = 0;
            Tup = 0;
            Segments = 0;
            TubeHomePosition = 0;
        }
    }
}
