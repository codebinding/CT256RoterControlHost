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
using System.Collections.Concurrent;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media;
using System.Text;
using System.Threading;

using RoterControlSupport;

namespace RCBUploader {

    struct FileTransmit {

        public string LocalFile;
        public string RemoteFile;
        public uint Permission;
    }

    public partial class MainWindow : Window {

        private RoterController m_rcb = null;
        private bool m_thread_run = false;
        private Thread m_thread_process_notification = null;

        private bool m_rcb_connected = false;

        public MainWindow() {

            InitializeComponent();

            AddControls();

            Initialize();
        }

        private void Main_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

            if (m_rcb_connected) {

                m_rcb.StopRcbServiceAsync();
            }

            if (m_thread_process_notification != null) {

                m_thread_process_notification.Abort();
                m_thread_process_notification.Join();
            }
        }

        private void AddControls() {

        }

        private void Initialize() {

            pgbFileTransProgress.Visibility = Visibility.Hidden;
            tblFileTransProgress.Visibility = Visibility.Hidden;
        }

        private void ProcessNotification() {

            Notification notification = null;

            while (m_thread_run) {

                notification = m_rcb.TakeNotification();

                ulong value = notification.Data64;

                switch ((ushort)notification.CommandBits) {

                case RoterController.NTF_FILEPROGRESS:

                    UpdateTransmissionProgress(value);
                    break;
                }
            }
        }

        #region House Keeping
        private void btnConnect_Click(object sender, RoutedEventArgs e) {

            try {

                int device_id = 0x1a;

                if (m_rcb == null) {

                    m_rcb = new RoterController(device_id);
                }

                m_rcb.SyncTime();

                m_thread_run = true;

                m_thread_process_notification = new Thread(() => ProcessNotification());
                m_thread_process_notification.IsBackground = true;
                m_thread_process_notification.Start();

                btnConnect.IsEnabled = false;
                btnUpdateBootloader.IsEnabled = true;
                btnUpdateFirmwareFPGA.IsEnabled = true;
                btnUpdateFirmwareHPS.IsEnabled = true;

                m_rcb_connected = true;
            }
            catch (FormatException) {

                MessageBox.Show("Device Id has bad format");
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateBootloader_Click(object sender, RoutedEventArgs e) {

            OpenFileDialog open_dialog = new OpenFileDialog();

            if (open_dialog.ShowDialog() == true) {

                try {

                    FileTransmit ft = new FileTransmit();

                    ft.Permission = Convert.ToUInt32("0755", 8);
                    ft.LocalFile = open_dialog.FileName;
                    ft.RemoteFile = "/home/root/acadia";

                    //new Thread(() => TransmitFiles(sender, new List<FileTransmit>() { ft })).Start();
                }
                catch (Exception ex) {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUpdateFirmwareFPGA_Click(object sender, RoutedEventArgs e) {

            OpenFileDialog open_dialog = new OpenFileDialog();

            if (open_dialog.ShowDialog() == true) {

                new Thread(() => UpdateFirmwareFPGA(sender, open_dialog.FileName)).Start();
            }
        }

        private void UpdateFirmwareFPGA(object sender, string p_file_path) {

            Button button = (sender as Button);
            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = false));

            try {

                m_rcb.UpdateFirmwareFPGA(p_file_path);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = true));
        }

        private void btnUpdateFirmwareHPS_Click(object sender, RoutedEventArgs e) {

            OpenFileDialog open_dialog = new OpenFileDialog();

            if (open_dialog.ShowDialog() == true) {

                new Thread(() => UpdateFirmwareHPS(sender, open_dialog.FileName)).Start();
            }
        }

        private void UpdateFirmwareHPS(object sender, string p_file_path) {

            Button button = (sender as Button);
            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = false));

            try {

                m_rcb.UpdateFirmwareHPS(p_file_path);
            }
            catch(Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = true));
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
    }
}