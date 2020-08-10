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
        }

        private void AddControls() {

        }

        private void Initialize() {

            pgbFileTransProgress.Visibility = Visibility.Hidden;
            tblFileTransProgress.Visibility = Visibility.Hidden;
        }

        #region House Keeping
        private void btnConnect_Click(object sender, RoutedEventArgs e) {

            try {

                int device_id = 0x1a;

                if (m_rcb == null) {

                    m_rcb = new RoterController(device_id);
                }

                m_rcb.SyncTime();

                btnConnect.IsEnabled = false;
                btnUploadBootloader.IsEnabled = true;
                btnUploadFPGAFW.IsEnabled = true;
                btnUpoadHPSSW.IsEnabled = true;

                m_rcb_connected = true;
            }
            catch (FormatException) {

                MessageBox.Show("Device Id has bad format");
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnUploadBootloader_Click(object sender, RoutedEventArgs e) {

            OpenFileDialog open_dialog = new OpenFileDialog();

            if (open_dialog.ShowDialog() == true) {

                try {

                    FileTransmit ft = new FileTransmit();

                    ft.Permission = Convert.ToUInt32("0755", 8);
                    ft.LocalFile = open_dialog.FileName;
                    ft.RemoteFile = "/home/root/acadia";

                    new Thread(() => TransmitFiles(sender, new List<FileTransmit>() { ft })).Start();
                }
                catch (Exception ex) {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUploadFPGAFW_Click(object sender, RoutedEventArgs e) {

            OpenFileDialog open_dialog = new OpenFileDialog();

            if (open_dialog.ShowDialog() == true) {

                try {

                    FileTransmit ft = new FileTransmit();

                    ft.Permission = Convert.ToUInt32("0644", 8);
                    ft.LocalFile = open_dialog.FileName;
                    ft.RemoteFile = "/home/root/boot/soc_system.rbf";

                    new Thread(() => TransmitFPGA(sender, new List<FileTransmit>() { ft })).Start();
                }
                catch (Exception ex) {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUploadHPSSW_Click(object sender, RoutedEventArgs e) {

            OpenFileDialog open_dialog = new OpenFileDialog();

            if (open_dialog.ShowDialog() == true) {

                new Thread(() => UploadHPSW(sender, open_dialog.FileName)).Start();
            }
        }

        private void UploadHPSW(object sender, string p_file_path) {

            Button button = (sender as Button);
            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = false));

            try {

                m_rcb.UpdateHPS(p_file_path);
            }
            catch(Exception ex) {

                MessageBox.Show(ex.Message);
            }

            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = true));
        }

        private void TransmitFile(FileTransmit p_file_transmit, long p_transmitted_size, long p_total_size) {

            using (FileStream file_stream = new FileStream(p_file_transmit.LocalFile, FileMode.Open, FileAccess.Read)) {

                long file_size = file_stream.Length;

                m_rcb.TransmitFileInfo(file_size, p_file_transmit.Permission, p_file_transmit.RemoteFile);

                byte[] block = new byte[RoterController.FileTransmitBlockSize];

                int bytes_read = 0;
                long bytes_sent = p_transmitted_size;

                List<ulong> request = new List<ulong>();

                while ((bytes_read = file_stream.Read(block, 0, block.Length)) > 0) {

                    request.Clear();

                    for (int offset = 0 ; offset < bytes_read ; offset += 8) {

                        request.Add(BitConverter.ToUInt64(block, offset));
                    }

                    m_rcb.TransmitFileContent(request, bytes_read);

                    bytes_sent += bytes_read;

                    UpdateTransmissionProgress((double)bytes_sent * 100.0 / (double)p_total_size);
                }
            }
        }

        private void TransmitFiles(object sender, List<FileTransmit> p_file_list) {

            Button button = (sender as Button);

            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = false));

            try {

                long total_file_size = 0;

                foreach (FileTransmit ft in p_file_list) {

                    FileInfo file_info = new FileInfo(ft.LocalFile);
                    total_file_size += file_info.Length;
                }

                long transmitted_size = 0;

                foreach (FileTransmit ft in p_file_list) {

                    TransmitFile(ft, transmitted_size, total_file_size);

                    FileInfo file_info = new FileInfo(ft.LocalFile);
                    transmitted_size += file_info.Length;
                }
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            UpdateTransmissionProgress(100);

            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = true));
        }

        private void TransmitFPGA(object sender, List<FileTransmit> p_file_list) {

            Button button = (sender as Button);

            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = false));

            try {

                m_rcb.MountFat();

                long total_file_size = 0;

                foreach (FileTransmit ft in p_file_list) {

                    FileInfo file_info = new FileInfo(ft.LocalFile);
                    total_file_size += file_info.Length;
                }

                long transmitted_size = 0;

                foreach (FileTransmit ft in p_file_list) {

                    TransmitFile(ft, transmitted_size, total_file_size);

                    FileInfo file_info = new FileInfo(ft.LocalFile);
                    transmitted_size += file_info.Length;
                }

                m_rcb.UmountFat();
            }
            catch (Exception ex) {

                m_rcb.UmountFatAsync();

                MessageBox.Show(ex.Message);
            }

            UpdateTransmissionProgress(100);

            this.Dispatcher.Invoke(new Action(() => button.IsEnabled = true));
        }

        private void UpdateTransmissionProgress(double status) {

            if (status == 0 || status == 100) {

                this.Dispatcher.Invoke(new Action(() => pgbFileTransProgress.Visibility = Visibility.Hidden));
                this.Dispatcher.Invoke(new Action(() => tblFileTransProgress.Visibility = Visibility.Hidden));
            }
            else {

                this.Dispatcher.Invoke(new Action(() => pgbFileTransProgress.Visibility = Visibility.Visible));
                this.Dispatcher.Invoke(new Action(() => pgbFileTransProgress.Value = status));
                this.Dispatcher.Invoke(new Action(() => tblFileTransProgress.Visibility = Visibility.Visible));
                this.Dispatcher.Invoke(new Action(() => tblFileTransProgress.Text = $"{(status / 100.0):P0}"));
            }
        }
        #endregion House Keeping
    }
}