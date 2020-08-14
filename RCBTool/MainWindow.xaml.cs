/*******************************************************************************
* Filename: C:\Development\RCBTool\RCBTool/MainWindow.xaml.cs
* Original Author: BobbyZhu 
* File Creation Date: May 15, 2018
* Subsystem: RCBTool
*
* Description: {fileDescription}  
*
* Copyright Information (c) FMI Medical Systems, Inc.  2018
* Source Control Information: $Id$ 
*******************************************************************************/

/* Microsoft WPF project default references */

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media;
using System.Threading;

using CoreWinSubFramework;
using RoterControlSupport;

namespace RCBTool {

    public class ExposureControl {

        public Label lblKv;
        public TextBox tbxKv;
        public Label lblMa;
        public TextBox tbxMa;
        public Label lblFss;
        public ComboBox cbxFss;
        public Label lblExposureTime;
        public TextBox tbxExposureTime;
        public Label lblScanSteps;
        public TextBox tbxScanSteps;
        public Label lblScanTime;
        public TextBox tbxScanTime;
        public Label lblTriggerMode;
        public ComboBox cbxTriggerMode;
        public Label lblTriggerPosition;
        public TextBox tbxTriggerPosition;
        public Label lblPositionType;
        public ComboBox cbxPositionType;
        public Label lblTicksPerRotation;
        public ComboBox cbxTicksPerRotation;
        public Label lblEncoderSource;
        public ComboBox cbxEncoderSource;
        public Label lblIntegrationTime;
        public TextBox tbxIntegrationTime;
        public Label lblDitherType;
        public ComboBox cbxDitherType;
        public Label lblTimePerRotationInMSec;
        public TextBox tbxTimePerRotationInMSec;
        public Label lblTimeoutBetweenArmXrayOn;
        public TextBox tbxTimeoutBetweenArmXrayOn;
        public Label lblCineScan;
        public ComboBox cbxCineScan;
        public Label lblDelayBetweenShots;
        public TextBox tbxDelayBetweenShots;
        public Label lblDelayBeforeNextSeries;
        public TextBox tbxDelayBeforeNextSeries;
        public CheckBox ckbIma;
        public CheckBox ckbEmergencyScan;

        public Label lblCardiacScan;
        public ComboBox cbxCardiacScan;
        public Label lblPhasePercentage;
        public TextBox tbxPhasePercentage;
        public Label lblApertureMode;
        public TextBox tbxApertureMode;
        public Label lblFilterMode;
        public TextBox tbxFilterMode;
        public Label lblRowNumber;
        public TextBox tbxRowNumber;

        public Label lblErrorRegisterReset;
        public ComboBox cbxErrorRegisterReset;
        public Label lblStartingSlice;
        public TextBox tbxStartingSlice;
        public Label lblEndingSlice;
        public TextBox tbxEndingSlice;
        public Label lblDataSource;
        public ComboBox cbxDataSource;
        public Label lblInputSource;
        public TextBox tbxInputSource;

        public Label lblSampleMode;
        public TextBox tbxSampleMode;
        public Label lblDecimation;
        public TextBox tbxDecimation;
        public Label lblClockSpeed;
        public TextBox tbxClockSpeed;
        public Label lblRange;
        public TextBox tbxRange;
        public Label lblPostConversionShutdown;
        public ComboBox cbxPostConversionShutdown;

        public Label lblIntegrationAveraging;
        public TextBox tbxIntegrationAveraging;
        public Label lblDetectorSource;
        public ComboBox cbxDetectorDataSource;
        public Label lblIntegrationLimit;
        public TextBox tbxIntegrationLimit;
        public Label lblOffsetIntegrationLimit;
        public TextBox tbxOffsetIntegrationLimit;

        public Button btnLoadImaTable;

        public ImaTable imaTable;
        public bool imaLoaded;

        public ExposureControl() {

            int left_most = 50;
            int top_most = 20;

            // Line 1
            lblKv = new Label() { Content = "kV", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most, top_most, 0, 0) };
            lblMa = new Label() { Content = "mA", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 100, top_most, 0, 0) };
            lblFss = new Label() { Content = "FSS", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 200, top_most, 0, 0) };
            lblExposureTime = new Label() { Content = "Shot Time(mS)", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 300, top_most, 0, 0) };
            lblScanSteps = new Label() { Content = "Number of Shots", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 450, top_most, 0, 0) };
            lblScanTime = new Label() { Content = "Whole series Time(mS)", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 600, top_most, 0, 0) };

            // Line 2
            tbxKv = new TextBox() { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Height = 20, Width = 80, Margin = new Thickness(left_most, top_most + 20, 0, 0), Text = "70", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };
            tbxMa = new TextBox() { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Height = 20, Width = 80, Margin = new Thickness(left_most + 100, top_most + 20, 0, 0), Text = "100", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };

            cbxFss = new ComboBox() { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 200, top_most + 20, 0, 0), Width = 80, Height = 25, SelectedIndex = 2, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            cbxFss.Items.Add("Small");
            cbxFss.Items.Add("Medium");
            cbxFss.Items.Add("Large");

            tbxExposureTime = new TextBox() { Text = "1000", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 300, top_most + 20, 0, 0), Height = 23, Width = 120, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };
            tbxScanSteps = new TextBox() { Text = "1", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 450, top_most + 20, 0, 0), Height = 23, Width = 120, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };
            tbxScanTime = new TextBox() { Text = "1100", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 600, top_most + 20, 0, 0), Height = 23, Width = 120, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };

            // Line 3
            lblTriggerMode = new Label() { Content = "Trigger Mode", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most, top_most + 60, 0, 0) };
            lblTriggerPosition = new Label() { Content = "Trigger Position(ticks)", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 150, top_most + 60, 0, 0) };
            lblPositionType = new Label() { Content = "Position Type", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 300, top_most + 60, 0, 0) };
            lblTicksPerRotation = new Label() { Content = "Ticks Per Rotation", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 450, top_most + 60, 0, 0) };
            lblEncoderSource = new Label() { Content = "Encoder Source", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 600, top_most + 60, 0, 0) };

            // Line 4
            cbxTriggerMode = new ComboBox() { Margin = new Thickness(left_most, top_most + 80, 0, 0), Width = 120, Height = 25, SelectedIndex = 0, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            cbxTriggerMode.Items.Add("Immediate");
            cbxTriggerMode.Items.Add("Traverse");
            cbxTriggerMode.Items.Add("Theta");
            cbxTriggerMode.Items.Add("No X-ray");

            tbxTriggerPosition = new TextBox() { Text = "0", Margin = new Thickness(left_most + 150, top_most + 80, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };

            cbxPositionType = new ComboBox() { Margin = new Thickness(left_most + 300, top_most + 80, 0, 0), Width = 120, Height = 25, SelectedIndex = 0, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            cbxPositionType.Items.Add("Absolute");
            cbxPositionType.Items.Add("Relative");

            cbxTicksPerRotation = new ComboBox() { Margin = new Thickness(left_most + 450, top_most + 80, 0, 0), Width = 120, Height = 25, SelectedIndex = 0, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            cbxTicksPerRotation.Items.Add("2048");
            cbxTicksPerRotation.Items.Add("4096");
            cbxTicksPerRotation.Items.Add("6144");
            cbxTicksPerRotation.Items.Add("8196");

            cbxEncoderSource = new ComboBox() { Margin = new Thickness(left_most + 600, top_most + 80, 0, 0), Width = 120, Height = 25, SelectedIndex = 0, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            cbxEncoderSource.Items.Add("Internal");
            cbxEncoderSource.Items.Add("External");

            // Line 5
            lblIntegrationTime = new Label() { Content = "Integration Time", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most, top_most + 120, 0, 0) };
            lblDitherType = new Label() { Content = "Dither Type", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 150, top_most + 120, 0, 0) };
            lblTimePerRotationInMSec = new Label() { Content = "Time/Rotation(mS)", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 300, top_most + 120, 0, 0) };
            lblTimeoutBetweenArmXrayOn = new Label() { Content = "Arm Timeout(mS)", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 450, top_most + 120, 0, 0) };
            lblCineScan = new Label() { Content = "Cine Scan", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 600, top_most + 120, 0, 0) };

            // Line 6
            tbxIntegrationTime = new TextBox() { Text = "488281", Margin = new Thickness(left_most, top_most + 140, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };

            cbxDitherType = new ComboBox() { Margin = new Thickness(left_most + 150, top_most + 140, 0, 0), Width = 120, Height = 25, SelectedIndex = 0, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            cbxDitherType.Items.Add("No Dithering");
            cbxDitherType.Items.Add("x-Def");
            cbxDitherType.Items.Add("z-Def");

            tbxTimePerRotationInMSec = new TextBox() { Text = "500", Margin = new Thickness(left_most + 300, top_most + 140, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };
            tbxTimeoutBetweenArmXrayOn = new TextBox() { Text = "120000", Margin = new Thickness(left_most + 450, top_most + 140, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };

            cbxCineScan = new ComboBox() { Margin = new Thickness(left_most + 600, top_most + 140, 0, 0), Width = 120, Height = 25, SelectedIndex = 0, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            cbxCineScan.Items.Add("No");
            cbxCineScan.Items.Add("Yes");

            // Line 7
            lblDelayBetweenShots = new Label() { Content = "Delay Between Shots", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most, top_most + 180, 0, 0) };
            lblDelayBeforeNextSeries = new Label() { Content = "Delay Before Next Series", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 150, top_most + 180, 0, 0) };

            // Line 8
            tbxDelayBetweenShots = new TextBox() { Text = "0", Margin = new Thickness(left_most, top_most + 200, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };
            tbxDelayBeforeNextSeries = new TextBox() { Text = "0", Margin = new Thickness(left_most + 150, top_most + 200, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };

            ckbIma = new CheckBox() { Content = "imA", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 600, top_most + 200, 0, 0) };
            ckbEmergencyScan = new CheckBox() { Content = "Emergency", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(left_most + 650, top_most + 200, 0, 0) };

            // Line 9
            lblCardiacScan = new Label() { Content = "Cardiac Scan", Margin = new Thickness(left_most, top_most + 240, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblPhasePercentage = new Label() { Content = "Phase Percentage", Margin = new Thickness(left_most + 150, top_most + 240, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblApertureMode = new Label() { Content = "Aperture Mode", Margin = new Thickness(left_most + 300, top_most + 240, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblFilterMode = new Label() { Content = "Filter Mode", Margin = new Thickness(left_most + 450, top_most + 240, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblRowNumber = new Label() { Content = "Row Number", Margin = new Thickness(left_most + 600, top_most + 240, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

            // Line 10
            cbxCardiacScan = new ComboBox() { Margin = new Thickness(left_most, top_most + 260, 0, 0), Width = 120, Height = 25, SelectedIndex = 0, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            cbxCardiacScan.Items.Add("No");
            cbxCardiacScan.Items.Add("Yes");

            tbxPhasePercentage = new TextBox() { Text = "0", Margin = new Thickness(left_most + 150, top_most + 260, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };
            tbxApertureMode = new TextBox() { Text = "0", Margin = new Thickness(left_most + 300, top_most + 260, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };
            tbxFilterMode = new TextBox() { Text = "0", Margin = new Thickness(left_most + 450, top_most + 260, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };
            tbxRowNumber = new TextBox() { Text = "0", Margin = new Thickness(left_most + 600, top_most + 260, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };

            // Line 11
            lblErrorRegisterReset = new Label() { Content = "Error Register Reset", Margin = new Thickness(left_most, top_most + 300, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblStartingSlice = new Label() { Content = "Starting Slice", Margin = new Thickness(left_most + 150, top_most + 300, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblEndingSlice = new Label() { Content = "End Slice", Margin = new Thickness(left_most + 300, top_most + 300, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblDataSource = new Label() { Content = "Data Source", Margin = new Thickness(left_most + 450, top_most + 300, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblInputSource = new Label() { Content = "Input Source", Margin = new Thickness(left_most + 600, top_most + 300, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

            // Line 12
            cbxErrorRegisterReset = new ComboBox() { Margin = new Thickness(left_most, top_most + 320, 0, 0), Width = 120, Height = 25, SelectedIndex = 0, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            cbxErrorRegisterReset.Items.Add("Reset");
            cbxErrorRegisterReset.Items.Add("No Reset");

            tbxStartingSlice = new TextBox() { Text = "0", Margin = new Thickness(left_most + 150, top_most + 320, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };
            tbxEndingSlice = new TextBox() { Text = "0", Margin = new Thickness(left_most + 300, top_most + 320, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };

            cbxDataSource = new ComboBox() { Margin = new Thickness(left_most + 450, top_most + 320, 0, 0), Width = 120, Height = 25, SelectedIndex = 0, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            cbxDataSource.Items.Add("Slice Data");
            cbxDataSource.Items.Add("Aggregator Data");

            tbxInputSource = new TextBox() { Text = "0", Margin = new Thickness(left_most + 600, top_most + 320, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };

            // Line 13
            lblSampleMode = new Label() { Content = "Sample Mode", Margin = new Thickness(left_most, top_most + 360, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblDecimation = new Label() { Content = "Decimation", Margin = new Thickness(left_most + 150, top_most + 360, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblClockSpeed = new Label() { Content = "Clock Speed", Margin = new Thickness(left_most + 300, top_most + 360, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblRange = new Label() { Content = "Range", Margin = new Thickness(left_most + 450, top_most + 360, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblPostConversionShutdown = new Label() { Content = "Post Conversion Shutdown", Margin = new Thickness(left_most + 600, top_most + 360, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

            // Line 14
            tbxSampleMode = new TextBox() { Text = "0", Margin = new Thickness(left_most, top_most + 380, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };
            tbxDecimation = new TextBox() { Text = "7", Margin = new Thickness(left_most + 150, top_most + 380, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };
            tbxClockSpeed = new TextBox() { Text = "0", Margin = new Thickness(left_most + 300, top_most + 380, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };
            tbxRange = new TextBox() { Text = "5", Margin = new Thickness(left_most + 450, top_most + 380, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };

            cbxPostConversionShutdown = new ComboBox() { Margin = new Thickness(left_most + 600, top_most + 380, 0, 0), Width = 120, Height = 25, SelectedIndex = 0, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            cbxPostConversionShutdown.Items.Add("Shutdown");
            cbxPostConversionShutdown.Items.Add("No Shutdown");

            // Line 15
            lblIntegrationAveraging = new Label() { Content = "Integration Averaging", Margin = new Thickness(left_most, top_most + 420, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblDetectorSource = new Label() { Content = "DetectorSource", Margin = new Thickness(left_most + 150, top_most + 420, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblIntegrationLimit = new Label() { Content = "Integration Limit", Margin = new Thickness(left_most + 300, top_most + 420, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            lblOffsetIntegrationLimit = new Label() { Content = "Offset Integration Limit", Margin = new Thickness(left_most + 450, top_most + 420, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

            // Line 16
            tbxIntegrationAveraging = new TextBox() { Text = "1", Margin = new Thickness(left_most, top_most + 440, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };
            cbxDetectorDataSource = new ComboBox() { Margin = new Thickness(left_most + 150, top_most + 440, 0, 0), Width = 120, Height = 25, SelectedIndex = 1, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            cbxDetectorDataSource.Items.Add("Null");
            cbxDetectorDataSource.Items.Add("ADC Data");
            cbxDetectorDataSource.Items.Add("Test Data");

            tbxIntegrationLimit = new TextBox() { Text = "0", Margin = new Thickness(left_most + 300, top_most + 440, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };
            tbxOffsetIntegrationLimit = new TextBox() { Text = "0", Margin = new Thickness(left_most + 450, top_most + 440, 0, 0), Height = 23, Width = 120, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MinWidth = 60 };

            btnLoadImaTable = new Button() { Content = "Load Ima Table", Margin = new Thickness(left_most + 600, top_most + 440, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, Width = 120, Height = 30, VerticalAlignment = VerticalAlignment.Top };

            imaTable = new ImaTable();
            imaLoaded = false;

        }
    }

    public struct ThermalSliceBoard {

        public CheckBox Selection;
        public TextBox Result;
        public TextBox Response;
        public UInt32 Address;
    }

    struct FileTransmit {

        public string LocalFile;
        public string RemoteFile;
        public uint Permission;
    }

    public partial class MainWindow : Window {

        private List<ThermalSliceBoard> m_thermal_sliceboards = null;
        private List<CheckBox> m_adc_sliceboards = null;
        private List<TextBox> m_tbx_agg_reply = null;
        private List<ExposureControl> m_exposure_controls = null;
        private List<TabItem> m_exposure_tabs = null;

        private RoterControlSupport.RoterController m_rcb = null;
        private RoterControlSupport.PeakCan m_gbox_canbus = null;

        bool m_thread_run = false;
        private Thread m_thread_process_notification = null;
        private Thread m_thread_process_log = null;
        private Thread m_thread_read_gbox_can = null;

        private bool m_rcb_connected = false;

        private List<SeriesParameter> m_scan_parameters = null;

        private int m_ape1_position, m_ape2_position, m_filt_position;
        private int m_x_step;

        private bool m_messaging_used = false;

        private const string gkResMainContactor = "contactor";
        private const string gkResDoor = "door";
        private const string gkResState = "state";
        private const string gkResTubeHeat = "tube_heat";

        public MainWindow() {

            InitializeComponent();

            AddControls();

            Initialize();
        }

        private void Main_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

            if (m_rcb_connected) {

                m_thread_run = false;
                Thread.Sleep(500);

                m_rcb.StopRcbServiceAsync();
                m_rcb.StopEngineeringServiceAsync();

                m_thread_process_notification.Abort();
                m_thread_process_notification.Join();

                m_thread_process_log.Abort();
                m_thread_process_log.Join();

                if (m_thread_read_gbox_can != null) {

                    m_thread_read_gbox_can.Abort();
                    m_thread_read_gbox_can.Join();
                }
            }

            if (m_messaging_used) {

                TurnOffXrayRoomLamp();
                cFramework.ShutdownMessaging();
            }
        }

        private void AddControls() {

            #region HouseKeeping

            #endregion

            #region Thermal
            m_thermal_sliceboards = new List<ThermalSliceBoard>();

            int checkBoxTop = 35;
            int textBoxTop = 32;
            for (int i = 0 ; i < 27 ; i++) {

                ThermalSliceBoard sliceBoard = new ThermalSliceBoard();
                sliceBoard.Selection = new CheckBox() { Content = $"#{i + 1:00}", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(30, checkBoxTop, 0, 0) };
                sliceBoard.Result = new TextBox() { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, TextWrapping = TextWrapping.NoWrap, MinWidth = 0, Width = 70, MinHeight = 0, Height = 22, Margin = new Thickness(80, textBoxTop, 0, 0), FontSize = 10 };
                sliceBoard.Response = new TextBox() { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, TextWrapping = TextWrapping.NoWrap, MinWidth = 0, Width = 120, MinHeight = 0, Height = 22, Margin = new Thickness(160, textBoxTop, 0, 0), FontSize = 10 };
                sliceBoard.Address = RoterController.HeaterXX | (uint)i << 23;

                //sliceBoard.Result.Text = "01234567";
                //sliceBoard.Response.Text = "012345678012345678";

                grdSliceBoard.Children.Add(sliceBoard.Selection);
                grdSliceBoard.Children.Add(sliceBoard.Result);
                grdSliceBoard.Children.Add(sliceBoard.Response);

                m_thermal_sliceboards.Add(sliceBoard);

                checkBoxTop += 28;
                textBoxTop += 28;
            }

            m_adc_sliceboards = new List<CheckBox>();
            m_tbx_agg_reply = new List<TextBox>();
            #endregion Thermal

            #region Detector
            int checkBoxLeft = 20;
            for (int column = 0 ; column < 3 ; column++) {

                checkBoxTop = 60;

                for (int line = 0 ; line < 9 ; line++) {

                    CheckBox checkBox = new CheckBox() { Content = $"#{line + column * 9 + 1:00}", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(checkBoxLeft, checkBoxTop, 0, 0) };

                    grdDetConfigEnv.Children.Add(checkBox);

                    m_adc_sliceboards.Add(checkBox);

                    checkBoxTop += 30;
                }

                checkBoxLeft += 70;
            }

            int textBoxLeft = 120;
            for (int i = 0 ; i < 8 ; i++) {

                TextBox textBox = new TextBox() { Text = "00000000", HorizontalAlignment = HorizontalAlignment.Left, Height = 23, Margin = new Thickness(textBoxLeft, 830, 0, 0), TextWrapping = TextWrapping.NoWrap, Width = 130, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, FontSize = 14, VerticalAlignment = VerticalAlignment.Top };
                grdDetector.Children.Add(textBox);
                m_tbx_agg_reply.Add(textBox);

                textBoxLeft += 160;
            }
            #endregion Detector

            #region Exposure
            m_exposure_controls = new List<ExposureControl>();
            m_exposure_tabs = new List<TabItem>();

            for (int tab = 0 ; tab < 10 ; tab++) {

                TabItem ti = new TabItem();
                ti.Header = $"Series {tab + 1}";
                tabScanParameter.Items.Add(ti);

                ExposureControl ec = new ExposureControl();
                m_exposure_controls.Add(ec);

                Grid gd = new Grid();
                ti.Content = gd;

                gd.Children.Add(ec.lblKv);
                gd.Children.Add(ec.lblMa);
                gd.Children.Add(ec.lblFss);
                gd.Children.Add(ec.lblExposureTime);
                gd.Children.Add(ec.lblScanSteps);
                gd.Children.Add(ec.lblScanTime);

                gd.Children.Add(ec.tbxKv);
                gd.Children.Add(ec.tbxMa);
                gd.Children.Add(ec.cbxFss);
                gd.Children.Add(ec.tbxExposureTime);
                gd.Children.Add(ec.tbxScanSteps);
                gd.Children.Add(ec.tbxScanTime);

                gd.Children.Add(ec.lblTriggerMode);
                gd.Children.Add(ec.lblTriggerPosition);
                gd.Children.Add(ec.lblPositionType);
                gd.Children.Add(ec.lblTicksPerRotation);
                gd.Children.Add(ec.lblEncoderSource);

                gd.Children.Add(ec.cbxTriggerMode);
                gd.Children.Add(ec.tbxTriggerPosition);
                gd.Children.Add(ec.cbxPositionType);
                gd.Children.Add(ec.cbxTicksPerRotation);
                gd.Children.Add(ec.cbxEncoderSource);

                gd.Children.Add(ec.lblIntegrationTime);
                gd.Children.Add(ec.lblDitherType);
                gd.Children.Add(ec.lblTimePerRotationInMSec);
                gd.Children.Add(ec.lblTimeoutBetweenArmXrayOn);
                gd.Children.Add(ec.lblCineScan);

                gd.Children.Add(ec.tbxIntegrationTime);
                gd.Children.Add(ec.cbxDitherType);
                gd.Children.Add(ec.tbxTimePerRotationInMSec);
                gd.Children.Add(ec.tbxTimeoutBetweenArmXrayOn);
                gd.Children.Add(ec.cbxCineScan);

                gd.Children.Add(ec.lblDelayBeforeNextSeries);
                gd.Children.Add(ec.tbxDelayBeforeNextSeries);
                gd.Children.Add(ec.lblDelayBetweenShots);
                gd.Children.Add(ec.tbxDelayBetweenShots);

                gd.Children.Add(ec.lblCardiacScan);
                gd.Children.Add(ec.lblPhasePercentage);
                gd.Children.Add(ec.lblApertureMode);
                gd.Children.Add(ec.lblFilterMode);
                gd.Children.Add(ec.lblRowNumber);

                gd.Children.Add(ec.cbxCardiacScan);
                gd.Children.Add(ec.tbxPhasePercentage);
                gd.Children.Add(ec.tbxApertureMode);
                gd.Children.Add(ec.tbxFilterMode);
                gd.Children.Add(ec.tbxRowNumber);

                gd.Children.Add(ec.lblErrorRegisterReset);
                gd.Children.Add(ec.lblStartingSlice);
                gd.Children.Add(ec.lblEndingSlice);
                gd.Children.Add(ec.lblDataSource);
                gd.Children.Add(ec.lblInputSource);

                gd.Children.Add(ec.cbxErrorRegisterReset);
                gd.Children.Add(ec.tbxStartingSlice);
                gd.Children.Add(ec.tbxEndingSlice);
                gd.Children.Add(ec.cbxDataSource);
                gd.Children.Add(ec.tbxInputSource);

                gd.Children.Add(ec.lblSampleMode);
                gd.Children.Add(ec.lblDecimation);
                gd.Children.Add(ec.lblClockSpeed);
                gd.Children.Add(ec.lblRange);
                gd.Children.Add(ec.lblPostConversionShutdown);

                gd.Children.Add(ec.tbxSampleMode);
                gd.Children.Add(ec.tbxDecimation);
                gd.Children.Add(ec.tbxClockSpeed);
                gd.Children.Add(ec.tbxRange);
                gd.Children.Add(ec.cbxPostConversionShutdown);

                gd.Children.Add(ec.lblIntegrationAveraging);
                gd.Children.Add(ec.lblDetectorSource);
                gd.Children.Add(ec.lblIntegrationLimit);
                gd.Children.Add(ec.lblOffsetIntegrationLimit);

                gd.Children.Add(ec.tbxIntegrationAveraging);
                gd.Children.Add(ec.cbxDetectorDataSource);
                gd.Children.Add(ec.tbxIntegrationLimit);
                gd.Children.Add(ec.tbxOffsetIntegrationLimit);

                gd.Children.Add(ec.ckbIma);
                gd.Children.Add(ec.ckbEmergencyScan);

                ec.btnLoadImaTable.Click += new RoutedEventHandler(btnLoadImaTable_Click);
                gd.Children.Add(ec.btnLoadImaTable);

                m_exposure_tabs.Add(ti);

                if (tab != 0) {

                    ti.IsEnabled = false;
                }
            }

            #endregion Exposure

            #region Error Code
            int left = 80;
            int top = 50;

            foreach (KeyValuePair<ushort, string> entry in RoterControllerException.ErrorTable) {

                Label label = new Label();
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.VerticalAlignment = VerticalAlignment.Top;
                label.Content = $"{entry.Key:X4}, {entry.Value}";
                label.Margin = new Thickness(left, top, 0, 0);

                if (top > 650) {

                    top = 50;
                    left += 250;
                }
                else {

                    top += 20;
                }

                grdErrorCode.Children.Add(label);

                //tbxInfo.AppendText($"{entry.Key:X4}, {entry.Value}\n");
            }
            #endregion Error Code
        }

        private void Initialize() {

            iconXrayOn.Visibility = Visibility.Hidden;

            pgbFileTransProgress.Visibility = Visibility.Hidden;
            tblFileTransProgress.Visibility = Visibility.Hidden;

            pgbXrayProgress.Visibility = Visibility.Hidden;
            tblXrayProgress.Visibility = Visibility.Hidden;

            ledXrayOn.Visibility = Visibility.Hidden;

            m_ape1_position = int.MaxValue;
            m_ape2_position = int.MaxValue;
            m_filt_position = int.MaxValue;

            m_x_step = int.MaxValue;
        }

        #region Notification
        private void ProcessNotification() {

            Notification notification = null;

            while (m_thread_run) {

                notification = m_rcb.TakeNotification();

                ulong value = notification.Data64;

                switch ((ushort)notification.CommandBits) {

                case RoterController.NTF_XMGRSTATE:

                    UpdateXmgrState(value);
                    break;

                case RoterController.NTF_XRAYON:

                    UpdateXraySign(value);
                    break;

                case RoterController.NTF_TUBEHEAT:

                    UpdateTubeHeat(value);
                    break;

                case RoterController.NTF_WARMUP:

                case RoterController.NTF_SEASONING:

                case RoterController.NTF_FILCAL:

                    UpdateXrayProgress(value);
                    break;

                case RoterController.NTF_XSTEP:

                    m_x_step = (int)value;
                    UpdateXStep(m_x_step);

                    SimulateMStepCStep(m_x_step);

                    break;

                case RoterController.NTF_AP1POS:

                    m_ape1_position = (int)value;
                    UpdateApe1Position(m_ape1_position);
                    break;

                case RoterController.NTF_AP2POS:

                    m_ape2_position = (int)value;
                    UpdateApe2Position(m_ape2_position);
                    break;

                case RoterController.NTF_FLTPOS:

                    m_filt_position = (int)value;
                    UpdateFiltPosition(m_filt_position);
                    break;

                case RoterController.NTF_DIAG_TCUERR:

                    UpdateDiagTcuErr(value);
                    break;

                case RoterController.NTF_DIAG_XRAYON:

                    UpdateDiagXrayOn(value);
                    break;

                case RoterController.NTF_DIAG_KVMAOK:

                    UpdateDiagKvMaOk(value);
                    break;

                case RoterController.NTF_FILEPROGRESS:

                    UpdateTransmissionProgress(value);
                    break;
                }
            }
        }

        private void UpdateXmgrState(ulong p_value) {

            string state_name = "";

            switch (p_value) {

            case 0x00:  // idle
                state_name = "Idle";
                break;

            case 0x01:  // ready
                state_name = "Ready";
                break;

            case 0x02:  // boosted
                state_name = "Boosted";
                break;

            case 0x03:  // exposing
                state_name = "Exposing";
                break;
            }

            if (m_messaging_used) {

                string string_state = "state=" + state_name;

                cFramework.AutoscribeUpdateStatus(gkResState, string_state);
            }

            this.Dispatcher.Invoke(new Action(() => tbxHVState.Text = state_name));
        }

        private void UpdateXraySign(ulong p_value) {

            if (p_value == 0) {

                this.Dispatcher.Invoke(new Action(() => iconXrayOn.Visibility = Visibility.Hidden));

                TurnOffXrayRoomLamp();
            }
            else {

                this.Dispatcher.Invoke(new Action(() => iconXrayOn.Visibility = Visibility.Visible));

                TurnOnXrayRoomLamp();
            }
        }

        private void UpdateXrayProgress(ulong p_value) {

            if (p_value == 0 || p_value == 100) {

                this.Dispatcher.Invoke(new Action(() => pgbXrayProgress.Visibility = Visibility.Hidden));
                this.Dispatcher.Invoke(new Action(() => tblXrayProgress.Visibility = Visibility.Hidden));
            }
            else {

                this.Dispatcher.Invoke(new Action(() => pgbXrayProgress.Visibility = Visibility.Visible));
                this.Dispatcher.Invoke(new Action(() => pgbXrayProgress.Value = p_value));
                this.Dispatcher.Invoke(new Action(() => tblXrayProgress.Visibility = Visibility.Visible));
                this.Dispatcher.Invoke(new Action(() => tblXrayProgress.Text = $"{(p_value / 100.0):P0}"));
            }
        }

        private void UpdateXStep(int p_value) {

            this.Dispatcher.Invoke(new Action(() => tbxXStep.Text = $"{p_value}"));
        }

        private void SimulateMStepCStep(int p_xstep) {

            this.Dispatcher.Invoke(new Action(() => {

                if (cbxSimulation.IsChecked == true) {

                    m_rcb.NotifyCStep(m_x_step + 1);
                    m_rcb.NotifyMStep(m_x_step);
                }
            }));
        }

        private void UpdateApe1Position(int p_value) {

            this.Dispatcher.Invoke(new Action(() => tbxAp1ActualPosition.Text = $"{p_value}"));
        }

        private void UpdateApe2Position(int p_value) {

            this.Dispatcher.Invoke(new Action(() => tbxAp2ActualPosition.Text = $"{p_value}"));
        }

        private void UpdateFiltPosition(int p_value) {

            this.Dispatcher.Invoke(new Action(() => tbxFltActualPosition.Text = $"{p_value}"));
        }

        private void UpdateCollimatorPosition(int ap1_position, int ap2_position, int flt_position) {

            this.Dispatcher.Invoke(new Action(() => tbxAp1ActualPosition.Text = $"{ap1_position}"));
            this.Dispatcher.Invoke(new Action(() => tbxAp2ActualPosition.Text = $"{ap2_position}"));
            this.Dispatcher.Invoke(new Action(() => tbxFltActualPosition.Text = $"{flt_position}"));
        }

        private void TurnOnXrayRoomLamp() {

            if (m_messaging_used) {

                // XRAYLIGHT state=(on | off) ==> ScanControlBoardManager
                cFramework.SendMessage("XRAYLIGHT", "state=on", "ScanControlBoardManager");
            }
        }

        private void TurnOffXrayRoomLamp() {

            if (m_messaging_used) {

                // XRAYLIGHT state=(on | off) ==> ScanControlBoardManager
                cFramework.SendMessage("XRAYLIGHT", "state=off", "ScanControlBoardManager");
            }
        }

        private void UpdateTubeHeat(ulong p_value) {

            this.Dispatcher.Invoke(new Action(() => tbxTubeHeat.Text = $"{(double)p_value / 10000.0:P2}"));

            if (m_messaging_used) {

                cFramework.AutoscribeUpdateStatus(gkResTubeHeat, $"TUBE_HEAT={(double)p_value / 10000.0:P2}");
            }
        }

        private void UpdateTablePosition(ulong p_value) {

            this.Dispatcher.Invoke(new Action(() => tbxTablePosition.Text = $"{p_value}"));
        }

        private void UpdateGantryPosition(ulong p_value) {

            this.Dispatcher.Invoke(new Action(() => tbxGantryPosition.Text = $"{p_value}"));
        }

        private void UpdateDiagTcuErr(ulong p_value) {

            if (p_value == 0) {

                this.Dispatcher.Invoke(new Action(() => ledTcuError.Style = (Style)FindResource("FMI_TinyRedOff_Button")));
            }
            else {

                this.Dispatcher.Invoke(new Action(() => ledTcuError.Style = (Style)FindResource("FMI_TinyRedOn_Button")));
            }
        }

        private void UpdateDiagKvMaOk(ulong p_value) {

            if (p_value == 0) {

                this.Dispatcher.Invoke(new Action(() => ledKvMaOk.Style = (Style)FindResource("FMI_TinyGreenOff_Button")));
            }
            else {

                this.Dispatcher.Invoke(new Action(() => ledKvMaOk.Style = (Style)FindResource("FMI_TinyGreenOn_Button")));
            }
        }

        private void UpdateDiagXrayOn(ulong p_value) {

            if (p_value == 0) {

                this.Dispatcher.Invoke(new Action(() => ledXrayOn.Visibility = Visibility.Hidden));
            }
            else {

                this.Dispatcher.Invoke(new Action(() => ledXrayOn.Visibility = Visibility.Visible));
            }
        }

        private void iconDoor_Click(object sender, RoutedEventArgs e) {

            try {

                if (iconDoor.Style == (Style)FindResource("FMI_DoorClosed_Button")) {

                    iconDoor.Style = (Style)Application.Current.Resources["FMI_DoorOpen_Button"];
                    lblDoor.Content = "open";
                    if (m_rcb_connected) {

                        m_rcb.NotifyDoorStatus(false);
                    }
                }
                else {

                    iconDoor.Style = (Style)Application.Current.Resources["FMI_DoorClosed_Button"];
                    lblDoor.Content = "closed";
                    if (m_rcb_connected) {

                        m_rcb.NotifyDoorStatus(true);
                    }
                }
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnResetTablePosition_Click(object sender, RoutedEventArgs e) {

            m_rcb.NotifyTablePosition();
        }
        #endregion

        #region Log
        private void PrintRotorControllerLog() {

            string log_message;

            while (m_thread_run) {

                log_message = m_rcb.TakeLog();

                this.Dispatcher.Invoke(new Action(() => tbxInfo.AppendText(log_message)));
                this.Dispatcher.Invoke(new Action(() => tbxInfo.ScrollToEnd()));
            }
        }

        private void btnListLog_Click(object sender, RoutedEventArgs e) {

            try {

                List<string> log_list;

                lsvLog.Items.Clear();

                m_rcb.ScanDir("/home/root/log/", out log_list);

                foreach (string log in log_list) {

                    CheckBox ckbox = new CheckBox();

                    ckbox.Content = log;

                    lsvLog.Items.Add(ckbox);
                }
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteLog_Click(object sender, RoutedEventArgs e) {

            try {

                List<string> log_list = new List<string>();

                foreach (CheckBox ckbox in lsvLog.Items) {

                    if (ckbox.IsChecked == true) {

                        log_list.Add("/home/root/log/" + (ckbox.Content as string));
                    }
                }

                m_rcb.DeleteFiles(log_list);

                for (int i = lsvLog.Items.Count - 1 ; i >= 0 ; i--) {

                    if ((lsvLog.Items[i] as CheckBox).IsChecked == true) {

                        lsvLog.Items.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDownloadLog_Click(object sender, RoutedEventArgs e) {

            SaveFileDialog save_dialog = new SaveFileDialog();

            save_dialog.OverwritePrompt = false;

            if (save_dialog.ShowDialog() == true) {

                try {

                    string local_directory = Path.GetDirectoryName(save_dialog.FileName);

                    List<FileTransmit> ft_list = new List<FileTransmit>();

                    foreach (CheckBox ckbox in lsvLog.Items) {

                        if (ckbox.IsChecked == true) {

                            FileTransmit ft = new FileTransmit();

                            string file_name = ckbox.Content as string;
                            ft.LocalFile = $"{local_directory}/{file_name}";
                            ft.RemoteFile = $"/home/root/log/{file_name}";

                            ft_list.Add(ft);
                        }
                    }

                    new Thread(() => RetrieveFiles(sender, ft_list)).Start();
                }
                catch (Exception ex) {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSelectAllLog_Click(object sender, RoutedEventArgs e) {

            foreach (CheckBox ckbox in lsvLog.Items) {

                ckbox.IsChecked = true;
            }
        }

        private void btnDeselectAllLog_Click(object sender, RoutedEventArgs e) {

            foreach (CheckBox ckbox in lsvLog.Items) {

                ckbox.IsChecked = false;
            }
        }
        #endregion Log

        #region House Keeping
        private void btnConnect_Click(object sender, RoutedEventArgs e) {

            try {

                int device_id = int.Parse(tbxDeviceId.Text, System.Globalization.NumberStyles.AllowHexSpecifier);

                if (m_rcb == null) {

                    m_rcb = new RoterController(device_id);
                }

                m_thread_run = true;

                m_thread_process_notification = new Thread(() => ProcessNotification());
                m_thread_process_notification.IsBackground = true;
                m_thread_process_notification.Start();

                m_thread_process_log = new Thread(() => PrintRotorControllerLog());
                m_thread_process_log.IsBackground = true;
                m_thread_process_log.Start();

                m_rcb.SyncTime();

                grdHouseKeeper.IsEnabled = true;
                grdLog.IsEnabled = true;
                grdRegister.IsEnabled = true;
                grdHighVoltage.IsEnabled = true;
                grdCollimatorLaser.IsEnabled = true;
                grdDetector.IsEnabled = true;
                grdThermalControl.IsEnabled = true;
                grdRCBDiagnostics.IsEnabled = true;

                btnConnect.IsEnabled = false;

                m_rcb_connected = true;
            }
            catch (FormatException) {

                MessageBox.Show("Device Id has bad format");
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSyncTime_Click(object sender, RoutedEventArgs e) {

            try {

                m_rcb.SyncTime();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnStartBryce_Click(object sender, RoutedEventArgs e) {

            try {

                iconDoor.Style = (Style)Application.Current.Resources["FMI_DoorOpen_Button"];
                lblDoor.Content = "unknown";

                m_rcb.StartRcbService();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnStopBryce_Click(object sender, RoutedEventArgs e) {

            try {

                iconDoor.Style = (Style)Application.Current.Resources["FMI_DoorOpen_Button"];
                lblDoor.Content = "unknown";

                m_rcb.StopRcbService();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnStartDenali_Click(object sender, RoutedEventArgs e) {

            try {

                m_rcb.StartEngineeringService();

                try {

                    m_gbox_canbus = new PeakCan(0x2d, Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_500K);

                    btnDiagCAN2.IsEnabled = true;

                    m_thread_read_gbox_can = new Thread(new ThreadStart(ReadGBoxCanBus));
                    m_thread_read_gbox_can.IsBackground = true;
                    m_thread_read_gbox_can.Start();
                }
                catch (Exception ex) {

                    btnDiagCAN2.IsEnabled = false;
                    MessageBox.Show(ex.Message);
                }

                grdRCBDiagnostics.IsEnabled = true;
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCloseDenali_Click(object sender, RoutedEventArgs e) {

            try {

                m_rcb.StopEngineeringService();

                grdRCBDiagnostics.IsEnabled = false;
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetLogLevelBryce_Click(object sender, RoutedEventArgs e) {

            int level = cbxLogLevelBryce.SelectedIndex;

            try {

                m_rcb.SetLogLevelBryce(level);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnTurnOffBryceLog_Click(object sender, RoutedEventArgs e) {

            try {

                m_rcb.TurnOffLogBryce();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnTurnOnLogStdOutputBryce_Click(object sender, RoutedEventArgs e) {

            try {

                m_rcb.TurnOnStdOutput();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnTurnOnLogFileOutputBryce_Click(object sender, RoutedEventArgs e) {

            try {

                m_rcb.TurnOnFileOutput();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnTurnOnLogCanOutputBryce_Click(object sender, RoutedEventArgs e) {

            try {

                m_rcb.TurnOnCanOutput();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnTurnOffDenaliLog_Click(object sender, RoutedEventArgs e) {

        }

        private void btnTurnOnLogStdOutputDenali_Click(object sender, RoutedEventArgs e) {

        }

        private void btnTurnOnLogFileOutputDenali_Click(object sender, RoutedEventArgs e) {

        }

        private void btnTurnOnLogCanOutputDenali_Click(object sender, RoutedEventArgs e) {

        }

        private void btnUpdateFPGA_Click(object sender, RoutedEventArgs e) {

            OpenFileDialog open_dialog = new OpenFileDialog();

            if (open_dialog.ShowDialog() == true) {

                new Thread(() => UpdateFPGA(sender, open_dialog.FileName)).Start();
            }
        }

        private void UpdateFPGA(object sender, string p_file_path) {

            Button button = (sender as Button);
            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = false));

            try {

                m_rcb.UpdateFPGA(p_file_path);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = true));
        }

        private void btnUpdateHPSFW_Click(object sender, RoutedEventArgs e) {

            OpenFileDialog open_dialog = new OpenFileDialog();

            if (open_dialog.ShowDialog() == true) {

                new Thread(() => UpdateHPSFW(sender, open_dialog.FileName)).Start();
            }
        }

        private void UpdateHPSFW(object sender, string p_file_path) {

            Button button = (sender as Button);
            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = false));

            try {

                m_rcb.UpdateHPSFW(p_file_path);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = true));
        }

        private void btnUploadConfig_Click(object sender, RoutedEventArgs e) {

            OpenFileDialog open_dialog = new OpenFileDialog();

            open_dialog.Multiselect = true;

            if (open_dialog.ShowDialog() == true) {

                try {

                    List<FileTransmit> ft_list = new List<FileTransmit>();

                    foreach (string file_name in open_dialog.FileNames) {

                        FileTransmit ft = new FileTransmit();

                        ft.Permission = Convert.ToUInt32("0644", 8);
                        ft.LocalFile = file_name;
                        ft.RemoteFile = "/home/root/etc/" + Path.GetFileName(file_name);

                        ft_list.Add(ft);
                    }

                    new Thread(() => TransmitFiles(sender, ft_list)).Start();
                }
                catch (Exception ex) {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnShutdownLinux_Click(object sender, RoutedEventArgs e) {

            try {

                iconDoor.Style = (Style)Application.Current.Resources["FMI_DoorOpen_Button"];
                lblDoor.Content = "unknown";

                m_rcb.ShutdownLinux();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnRestartLinux_Click(object sender, RoutedEventArgs e) {

            try {

                iconDoor.Style = (Style)Application.Current.Resources["FMI_DoorOpen_Button"];
                lblDoor.Content = "unknown";

                m_rcb.RestartLinux();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnTransmitFile_Click(object sender, RoutedEventArgs e) {

            OpenFileDialog open_dialog = new OpenFileDialog();

            if (open_dialog.ShowDialog() == true) {

                try {

                    FileTransmit ft = new FileTransmit();

                    ft.Permission = Convert.ToUInt32(tbxPermission.Text, 8);
                    ft.LocalFile = open_dialog.FileName;
                    ft.RemoteFile = tbxDestPath.Text;

                    new Thread(() => TransmitFiles(sender, new List<FileTransmit>() { ft })).Start();
                }
                catch (Exception ex) {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void TransmitFiles(object sender, List<FileTransmit> p_file_list) {

            Button button = (sender as Button);

            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = false));

            try {

                foreach (FileTransmit ft in p_file_list) {

                    m_rcb.TransmitFile(ft.LocalFile, ft.Permission, ft.RemoteFile);
                }
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = true));
        }

        private void btnRetrieveFile_Click(object sender, RoutedEventArgs e) {

            SaveFileDialog save_dialog = new SaveFileDialog();

            if (save_dialog.ShowDialog() == true) {

                try {

                    FileTransmit ft = new FileTransmit();

                    ft.RemoteFile = tbxDestPath.Text;
                    ft.LocalFile = save_dialog.FileName;

                    new Thread(() => RetrieveFiles(sender, new List<FileTransmit>() { ft })).Start();
                }
                catch (Exception ex) {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void RetrieveFiles(object sender, List<FileTransmit> p_file_list) {

            Button button = (sender as Button);

            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = false));

            try {

                foreach (FileTransmit ft in p_file_list) {

                    m_rcb.RetrieveFile(ft.LocalFile, ft.RemoteFile);
                }
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = true));
        }

        private void btnAbortFileTransmission_Click(object sender, RoutedEventArgs e) {

            try {

                m_rcb.AbortTransmit();
                pgbFileTransProgress.Visibility = Visibility.Hidden;
                tblFileTransProgress.Visibility = Visibility.Hidden;
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateTransmissionProgress(double percent) {

            if (percent == 0 || percent == 100) {

                this.Dispatcher.Invoke(new Action(() => pgbFileTransProgress.Visibility = Visibility.Hidden));
                this.Dispatcher.Invoke(new Action(() => tblFileTransProgress.Visibility = Visibility.Hidden));
            }
            else {

                this.Dispatcher.Invoke(new Action(() => pgbFileTransProgress.Visibility = Visibility.Visible));
                this.Dispatcher.Invoke(new Action(() => pgbFileTransProgress.Value = percent));
                this.Dispatcher.Invoke(new Action(() => tblFileTransProgress.Visibility = Visibility.Visible));
                this.Dispatcher.Invoke(new Action(() => tblFileTransProgress.Text = $"{(percent / 100.0):P0}"));
            }
        }
        #endregion House Keeping

        #region Register
        private void btn031_Click(object sender, RoutedEventArgs e) {


        }

        private void btn032_Click(object sender, RoutedEventArgs e) {


        }

        private void btn001_Click(object sender, RoutedEventArgs e) {

            tbxTablePosition.Text = $"{0.11:P2}";
        }

        private void btnWriteRegister1_Click(object sender, RoutedEventArgs e) {

            try {

                int offset = Convert.ToInt32(tbxOffset1.Text, 16);
                ulong content = Convert.ToUInt64(tbxRegister1.Text, 16);

                m_rcb.WriteRegister(offset >> 3, content);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadRegister1_Click(object sender, RoutedEventArgs e) {

            try {

                int offset = Convert.ToInt32(tbxOffset1.Text, 16);
                ulong content = m_rcb.ReadRegister(offset >> 3);

                tbxRegister1.Text = $"{content:X16}";
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnWriteRegister2_Click(object sender, RoutedEventArgs e) {

            try {

                int offset = Convert.ToInt32(tbxOffset2.Text, 16);
                ulong content = Convert.ToUInt64(tbxRegister2.Text, 16);

                m_rcb.WriteRegister(offset >> 3, content);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadRegister2_Click(object sender, RoutedEventArgs e) {

            try {

                int offset = Convert.ToInt32(tbxOffset2.Text, 16);
                ulong content = m_rcb.ReadRegister(offset >> 3);

                tbxRegister2.Text = $"{content:X16}";
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region TCU G-Box
        private void btnUpdateFW_Click(object sender, RoutedEventArgs e) {

        }
        #endregion

        #region High Voltage

        private void cbxNumberOfScans_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if (m_exposure_tabs != null) {

                int number_of_scans = (int)cbxNumberOfScans.SelectedValue;

                foreach (TabItem ti in m_exposure_tabs) {

                    ti.IsEnabled = m_exposure_tabs.IndexOf(ti) < number_of_scans;
                }
            }
        }

        private void btnInitHighVoltage_Click(object sender, RoutedEventArgs e) {

            new Thread(() => InitHighVoltage()).Start();
        }

        private void InitHighVoltage() {

            this.Dispatcher.Invoke(new Action(() => btnHVInit.IsEnabled = false));

            try {

                m_rcb.InitHighVoltage();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnHVInit.IsEnabled = true));
        }

        private void btnStartAnode_Click(object sender, RoutedEventArgs e) {

            new Thread(() => StartAnode()).Start();
        }

        private void StartAnode() {

            this.Dispatcher.Invoke(new Action(() => btnStartAnode.IsEnabled = false));

            try {

                m_rcb.StartAnode();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnStartAnode.IsEnabled = true));
        }

        private void btnStopAnode_Click(object sender, RoutedEventArgs e) {

            new Thread(() => StopAnode()).Start();
        }

        private void StopAnode() {

            this.Dispatcher.Invoke(new Action(() => btnStopAndoe.IsEnabled = false));

            try {

                m_rcb.StopAnode();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnStopAndoe.IsEnabled = true));
        }

        private void btnDailyWarmup_Click(object sender, RoutedEventArgs e) {

            new Thread(() => DailyWarmup()).Start();
        }

        private void DailyWarmup() {

            this.Dispatcher.Invoke(new Action(() => btnDailyWarmup.IsEnabled = false));

            try {

                m_rcb.Warmup(0);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            UpdateXrayProgress(100);

            this.Dispatcher.Invoke(new Action(() => btnDailyWarmup.IsEnabled = true));
        }

        private void btnQuickWarmup_Click(object sender, RoutedEventArgs e) {

            new Thread(() => QuickWarmup()).Start();
        }

        private void QuickWarmup() {

            this.Dispatcher.Invoke(new Action(() => btnQuickWarmup.IsEnabled = false));

            try {

                m_rcb.Warmup(1);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            UpdateXrayProgress(100);

            this.Dispatcher.Invoke(new Action(() => btnQuickWarmup.IsEnabled = true));
        }

        private void btnSeason_Click(object sender, RoutedEventArgs e) {

            new Thread(() => Season()).Start();
        }

        private void Season() {

            this.Dispatcher.Invoke(new Action(() => btnSeason.IsEnabled = false));

            try {

                m_rcb.Season();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            UpdateXrayProgress(100);

            this.Dispatcher.Invoke(new Action(() => btnSeason.IsEnabled = true));
        }

        private void btnFilcal_Click(object sender, RoutedEventArgs e) {

            List<byte> kvlist = new List<byte>();

            if (cbx80kV.IsChecked == true) {

                kvlist.Add(80);
            }

            if (cbx100kV.IsChecked == true) {

                kvlist.Add(100);
            }

            if (cbx120kV.IsChecked == true) {

                kvlist.Add(120);
            }

            if (cbx140kV.IsChecked == true) {

                kvlist.Add(140);
            }

            new Thread(() => Filcal(kvlist)).Start();
        }

        private void Filcal(List<byte> kvlist) {

            this.Dispatcher.Invoke(new Action(() => btnFilcal.IsEnabled = false));

            try {

                m_rcb.Filcal(kvlist);
            }
            catch (Exception e) {

                MessageBox.Show(e.Message);
            }

            UpdateXrayProgress(100);

            this.Dispatcher.Invoke(new Action(() => btnFilcal.IsEnabled = true));
        }

        private void btnPrepare_Click(object sender, RoutedEventArgs e) {

            try {

                if (lblDoor.Content.ToString() != "closed") {

                    throw new Exception("Door is open");
                }

                GetExposureParameter();

                new Thread(() => Prepare()).Start();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void GetExposureParameter() {

            m_scan_parameters = new List<SeriesParameter>();

            for (int tab = 0 ; tab < m_exposure_tabs.Count ; tab++) {

                if (!m_exposure_tabs[tab].IsEnabled) {

                    continue;
                }

                ExposureControl ec = m_exposure_controls[tab];

                SeriesParameter series_parameter = new SeriesParameter();

                series_parameter.Kv = Convert.ToByte(ec.tbxKv.Text);
                series_parameter.Ma = Convert.ToUInt16(ec.tbxMa.Text);

                if (ec.cbxFss.Text == "Small") {

                    series_parameter.Fss = 0;
                }
                else if (ec.cbxFss.Text == "Medium") {

                    series_parameter.Fss = 1;
                }
                else if (ec.cbxFss.Text == "Large") {

                    series_parameter.Fss = 2;
                }
                else {
                    throw new Exception("Illegal Focal Size");
                }

                series_parameter.ShotTimeInMSec = Convert.ToUInt32(ec.tbxExposureTime.Text);
                series_parameter.NumberOfShots = Convert.ToByte(ec.tbxScanSteps.Text);
                series_parameter.TriggerPosition = Convert.ToInt32(ec.tbxTriggerPosition.Text);
                series_parameter.SeriesTimeInMSec = Convert.ToInt32(ec.tbxScanTime.Text);
                series_parameter.DelayBeforeNextSeries = Convert.ToInt32(ec.tbxDelayBeforeNextSeries.Text);
                series_parameter.DelayBetweenShots = Convert.ToInt32(ec.tbxDelayBetweenShots.Text);

                series_parameter.EmergencyScan = (ec.ckbEmergencyScan.IsChecked == true) ? 1 : 0;
                series_parameter.CardiacScan = (byte)ec.cbxCardiacScan.SelectedIndex;
                series_parameter.PhasePercentage = Convert.ToInt32(ec.tbxPhasePercentage.Text);
                series_parameter.TicksPerRotation = Convert.ToInt16(ec.cbxTicksPerRotation.SelectedValue);
                series_parameter.ScanType = (ec.ckbIma.IsChecked == true) ? 1 : 0;
                series_parameter.DitherType = (byte)ec.cbxDitherType.SelectedIndex;
                series_parameter.EncoderSource = (byte)ec.cbxEncoderSource.SelectedIndex;
                series_parameter.TimeoutBetweenArmXrayOn = Convert.ToInt32(ec.tbxTimeoutBetweenArmXrayOn.Text);

                series_parameter.CineScan = (byte)ec.cbxCineScan.SelectedIndex;

                switch (ec.cbxTriggerMode.Text) {

                case "Immediate":
                    series_parameter.TriggerMode = 0;
                    break;

                case "Traverse":
                    series_parameter.TriggerMode = 1;
                    break;

                case "Theta":
                    series_parameter.TriggerMode = 2;
                    break;

                case "No X-ray":
                    series_parameter.TriggerMode = 3;
                    break;
                }

                series_parameter.RowNumber = Convert.ToByte(ec.tbxRowNumber.Text);
                series_parameter.FilterMode = Convert.ToByte(ec.tbxFilterMode.Text);
                series_parameter.ApertureMode = Convert.ToByte(ec.tbxApertureMode.Text);

                series_parameter.TimePerRotationInMSec = Convert.ToUInt16(ec.tbxTimePerRotationInMSec.Text);
                series_parameter.OffsetIntegrationLimit = Convert.ToUInt16(ec.tbxOffsetIntegrationLimit.Text);
                series_parameter.IntegrationLimit = Convert.ToUInt32(ec.tbxIntegrationLimit.Text);
                series_parameter.IntegrationTime = Convert.ToUInt32(ec.tbxIntegrationTime.Text);
                series_parameter.StartingSlice = Convert.ToByte(ec.tbxStartingSlice.Text);
                series_parameter.ErrorRegisterReset = ec.cbxErrorRegisterReset.SelectedIndex;
                series_parameter.EndingSlice = Convert.ToByte(ec.tbxEndingSlice.Text);
                series_parameter.DataSource = (byte)ec.cbxDataSource.SelectedIndex;
                series_parameter.InputSource = Convert.ToByte(ec.tbxInputSource.Text);
                series_parameter.SampleMode = Convert.ToByte(ec.tbxSampleMode.Text);
                series_parameter.Decimation = Convert.ToByte(ec.tbxDecimation.Text);
                series_parameter.ClockSpeed = Convert.ToByte(ec.tbxClockSpeed.Text);
                series_parameter.Range = Convert.ToByte(ec.tbxRange.Text);
                series_parameter.PostConversionShutdown = (byte)ec.cbxPostConversionShutdown.SelectedIndex;
                series_parameter.IntegrationAveraging = Convert.ToByte(ec.tbxIntegrationAveraging.Text);
                series_parameter.DetectorDataSource = (byte)ec.cbxDetectorDataSource.SelectedIndex;

                if (ec.ckbIma.IsChecked == true) {

                    if (!ec.imaLoaded) {

                        throw new Exception("Ima tabel is not loaded");
                    }

                    series_parameter.ImaTable = ec.imaTable;
                }

                m_scan_parameters.Add(series_parameter);
            }
        }

        private void Prepare() {

            this.Dispatcher.Invoke(new Action(() => btnPrepare.IsEnabled = false));

            try {

                m_rcb.Prepare(m_scan_parameters);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnPrepare.IsEnabled = true));
        }

        private void btnLoadImaTable_Click(object sender, RoutedEventArgs e) {

            int selected_index = 0;

            for (int i = 0 ; i < m_exposure_tabs.Count ; i++) {

                if (m_exposure_tabs[i].IsSelected) {

                    selected_index = i;

                    break;
                }
            }

            OpenFileDialog open_dialog = new OpenFileDialog();

            if (open_dialog.ShowDialog() == true) {

                try {

                    m_exposure_controls[selected_index].imaTable.ParseMapFile(open_dialog.FileName);
                    m_exposure_controls[selected_index].imaLoaded = true;
                }
                catch (Exception ex) {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnScan_Click(object sender, RoutedEventArgs e) {

            new Thread(() => Scan()).Start();
        }

        private void Scan() {

            this.Dispatcher.Invoke(new Action(() => btnScan.IsEnabled = false));

            try {

                int total_scan_time = 0;

                foreach (SeriesParameter series in m_scan_parameters) {

                    total_scan_time += series.SeriesTimeInMSec + series.DelayBeforeNextSeries + 2000;  // boost time ~= 2 sec.
                }

                m_rcb.Expose(total_scan_time);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnScan.IsEnabled = true));
        }

        private void btnAbortScan_Click(object sender, RoutedEventArgs e) {

            new Thread(() => AbortScan()).Start();
        }

        private void AbortScan() {

            this.Dispatcher.Invoke(new Action(() => btnAbortScan.IsEnabled = false));

            try {

                m_rcb.Abort();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnAbortScan.IsEnabled = true));
        }

        private void btnEstimate_Click(object sender, RoutedEventArgs e) {

            try {

                GetExposureParameter();

                new Thread(() => Estimate()).Start();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void Estimate() {

            this.Dispatcher.Invoke(new Action(() => btnEstimate.IsEnabled = false));

            try {

                m_rcb.Estimate(m_scan_parameters);

            }
            catch (Exception e) {

                MessageBox.Show(e.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnEstimate.IsEnabled = true));
        }

        private void btnResetHighVoltage_Click(object sender, RoutedEventArgs e) {

            new Thread(() => ResetHighVoltage()).Start();
        }

        private void ResetHighVoltage() {

            this.Dispatcher.Invoke(new Action(() => btnResetHighVoltage.IsEnabled = false));

            try {

                m_rcb.ResetHighVoltage();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnResetHighVoltage.IsEnabled = true));
        }

        private void btnRebootGbox_Click(object sender, RoutedEventArgs e) {

            new Thread(() => RebootGbox()).Start();
        }

        private void RebootGbox() {

            this.Dispatcher.Invoke(new Action(() => btnRebootGbox.IsEnabled = false));

            try {

                m_rcb.RebootGbox();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnRebootGbox.IsEnabled = true));
        }
        #endregion High Voltage

        #region Laser
        private void btnTurnOnLaser_Click(object sender, RoutedEventArgs e) {

            try {

                m_rcb.TurnOnLaser();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnTurnOffLaser_Click(object sender, RoutedEventArgs e) {

            try {

                m_rcb.TurnOffLaser();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }
        #endregion Laser

        #region Collimator
        private void btnCollimatorVersion_Click(object sender, RoutedEventArgs e) {

            new Thread(() => CollimatorVersion()).Start();
        }

        private void CollimatorVersion() {

            this.Dispatcher.Invoke(new Action(() => btnCollimatorVersion.IsEnabled = false));

            try {

                int galilVersion = 0;

                m_rcb.CollimatorVersion(out galilVersion);

                this.Dispatcher.Invoke(new Action(() => btnCollimatorVersion.Content = $"Ver: {galilVersion}"));
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnCollimatorVersion.IsEnabled = true));
        }

        private void btnHomeAperture_Click(object sender, RoutedEventArgs e) {

            new Thread(() => HomeAperture()).Start();
        }

        private void HomeAperture() {

            this.Dispatcher.Invoke(new Action(() => btnHomeAperture.IsEnabled = false));

            try {

                ushort result = 0;
                int ap1_position = m_ape1_position;
                int ap2_position = m_ape2_position;
                int flt_position = m_filt_position;

                //m_rcb.HomeAperture(out ap1_position, out ap2_position, out flt_position);

                m_rcb.HomeApertureAsync();

                do {

                    ap1_position = m_ape1_position;
                    ap2_position = m_ape2_position;

                    Thread.Sleep(2000);

                } while (ap1_position != m_ape1_position || ap2_position != m_ape2_position);

                if (!m_rcb.WaitHomeAperture(100, out result, out ap1_position, out ap2_position, out flt_position)) {

                    throw new Exception("response timed out");
                }

                if (result != 0) {

                    throw new Exception($"{result:X04}");
                }

                UpdateCollimatorPosition(ap1_position, ap2_position, flt_position);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnHomeAperture.IsEnabled = true));
        }

        private void btnHomeFilter_Click(object sender, RoutedEventArgs e) {

            new Thread(() => HomeFilter()).Start();
        }

        private void HomeFilter() {

            this.Dispatcher.Invoke(new Action(() => btnHomeFilter.IsEnabled = false));

            try {

                ushort result = 0;
                int ap1_position = m_ape1_position;
                int ap2_position = m_ape2_position;
                int flt_position = m_filt_position;

                //m_rcb.HomeFilter(out ap1_position, out ap2_position, out flt_position);

                m_rcb.HomeFilterAsync();

                do {

                    flt_position = m_filt_position;

                    Thread.Sleep(2000);

                } while (flt_position != m_filt_position);

                if (!m_rcb.WaitHomeFilter(100, out result, out ap1_position, out ap2_position, out flt_position)) {

                    throw new Exception("response timed out");
                }

                if (result != 0) {

                    throw new Exception($"{result:X04}");
                }

                UpdateCollimatorPosition(ap1_position, ap2_position, flt_position);
            }
            catch (Exception e) {

                MessageBox.Show(e.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnHomeFilter.IsEnabled = true));
        }

        private void btnMoveAperture_Click(object sender, RoutedEventArgs e) {

            try {

                int ap1_position = Convert.ToInt32(tbxAp1TargetPosition.Text);
                int ap2_position = Convert.ToInt32(tbxAp2TargetPosition.Text);
                bool ap1_selected = cbxAp1Pos.IsChecked == true;
                bool ap2_selected = cbxAp2Pos.IsChecked == true;

                new Thread(() => MoveAperture(ap1_selected, ap2_selected, ap1_position, ap2_position)).Start();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void MoveAperture(bool aperture1, bool aperture2, int position1, int position2) {

            this.Dispatcher.Invoke(new Action(() => btnMoveAperture.IsEnabled = false));

            try {

                ushort result = 0;
                int ap1_position = m_ape1_position;
                int ap2_position = m_ape2_position;
                int flt_position = m_filt_position;

                //m_rcb.MoveAperture(aperture1, aperture2, position1, position2, out ap1_position, out ap2_position, out flt_position);
                m_rcb.MoveApertureAsync(aperture1, aperture2, position1, position2);

                do {

                    ap1_position = m_ape1_position;
                    ap2_position = m_ape2_position;

                    Thread.Sleep(2000);

                } while (ap1_position != m_ape1_position || ap2_position != m_ape2_position);

                if (!m_rcb.WaitMoveAperture(100, out result, out ap1_position, out ap2_position, out flt_position)) {

                    throw new Exception("response timed out");
                }

                if (result != 0) {

                    throw new Exception($"{result:X04}");
                }

                UpdateCollimatorPosition(ap1_position, ap2_position, flt_position);
            }
            catch (Exception e) {

                MessageBox.Show(e.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnMoveAperture.IsEnabled = true));
        }

        private void btnMoveFilter_Click(object sender, RoutedEventArgs e) {

            try {

                int flt_position = Convert.ToInt32(tbxFilPosition.Text);

                new Thread(() => MoveFilter(flt_position)).Start();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void MoveFilter(int position) {

            this.Dispatcher.Invoke(new Action(() => btnMoveFilter.IsEnabled = false));

            try {

                ushort result = 0;
                int ap1_position = m_ape1_position;
                int ap2_position = m_ape2_position;
                int flt_position = m_filt_position;

                //m_rcb.MoveFilter(position, out ap1_position, out ap2_position, out flt_position);
                m_rcb.MoveFilterAsync(position);

                do {

                    flt_position = m_filt_position;

                    Thread.Sleep(2000);

                } while (flt_position != m_filt_position);

                if (!m_rcb.WaitMoveFilter(100, out result, out ap1_position, out ap2_position, out flt_position)) {

                    throw new Exception("response timed out");
                }

                if (result != 0) {

                    throw new Exception($"{result:X04}");
                }

                UpdateCollimatorPosition(ap1_position, ap2_position, flt_position);
            }
            catch (Exception e) {

                MessageBox.Show(e.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnMoveFilter.IsEnabled = true));
        }

        private void btnStopCollimator_Click(object sender, RoutedEventArgs e) {

            new Thread(() => StopCollimator()).Start();
        }

        private void StopCollimator() {

            this.Dispatcher.Invoke(new Action(() => btnStopCollimator.IsEnabled = false));

            try {

                m_rcb.StopCollimator();

                // update result
            }
            catch (Exception e) {

                MessageBox.Show(e.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnStopCollimator.IsEnabled = true));
        }

        private void btnCMSetApe1Speed_Click(object sender, RoutedEventArgs e) {

            try {

                int speed = Convert.ToInt32(tbxAp1Speed.Text);

                new Thread(() => SetApeture1Speed(speed)).Start();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCMSetApe2Speed_Click(object sender, RoutedEventArgs e) {

            try {

                int speed = Convert.ToInt32(tbxAp2Speed.Text);

                new Thread(() => SetApeture2Speed(speed)).Start();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCMSetFiltSpeed_Click(object sender, RoutedEventArgs e) {

            try {

                int speed = Convert.ToInt32(tbxFltSpeed.Text);

                new Thread(() => SetFilterSpeed(speed)).Start();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void SetApeture1Speed(int speed) {

            this.Dispatcher.Invoke(new Action(() => btnSetApe1Speed.IsEnabled = false));

            try {

                m_rcb.SetApeture1Speed(speed);

                // update result
            }
            catch (Exception e) {

                MessageBox.Show(e.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnSetApe1Speed.IsEnabled = true));
        }

        private void SetApeture2Speed(int speed) {

            this.Dispatcher.Invoke(new Action(() => btnSetApe2Speed.IsEnabled = false));

            try {

                m_rcb.SetApeture2Speed(speed);

                // update result
            }
            catch (Exception e) {

                MessageBox.Show(e.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnSetApe2Speed.IsEnabled = true));
        }

        private void SetFilterSpeed(int speed) {

            this.Dispatcher.Invoke(new Action(() => btnSetFiltSpeed.IsEnabled = false));

            try {

                m_rcb.SetFilterSpeed(speed);

                // update result
            }
            catch (Exception e) {

                MessageBox.Show(e.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnSetFiltSpeed.IsEnabled = true));
        }
        #endregion Collimator

        #region Data Acquisition
        private void btnDetConfigEnv_Click(object sender, RoutedEventArgs e) {

            try {

                Int32 sliceBoardBitMap = 0;
                foreach (CheckBox slice in m_adc_sliceboards) {

                    if (slice.IsChecked == true) {

                        sliceBoardBitMap |= 1 << m_adc_sliceboards.IndexOf(slice);
                    }
                }

                bool jtagEnabled = cbxEnableJTAG.IsChecked == true;
                bool compressionOff = cbxCompressionOff.IsChecked == true;
                bool slipRingNotPresent = cbxSlipRingNotPresent.IsChecked == true;
                bool rcbNotPresent = cbxRCBNotPresent.IsChecked == true;

                int argument = (sliceBoardBitMap << 4);
                argument |= jtagEnabled ? 1 << 31 : 0;
                argument |= compressionOff ? 1 << 2 : 0;
                argument |= rcbNotPresent ? 1 << 0 : 0;

                List<UInt32> request = new List<uint>() { RoterController.ConfigureEnvironment, (UInt32)argument, 0, 0, 0, 0, 0, 0 };

                List<UInt32> response;

                m_rcb.AggregatorControl(request, out response, 1500);

                DisplaySliceBoardReply(response);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnInitAdc_Click(object sender, RoutedEventArgs e) {

            try {

                List<uint> request = new List<uint>() { RoterController.DataAcquisition | RoterController.InitializeAdc, 0, 0, 0, 0, 0, 0, 0 };

                List<UInt32> response;

                m_rcb.AggregatorControl(request, out response, 1500);

                DisplaySliceBoardReply(response);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnInitDataAcq_Click(object sender, RoutedEventArgs e) {

            try {

                int errorRegisterReset = cdtErrorRegisterReset.SelectedIndex;
                int startSlice = Convert.ToInt16(tdtStartingSlice.Text);
                int endSlice = Convert.ToInt16(tdtEndingSlice.Text);
                int dataSource = cdtDataSource.SelectedIndex;

                int argument = (errorRegisterReset & 0x01) << 15 | (startSlice & 0x1f) << 6 | (endSlice & 0x1f) << 1 | (dataSource & 0x01) << 0;

                List<uint> request = new List<uint>() { RoterController.DataAcquisition | RoterController.InitializeDataAcquisition, (uint)argument, 0, 0, 0, 0, 0, 0 };

                List<UInt32> response;

                m_rcb.AggregatorControl(request, out response, 1500);

                DisplaySliceBoardReply(response);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnLinkTrain_Click(object sender, RoutedEventArgs e) {

            try {

                int channel1 = cdtChannel1.SelectedIndex == 0 ? 0 : cdtChannel1.SelectedIndex + 1;
                int channel2 = cdtChannel2.SelectedIndex == 0 ? 0 : cdtChannel2.SelectedIndex + 1;
                int channel3 = cdtChannel3.SelectedIndex == 0 ? 0 : cdtChannel3.SelectedIndex + 1;

                int argument = (channel1 & 0x03) << 4 | (channel2 & 0x03) << 2 | (channel3 & 0x03) << 0;

                List<uint> request = new List<uint>() { RoterController.DataAcquisition | RoterController.SlipRingLinkTrain, (uint)argument, 0, 0, 0, 0, 0, 0 };

                List<UInt32> response;

                m_rcb.AggregatorControl(request, out response, 1500);

                DisplaySliceBoardReply(response);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdcOn_Click(object sender, RoutedEventArgs e) {

            try {

                List<uint> request = new List<uint>() { RoterController.DataAcquisition | RoterController.AdcPowerSave, 1, 0, 0, 0, 0, 0, 0 };

                List<UInt32> response;

                m_rcb.AggregatorControl(request, out response, 1500);

                DisplaySliceBoardReply(response);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdcOff_Click(object sender, RoutedEventArgs e) {

            try {

                List<uint> request = new List<uint>() { RoterController.DataAcquisition | RoterController.AdcPowerSave, 0, 0, 0, 0, 0, 0, 0 };

                List<UInt32> response;

                m_rcb.AggregatorControl(request, out response, 1500);

                DisplaySliceBoardReply(response);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetIntegrationTime_Click(object sender, RoutedEventArgs e) {

            try {

                int inputSource = Convert.ToInt16(tdtInputSource.Text);
                int sampleMode = Convert.ToInt16(tdtSampleMode.Text);
                int decimation = Convert.ToInt16(tdtDecimation.Text);
                int clockSpeed = Convert.ToInt16(tdtClockSpeed.Text);
                int range = Convert.ToInt16(tdtRange.Text);
                int shutdownMode = cdtPostConversionShutdown.SelectedIndex;

                int argument = (inputSource & 0x0f) << 10 | (sampleMode & 0x01) << 9 | (decimation & 0x07) << 6;
                argument |= (clockSpeed & 0x03) << 4 | (range & 0x07) << 1 | (shutdownMode & 0x01) << 0;

                List<uint> request = new List<uint>() { RoterController.DataAcquisition | RoterController.SetIntegrationTime, (uint)argument, 0, 0, 0, 0, 0, 0 };

                List<UInt32> response;

                m_rcb.AggregatorControl(request, out response, 1500);

                DisplaySliceBoardReply(response);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnStartDataAcq_Click(object sender, RoutedEventArgs e) {

            try {

                int integrationAveraging = Convert.ToInt16(tdtIntegrationAveraging.Text);
                int detectorDataSource = cdtDetectorDataSource.SelectedIndex;
                int samples2Collect = Convert.ToInt16(tdtIntegrationLimit.Text);

                int argument = (integrationAveraging & 0x07) << 29 | (detectorDataSource & 0x07) << 26 | (samples2Collect & 0x00ffffff) << 2;

                List<uint> request = new List<uint>() { RoterController.DataAcquisition | RoterController.StartDataAcq, (uint)argument, 0, 0, 0, 0, 0, 0 };

                List<UInt32> response;

                m_rcb.AggregatorControl(request, out response, 5000);

                DisplaySliceBoardReply(response);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void bdtStopAcq_Click(object sender, RoutedEventArgs e) {

            try {

                List<uint> request = new List<uint>() { RoterController.DataAcquisition | RoterController.StopDataAcq, 0, 0, 0, 0, 0, 0, 0 };

                List<UInt32> response;

                m_rcb.AggregatorControl(request, out response, 1500);

                DisplaySliceBoardReply(response);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDetNop_Click(object sender, RoutedEventArgs e) {

            try {

                List<uint> request = new List<uint>() { RoterController.DataAcquisition | RoterController.DacNop, 0, 0, 0, 0, 0, 0, 0 };

                List<UInt32> response;

                m_rcb.AggregatorControl(request, out response, 1500);

                DisplaySliceBoardReply(response);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnGetSliceVersion_Click(object sender, RoutedEventArgs e) {

            try {

                int sliceNumber = cdtGetSliceVersion.SelectedIndex;

                List<uint> request = new List<uint>() { RoterController.GetSliceVersion | (uint)sliceNumber << 20, 0, 0, 0, 0, 0, 0, 0 };

                List<UInt32> response;

                m_rcb.AggregatorControl(request, out response, 1500);

                DisplaySliceBoardReply(response);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnTurnOnEncoderFlow_Click(object sender, RoutedEventArgs e) {

        }

        private void btnTurnOffEncoderFlow_Click(object sender, RoutedEventArgs e) {

        }

        private void btnDetClearDDResult_Click(object sender, RoutedEventArgs e) {

            tbxDetDiagResult.Clear();
        }

        private void btnDetDiagnostic_Click(object sender, RoutedEventArgs e) {

            try {

                List<uint> request = new List<uint>() { RoterController.DataAcquisition | RoterController.DetectorDiagnostics, 1, 0x0f, 0, 0, 0, 0, 0 };
                List<UInt32> response;
                int highlight = 0;

                m_rcb.AggregatorControl(request, out response, 15000);

                if (response[0] != RoterController.DetectorDiagnostics + 1) {  // NACK

                    tbxDetDiagResult.AppendText("NACK received\n");
                    highlight |= 0x01;

                    if (response[0] == RoterController.DAQ_SET_INT_TIME_FAILED) {

                        tbxDetDiagResult.AppendText("DAQ_SET_INT_TIME_FAILED\n");
                    }
                    else if (response[0] == RoterController.ADC_INIT_FAILED) {

                        tbxDetDiagResult.AppendText("ADC_INIT_FAILED\n");
                    }
                    else if (response[0] == RoterController.DAQ_INIT_FAILED) {

                        tbxDetDiagResult.AppendText("DAQ_INIT_FAILED\n");
                    }
                    else if (response[0] == RoterController.ADC_PWR_SAVE_FAILED) {

                        tbxDetDiagResult.AppendText("ADC_PWR_SAVE_FAILED\n");
                    }
                    else if (response[0] == RoterController.REG_TEST_FAILED) {

                        tbxDetDiagResult.AppendText("REG_TEST_FAILED\n");
                    }
                    else if (response[0] == RoterController.DATA_TEST_FAILED) {

                        tbxDetDiagResult.AppendText("DATA_TEST_FAILED\n");
                    }
                    else if (response[0] == RoterController.DAQ_START_FAILED) {

                        tbxDetDiagResult.AppendText("DAQ_START_FAILED\n");
                    }
                    else if (response[0] == RoterController.DAQ_STOP_FAILED) {

                        tbxDetDiagResult.AppendText("DAQ_STOP_FAILED\n");
                    }
                    else if (response[0] == RoterController.SLICE_LINK_FAILED) {

                        tbxDetDiagResult.AppendText("SLICE_LINK_FAILED\n");
                    }
                    else if (response[0] == RoterController.DAQ_REXMIT_WAIT_TIMEOUT) {

                        tbxDetDiagResult.AppendText("DAQ_REXMIT_WAIT_TIMEOUT\n");
                    }
                    else if (response[0] == RoterController.DAQ_REXMIT_OVERFLOW) {

                        tbxDetDiagResult.AppendText("DAQ_REXMIT_OVERFLOW\n");
                    }
                }

                // Check Word#8
                if ((response[1] & 1) != 0) {   // Start Error

                    tbxDetDiagResult.AppendText("Start Error\n");
                    tbxDetDiagResult.AppendText($"ACQ Ctrl Reg: {response[7] & 0xffff:X04}(L), {(response[6] >> 16) & 0xffff:x04}(H)\n");
                    highlight |= 0x82;
                }
                else {  // Link Drop Status

                    if (response[7] != 0) {

                        tbxDetDiagResult.AppendText($"Link Drop Status: {response[6]:X08}\n");
                        if ((response[7] & 0x8000000) != 0) {

                            tbxDetDiagResult.AppendText($"RCB Link Drop\n");
                        }

                        if ((response[7] & 0x7ffffff) != 0) {

                            tbxDetDiagResult.AppendText($"Slice Link Drop: {response[7]:X08}\n");
                        }

                        highlight |= 0x82;
                    }
                }

                if ((response[1] & 4) != 0 && (response[1] & 2) == 0) {

                    tbxDetDiagResult.AppendText($"Data Expected: {response[5]:X08}\n");   // Word#6
                    tbxDetDiagResult.AppendText($"Data Received: {response[6]:X08}\n");   // Word#7
                }
                else {

                    tbxDetDiagResult.AppendText($"Samples: {(response[5] - 2) / 65}\n");  // Word#6
                    tbxDetDiagResult.AppendText($"Short Clock: {response[6] * 0.02} us\n");   // Workd#7
                }

                tbxDetDiagResult.ScrollToEnd();

                DisplaySliceBoardReply(response, highlight);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void DisplaySliceBoardReply(List<uint> response, int highlight = 0) {

            for (int i = 0 ; i < response.Count ; i++) {

                if ((highlight & (1 << i)) != 0) {

                    m_tbx_agg_reply[i].Foreground = Brushes.Red;
                }

                m_tbx_agg_reply[i].FontWeight = FontWeights.Bold;
                m_tbx_agg_reply[i].Text = $"{response[i]:X8}";
            }
        }
        #endregion Data Acquisition

        #region Thermal Control
        private void ThermalControl(byte command, List<uint> argument, int timeout) {

            if (argument == null) {

                argument = new List<uint>() { 0, 0, 0, 0, 0, 0, 0 };
            }

            if (argument.Count != 7) {

                throw new Exception("argument length should be 7 32-bit words");
            }

            foreach (ThermalSliceBoard sliceBoard in m_thermal_sliceboards) {

                if (sliceBoard.Selection.IsChecked == true) {

                    List<UInt32> request = new List<uint>();
                    request.Add(sliceBoard.Address | command);
                    request.AddRange(argument);

                    ushort errcode = 0;
                    List<UInt32> response = null;

                    //m_rcb.AggregatorControl(request, out errcode, out response, timeout);

                    sliceBoard.Result.Text = $"{errcode:X8}";

                    if (errcode != 0) {

                        sliceBoard.Result.Foreground = new SolidColorBrush(Colors.Red);
                        sliceBoard.Response.Text = "";
                    }
                    else {

                        sliceBoard.Response.Foreground = new SolidColorBrush(Colors.Black);
                        for (int i = 0 ; i < response.Count ; i++) {

                            sliceBoard.Response.Text += $"{response[i]:X8}";

                            if (i != response.Count - 1) {

                                sliceBoard.Response.Text += ",";
                            }
                        }
                    }
                }
            }
        }

        private void btnReadRailTemperature_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadRailTemperature, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadErrorCode_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadThermalErrorCode, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadSetPoint_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadSetPoint, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnRead18VMonitor_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.Read18VMonitor, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnRead25VMonitor_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.Read25VMonitor, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadMinus3VMonitor_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadMinus3VMonitor, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnRead3VMonitor_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.Read3VMonitor, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadHeaterVoltageDetector_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadHeaterVoltageDetector, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadEnableInputState_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadEnableInputState, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadProportionalPIDConstant_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadProportionalPIDConstant, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadIntegralPIDConstant_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadIntegralPIDConstant, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadDerivativePIDConstant_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadDerivativePIDConstant, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadAutoTuneStepSize_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadAutoTuneStepSize, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadAutoTuneNoiseValue_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadAutoTuneNoiseValue, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadAutoTuneStartValue_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadAutoTuneStartValue, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadAutoTuneLookbackSeconds_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadAutoTuneLookbackSeconds, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadSetpointA2DTargetValue_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadSetpointA2DTargetValue, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadThermistorA2DInputValue_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadThermistorA2DInputValue, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadHeaterPWMOutputValue_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ReadHeaterPWMOutputValue, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeTemperatureSetpoint_Click(object sender, RoutedEventArgs e) {

            try {

                uint value = Convert.ToUInt32(tbxChangeTemperatureSetpoint.Text);

                List<uint> argument = new List<uint>() { value, 0, 0, 0, 0, 0, 0, 0 };

                ThermalControl(RoterController.ChangeTemperatureSetpoint, argument, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeProportionalPIDConstant_Click(object sender, RoutedEventArgs e) {

            try {

                uint value = Convert.ToUInt32(tbxChangeProportionalPIDConstant.Text);

                List<uint> argument = new List<uint>() { value, 0, 0, 0, 0, 0, 0, 0 };

                ThermalControl(RoterController.ChangeProportionalPIDConstant, argument, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeIntegralPIDConstant_Click(object sender, RoutedEventArgs e) {

            try {

                uint value = Convert.ToUInt32(tbxChangeIntegralPIDConstant.Text);

                List<uint> argument = new List<uint>() { value, 0, 0, 0, 0, 0, 0, 0 };

                ThermalControl(RoterController.ChangeIntegralPIDConstant, argument, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeDerivativePIDConstant_Click(object sender, RoutedEventArgs e) {

            try {

                uint value = Convert.ToUInt32(tbxChangeDerivativePIDConstant.Text);

                List<uint> argument = new List<uint>() { value, 0, 0, 0, 0, 0, 0, 0 };

                ThermalControl(RoterController.ChangeDerivativePIDConstant, argument, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeAutoTuneStepSize_Click(object sender, RoutedEventArgs e) {

            try {

                uint value = Convert.ToUInt32(tbxChangeAutoTuneStepSize.Text);

                List<uint> argument = new List<uint>() { value, 0, 0, 0, 0, 0, 0, 0 };

                ThermalControl(RoterController.ChangeAutoTuneStepSize, argument, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeAutoTuneNoiseValue_Click(object sender, RoutedEventArgs e) {

            try {

                uint value = Convert.ToUInt32(tbxChangeAutoTuneNoiseValue.Text);

                List<uint> argument = new List<uint>() { value, 0, 0, 0, 0, 0, 0, 0 };

                ThermalControl(RoterController.ChangeAutoTuneNoiseValue, argument, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeAutoTuneStartValue_Click(object sender, RoutedEventArgs e) {

            try {

                uint value = Convert.ToUInt32(tbxChangeAutoTuneStartValue.Text);

                List<uint> argument = new List<uint>() { value, 0, 0, 0, 0, 0, 0, 0 };

                ThermalControl(RoterController.ChangeAutoTuneStartValue, argument, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeAutoTuneLookbackSeconds_Click(object sender, RoutedEventArgs e) {

            try {

                uint value = Convert.ToUInt32(tbxChangeAutoTuneLookbackSec.Text);

                List<uint> argument = new List<uint>() { value, 0, 0, 0, 0, 0, 0, 0 };

                ThermalControl(RoterController.ChangeAutoTuneLookbackSeconds, argument, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetAutoTuneMode_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.StartAutoTuneMode, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelAutoTuneMode_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.CancelAutoTuneMode, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnThermalNop_Click(object sender, RoutedEventArgs e) {

            try {

                ThermalControl(RoterController.ThermalNop, null, 1500);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }
        #endregion Thermal Control

        #region Fan Control
        private void FanControl(byte command, List<uint> argument) {

            if (argument == null) {

                argument = new List<uint>() { 0, 0, 0, 0, 0, 0, 0 };
            }

            if (argument.Count != 7) {

                throw new Exception("argument length should be 7 32-bit words");
            }

            List<UInt32> request = new List<uint>();
            request.Add(RoterController.FanControl | command);
            request.AddRange(argument);

            ushort errcode = 0;
            List<UInt32> response = null;

            //m_rcb.AggregatorControl(request, out errcode, out response, 1500);

            tbxFanResult.Text = $"{errcode:X8}";

            if (errcode != 0) {

                tbxFanResult.Foreground = new SolidColorBrush(Colors.Red);
                tbxFanResponse.Text = "";
            }
            else {

                tbxFanResult.Foreground = new SolidColorBrush(Colors.Black);
                for (int i = 0 ; i < response.Count ; i++) {

                    tbxFanResponse.Text += $"{response[i]:X8}";

                    if (i != response.Count - 1) {

                        tbxFanResponse.Text += ",";
                    }
                }
            }
        }

        private void btnReadFanErrorCode_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadFanErrorCode, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadDetectorAmbientTemp_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadDetectorAmbientTemp, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadOtherTemperature1_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadOtherTemperature1, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadOtherTemperature2_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadOtherTemperature2, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadOtherTemperature3_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadOtherTemperature3, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadRPM1_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadRPM1, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadRPM2_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadRPM2, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadRPM3_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadRPM3, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadRPM4_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadRPM4, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadRPM5_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadRPM5, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadRPM6_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadRPM6, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadRPM7_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadRPM7, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadCurrentFanPWM_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadCurrentFanPWM, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadInitialFanSpeedSetting_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadInitialFanSpeedSetting, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadModeSetting_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadModeSetting, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadLastManualFanPWM_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadLastManualFanPWM, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadFanProgramName_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadFanProgramName, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadFanProgramVersion_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ReadFanProgramVersion, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetFanPWM_Click(object sender, RoutedEventArgs e) {

            try {

                uint value = Convert.ToUInt32(tbxSetFanPWM.Text);

                List<uint> argument = new List<uint>() { value, 0, 0, 0, 0, 0, 0, 0 };

                FanControl(RoterController.SetFanPWM, argument);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeInitialFanSpeed_Click(object sender, RoutedEventArgs e) {

            try {

                uint value = Convert.ToUInt32(tbxChangeInitialFanSpeed.Text);

                List<uint> argument = new List<uint>() { value, 0, 0, 0, 0, 0, 0, 0 };

                FanControl(RoterController.ChangeInitialFanSpeed, argument);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetManualMode_Click(object sender, RoutedEventArgs e) {

            try {

                uint value = Convert.ToUInt32(tbxSetManualMode.Text);

                List<uint> argument = new List<uint>() { value, 0, 0, 0, 0, 0, 0, 0 };

                FanControl(RoterController.SetManualMode, argument);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetAutoMode_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.SetAutoMode, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveCurrentParameters_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.SaveCurrentPamarameters, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnResetFanErrorLog_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.ResetFanErrorLog, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnFanNop_Click(object sender, RoutedEventArgs e) {

            try {

                FanControl(RoterController.FanNop, null);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        #endregion Fan Control

        #region FMI Messaging
        private void btnRegisterYellowPage_Click(object sender, RoutedEventArgs e) {
            //btnRegister.IsEnabled = false;
            try {

                if (m_messaging_used)
                    return;

                cFramework.gProgramName = "RCBTool";
                cFramework.gMessagingName = "RCBTool";
                cFramework.gVersionInfo = "";
                cFramework.gProgramDescription = "Engineering Tool for RCB Development";

                DefineMessageSupport();

                if (cFramework.InitializeMessaging() == 0) {

                    btnRegisterYellowPage.IsEnabled = false;

                    cFramework.ClearLegacyMode();

                    SetupConsumerResources();

                    DefineTimerSupport();

                    m_messaging_used = true;
                }
                else {

                    m_messaging_used = false;
                    throw new Exception("Unable to register with YellowPage.");
                }
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void DefineMessageSupport() {

            cFramework.gSupportedMessages.Add(new cMessage("STATUS", "Get the status of a resource", null, ProcessStatus, true));
            cFramework.gSupportedMessages.Add(new cMessage("XRAYLIGHT", "Response to XRAYLIGHT Request", null, ProcessXrayLightResponse));
        }

        private void SetupConsumerResources() {

            if (0 != cFramework.AutoscribeRegister(gkResDoor, gkResDoor, "safe", "enhanced")) {

                new Exception($"Unable to autoscribe to '{gkResDoor}' resource;");
            }

            if (0 != cFramework.AutoscribeRegister(gkResMainContactor, gkResMainContactor, "safe", "enhanced")) {

                new Exception($"Unable to autoscribe to '{gkResMainContactor}' resource;");
            }
        }

        private void SetupProviderResources() {

            const int kSuccess = 0;

            if (kSuccess != cFramework.AutoscribeAddFilter(gkResState, "state=Init")) {

                throw new Exception($"Unable to setup '{gkResState}' resource");
            }
            Thread.Sleep(200);

            if (kSuccess != cFramework.AutoscribeAddFilter(gkResTubeHeat, "TUBE_HEAT=0.00 %")) {

                throw new Exception($"Unable to setup '{gkResTubeHeat}' resource");
            }
            Thread.Sleep(200);

            if (kSuccess != cFramework.AutoscribeEnable(250)) {

                throw new Exception("Unable to enable providing resources. Features Disabled!");
            }
        }

        private void DefineTimerSupport() {

            cFramework.gSupportedTimers.Add(new cTimer("RequestTimer", new EventHandler(RequestTimer_Tick), new TimeSpan(0, 0, 0, 0, 250), "Main Request Timer", true));

            cFramework.SetupTimers();
        }

        private void RequestTimer_Tick(object sender, EventArgs e) {

            cRequest myRequest;

            while (cFramework.gRequestQueue.Count > 0) {

                myRequest = cFramework.GrabRequest();

                // Based on the request "Command", dispatch to the appropriate handling function.

                bool myMessageHandledFlag = false;

                foreach (cMessage msg in cFramework.gSupportedMessages) {

                    if (msg.Command == myRequest.Command) {

                        //GUIVariables.LastMessage = msg.Command;

                        msg.Handler(myRequest);

                        myMessageHandledFlag = true;

                        break;
                    }
                }

                if (!myMessageHandledFlag) {

                    cFramework.ProcessInvalidRequest(myRequest);
                }
            }
        }

        private void ProcessStatus(cRequest pRequest) {

            string myValue;

            try {

                if (!pRequest.PriorityRequest)
                    return;

                string myResourceId;
                if (pRequest.GrabParameter("rid", out myResourceId)) {

                    switch (myResourceId.ToLower()) {

                    case gkResDoor:
                        pRequest.GrabParameter("value", out myValue);

                        switch (myValue.ToLower()) {

                        case "open":

                            iconDoor.Style = (Style)Application.Current.Resources["FMI_DoorOpen_Button"];
                            lblDoor.Content = "open";
                            m_rcb.NotifyDoorStatus(false);
                            break;

                        case "closed":

                            iconDoor.Style = (Style)Application.Current.Resources["FMI_DoorClosed_Button"];
                            lblDoor.Content = "closed";
                            m_rcb.NotifyDoorStatus(true);
                            break;

                        default:

                            iconDoor.Style = (Style)Application.Current.Resources["FMI_DoorOpen_Button"];
                            lblDoor.Content = "unknown";
                            m_rcb.NotifyDoorStatus(false);
                            break;
                        }

                        break;
                    }
                }
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void ProcessXrayLightResponse(cRequest pRequest) {

        }
        #endregion FMI Messaging

        #region RCB Diagnostics
        private void btnDiagDIO_Click(object sender, RoutedEventArgs e) {

            new Thread(() => DiagnoseDigitalIO()).Start();
        }

        private void DiagnoseDigitalIO() {

            this.Dispatcher.Invoke(new Action(() => btnDiagDIO.IsEnabled = false));

            try {

                m_rcb.DiagnoseDigitalIO();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnDiagDIO.IsEnabled = true));
        }

        private void btnDiagRS232_Click(object sender, RoutedEventArgs e) {

            try {

                string rx = "";

                tbxRS232Rx.Clear();

                m_rcb.DiagnoseRS232(tbxRS232Tx.Text, out rx);

                tbxRS232Rx.Text = rx;
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDiagClock_Click(object sender, RoutedEventArgs e) {

            try {

                m_rcb.DiagnoseClock();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDiagSFP_Click(object sender, RoutedEventArgs e) {

            List<uint> tx = new List<uint>();
            List<uint> rx;

            try {

                tx.Add(Convert.ToUInt32(tbxSFPTx0.Text, 16));
                tx.Add(Convert.ToUInt32(tbxSFPTx1.Text, 16));
                tx.Add(Convert.ToUInt32(tbxSFPTx2.Text, 16));
                tx.Add(Convert.ToUInt32(tbxSFPTx3.Text, 16));
                tx.Add(Convert.ToUInt32(tbxSFPTx4.Text, 16));
                tx.Add(Convert.ToUInt32(tbxSFPTx5.Text, 16));
                tx.Add(Convert.ToUInt32(tbxSFPTx6.Text, 16));
                tx.Add(Convert.ToUInt32(tbxSFPTx7.Text, 16));

                tbxSFPRx0.Clear();
                tbxSFPRx1.Clear();
                tbxSFPRx2.Clear();
                tbxSFPRx3.Clear();
                tbxSFPRx4.Clear();
                tbxSFPRx5.Clear();
                tbxSFPRx6.Clear();
                tbxSFPRx7.Clear();

                m_rcb.DiagnoseSFP(tx, out rx);

                tbxSFPRx0.Text = $"{rx[0]:X8}";
                tbxSFPRx1.Text = $"{rx[1]:X8}";
                tbxSFPRx2.Text = $"{rx[2]:X8}";
                tbxSFPRx3.Text = $"{rx[3]:X8}";
                tbxSFPRx4.Text = $"{rx[4]:X8}";
                tbxSFPRx5.Text = $"{rx[5]:X8}";
                tbxSFPRx6.Text = $"{rx[6]:X8}";
                tbxSFPRx7.Text = $"{rx[7]:X8}";
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDiagLaser_Click(object sender, RoutedEventArgs e) {

            new Thread(() => DiagnoseLaser()).Start();
        }

        private void DiagnoseLaser() {

            this.Dispatcher.Invoke(new Action(() => btnDiagLaser.IsEnabled = false));

            try {

                m_rcb.DiagnoseLaser();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnDiagLaser.IsEnabled = true));
        }

        private void btnRS485_Click(object sender, RoutedEventArgs e) {

            new Thread(() => DiagnoseRS485()).Start();
        }

        private void DiagnoseRS485() {

            this.Dispatcher.Invoke(new Action(() => btnDiagRS485.IsEnabled = false));

            try {

                m_rcb.DiagnoseRS485();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => btnDiagRS485.IsEnabled = true));
        }

        private void btnDiagEthernet_Click(object sender, RoutedEventArgs e) {

            try {

                m_rcb.DiagnoseEthernet();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDiagCAN2_Click(object sender, RoutedEventArgs e) {

            try {

                m_rcb.DiagnoseCAN2();
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDiagAbort_Click(object sender, RoutedEventArgs e) {

            m_rcb.AbortDiagnosis();
        }

        private void ReadGBoxCanBus() {

            Peak.Can.Basic.TPCANMsg raw_frame;
            string message = "";

            while (m_thread_run) {

                m_gbox_canbus.DequeueRx(out raw_frame);

                for (int i = 0 ; i < raw_frame.LEN ; i++) {

                    message += (char)raw_frame.DATA[i];
                }

                message += Environment.NewLine;

                this.Dispatcher.Invoke(new Action(() => tbxCAN2Rx.AppendText(message)));
                this.Dispatcher.Invoke(new Action(() => tbxCAN2Rx.ScrollToEnd()));

                message = "";
            }
        }

        #endregion RCB Diagnostics
    }
}