using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;

using Peak.Can.Basic;

namespace RoterControlSupport
{
    public class RoterController {

        #region RCB Commands
        // Broadcast Notification (0x00)
        public const ushort NTF_DOOR = 0x0001;
        public const ushort NTF_CSTEP = 0x0002;
        public const ushort NTF_MSTEP = 0x0003;
        public const ushort NTF_RESETTPOS = 0x0004;

        public const ushort NTF_RCBSTATE = 0x0040;
        public const ushort NTF_XMGRSTATE = 0x0041;
        public const ushort NTF_XRAYON = 0x0042;
        public const ushort NTF_TUBEHEAT = 0x0043;
        public const ushort NTF_WARMUP = 0x0044;
        public const ushort NTF_SEASONING = 0x0045;
        public const ushort NTF_FILCAL = 0x0046;
        public const ushort NTF_XSTEP = 0x0047;

        public const ushort NTF_AP1POS = 0x004a;
        public const ushort NTF_AP2POS = 0x004b;
        public const ushort NTF_FLTPOS = 0x004c;

        public const ushort NTF_LOG = 0x0050;

        // Acadia System (0x05)
        public const ushort CMD_SYNCTIME = 0x0500;
        public const ushort CMD_STOPBRYCE = 0x0501;
        public const ushort CMD_STARTBRYCE = 0x0502;
        public const ushort CMD_SHUTDOWNOS = 0x0503;
        public const ushort CMD_RESTARTOS = 0x0504;
        public const ushort CMD_STOPDENALI = 0x0505;
        public const ushort CMD_STARTDENALI = 0x0506;

        // File Transfer (0x06)
        public const ushort CMD_TXFILEINFO = 0x0601;
        public const ushort CMD_TXFILECONTENT = 0x0602;
        public const ushort CMD_RXFILEINFO = 0x0603;
        public const ushort CMD_RXFILECONTENT = 0x0604;
        public const ushort CMD_ABORTTXRX = 0x0605;
        public const ushort CMD_SCANDIR = 0x0606;
        public const ushort CMD_DELETEFILES = 0x0607;
        public const ushort CMD_MOUNTFAT = 0x0608;
        public const ushort CMD_UMOUNTFAT = 0x0609;

        // Bryce System (0x10)
        public const ushort CMD_SETLOGLEVEL_B = 0x1041;
        public const ushort CMD_TURNOFFLOGOUTPUT_B = 0x1042;
        public const ushort CMD_TURNONFILEOUTPUT_B = 0x1043;
        public const ushort CMD_TURNONSTDOUTPUT_B = 0x1044;
        public const ushort CMD_TURNONCANOUTPUT_B = 0x1045;

        // Denali System (0x0a)
        public const ushort CMD_SETLOGLEVEL_D = 0x0a41;
        public const ushort CMD_TURNOFFLOGOUTPUT_D = 0x0a42;
        public const ushort CMD_TURNONFILEOUTPUT_D = 0x0a43;
        public const ushort CMD_TURNONSTDOUTPUT_D = 0x0a44;
        public const ushort CMD_TURNONCANOUTPUT_D = 0x0a45;

        // Denali Diagnostic (0x0b)
        public const ushort CMD_READREGISTER = 0x0b01;
        public const ushort CMD_WRITEREGISTER = 0x0b02;
        public const ushort CMD_DIAG_ABORT = 0x0b11;
        public const ushort CMD_DIAG_IO_422 = 0x0b12;
        public const ushort CMD_DIAG_RS232 = 0x0b13;
        public const ushort CMD_DIAG_CLOCK = 0x0b14;
        public const ushort CMD_DIAG_SFP = 0x0b15;
        public const ushort CMD_DIAG_LASER = 0x0b16;
        public const ushort CMD_DIAG_RS485 = 0x0b17;
        public const ushort CMD_DIAG_CAN2 = 0x0b18;
        public const ushort CMD_DIAG_ETHERNET = 0x0b19;

        public const ushort NTF_DIAG_TCUERR = 0x0070;
        public const ushort NTF_DIAG_XRAYON = 0x0071;
        public const ushort NTF_DIAG_KVMAOK = 0x0072;

        // Denali TCU/G-Box (0x0c)
        public const ushort CMD_GBUPDATE = 0x0c01;

        // X-Ray (0x11)
        public const ushort CMD_HVINIT = 0x1100;
        public const ushort CMD_START = 0x1101;
        public const ushort CMD_STOP = 0x1102;
        public const ushort CMD_PREPARE = 0x1103;
        public const ushort CMD_EXPOSE = 0x1104;
        public const ushort CMD_RESET = 0x1105;
        public const ushort CMD_REBOOT = 0x1106;
        public const ushort CMD_ABORT = 0x1107;
        public const ushort CMD_WARMUP = 0x110a;
        public const ushort CMD_SEASON = 0x110b;
        public const ushort CMD_FILCAL = 0x110c;
        public const ushort CMD_ESTIMATE = 0x110d;

        // Collimator (0x12)
        public const ushort CMD_CLMTVER = 0x1200;
        public const ushort CMD_HOMEAPE = 0x1201;
        public const ushort CMD_HOMEFLT = 0x1202;
        public const ushort CMD_MOVEAPE = 0x1203;
        public const ushort CMD_MOVEFLT = 0x1204;
        public const ushort CMD_STOPCOL = 0x1205;
        public const ushort CMD_SETSPDAPE1 = 0x1206;
        public const ushort CMD_SETSPDAPE2 = 0x1207;
        public const ushort CMD_SETSPDFILT = 0x1208;

        // Aggregator (0x13)
        public const ushort CMD_AGGREGATOR = 0x1300;

        // Laser (0x14)
        public const ushort CMD_LASERON = 0x1501;
        public const ushort CMD_LASEROFF = 0x1502;
        public const ushort CMD_SETLED = 0x1503;
        #endregion

        #region Aggregator Commands
        // Thermal Control Commands
        public const uint HeaterAll = 0xE0008000;
        public const uint HeaterXX = 0xC0008000u;
        public const byte ReadRailTemperature = 0xBC;
        public const byte ReadThermalErrorCode = 0xBE;
        public const byte ReadSetPoint = 0xC0;
        public const byte Read18VMonitor = 0xC2;
        public const byte Read25VMonitor = 0xC4;
        public const byte ReadMinus3VMonitor = 0xC6;
        public const byte Read3VMonitor = 0xC8;
        public const byte ReadHeaterVoltageDetector = 0xCA;
        public const byte ReadEnableInputState = 0xCC;
        public const byte ReadProportionalPIDConstant = 0xCE;
        public const byte ReadIntegralPIDConstant = 0xD0;
        public const byte ReadDerivativePIDConstant = 0xD2;
        public const byte ReadAutoTuneStepSize = 0xD4;
        public const byte ReadAutoTuneNoiseValue = 0xD6;
        public const byte ReadAutoTuneStartValue = 0xD8;
        public const byte ReadAutoTuneLookbackSeconds = 0xDA;
        public const byte ReadSetpointA2DTargetValue = 0xDC;
        public const byte ReadThermistorA2DInputValue = 0xDE;
        public const byte ReadHeaterPWMOutputValue = 0xE0;
        public const byte ReadThermalProgramName = 0xF6;
        public const byte ReadThermalProgramVersion = 0xF8;
        public const byte ChangeTemperatureSetpoint = 0xE4;
        public const byte ChangeProportionalPIDConstant = 0xE6;
        public const byte ChangeIntegralPIDConstant = 0xE8;
        public const byte ChangeDerivativePIDConstant = 0xEA;
        public const byte ChangeAutoTuneStepSize = 0xEC;
        public const byte ChangeAutoTuneNoiseValue = 0xEE;
        public const byte ChangeAutoTuneStartValue = 0xF0;
        public const byte ChangeAutoTuneLookbackSeconds = 0xF2;
        public const byte StartAutoTuneMode = 0xB8;
        public const byte CancelAutoTuneMode = 0xBA;
        public const byte ThermalNop = 0xFF;

        public const uint FanControl = 0xA0000000;
        public const byte ReadFanErrorCode = 0xBE;
        public const byte ReadDetectorAmbientTemp = 0xC0;
        public const byte ReadOtherTemperature1 = 0xC2;
        public const byte ReadOtherTemperature2 = 0xC4;
        public const byte ReadOtherTemperature3 = 0xC6;
        public const byte ReadRPM1 = 0xC8;
        public const byte ReadRPM2 = 0xCA;
        public const byte ReadRPM3 = 0xCC;
        public const byte ReadRPM4 = 0xCE;
        public const byte ReadRPM5 = 0xD0;
        public const byte ReadRPM6 = 0xD2;
        public const byte ReadRPM7 = 0xD4;
        public const byte ReadCurrentFanPWM = 0xD6;
        public const byte ReadInitialFanSpeedSetting = 0xDA;
        public const byte ReadModeSetting = 0xDC;
        public const byte ReadLastManualFanPWM = 0xE2;
        public const byte ReadFanProgramName = 0xE6;
        public const byte ReadFanProgramVersion = 0xE8;
        public const byte SetFanPWM = 0xB6;
        public const byte ChangeInitialFanSpeed = 0xB8;
        public const byte SetManualMode = 0xBC;
        public const byte SetAutoMode = 0xBA;
        public const byte SaveCurrentPamarameters = 0xDE;
        public const byte SetRapidRailHeatupMode = 0xE0;
        public const byte ResetFanErrorLog = 0xE4;
        public const byte FanNop = 0xFF;

        public const uint DataAcquisition = 0xD0000000;
        public const byte InitializeDataAcquisition = 0xD1;
        public const byte SetIntegrationTime = 0xD2;
        public const byte SlipRingLinkTrain = 0xD3;
        public const byte StartDataAcq = 0xDA;
        public const byte AdcPowerSave = 0xAF;
        public const byte DacNop = 0x00;
        public const byte InitializeAdc = 0xA1;
        public const byte StopDataAcq = 0xDF;
        public const byte DetectorDiagnostics = 0xDD;

        public const uint ConfigureEnvironment = 0x10000000;
        public const uint RcbNop = 0x00000000;
        public const uint GetSliceVersion = 0x20000000;
        #endregion Aggregator Commands

        #region Pre-defined Constants
        public const int FileTransmitBlockSize = 1022 * 8;
        #endregion

        private PeakCan m_canbus = null;

        private bool m_thread_run = false;
        private Thread m_thread_read_raw = null;

        private BlockingCollection<Notification> m_notification_queue;
        private BlockingCollection<string> m_log_queue;

        private ConcurrentDictionary<ushort, MessageCollection> m_response_pool;

        public RoterController(int p_device_id) {

            if (m_canbus == null) {

                m_canbus = new PeakCan();

                m_canbus.Init(p_device_id);
            }

            m_notification_queue = new BlockingCollection<Notification>();
            m_log_queue = new BlockingCollection<string>();

            m_response_pool = new ConcurrentDictionary<ushort, MessageCollection>();

            m_thread_run = true;
            m_thread_read_raw = new Thread(new ThreadStart(ReadSlipringRawFrame));
            m_thread_read_raw.IsBackground = true;
            m_thread_read_raw.Start();
        }

        ~RoterController() {

            m_thread_run = false;

            Thread.Sleep(1000);

            if (m_thread_read_raw != null) {

                m_thread_read_raw.Join();
                m_thread_read_raw = null;
            }

            m_canbus.Close();
        }

        public void SendNotification(ushort p_command, List<ulong> p_request) {

            MessageFactory factory = new MessageFactory();
            List<TPCANMsg> raw_message;

            factory.BuildRawMessage(p_command, p_request, out raw_message);

            m_canbus.EnqueueSlipringOutgoing(raw_message);
        }

        public void SendRequestAsync(ushort p_command, List<ulong> p_request) {

            MessageCollection response_collection = m_response_pool.AddOrUpdate(p_command, new MessageCollection(), (key, value) => new MessageCollection());

            MessageFactory factory = new MessageFactory();
            List<TPCANMsg> raw_message;

            factory.BuildRawMessage(p_command, p_request, out raw_message);

            m_canbus.EnqueueSlipringOutgoing(raw_message);
        }

        public bool WaitResponse(ushort p_command, out List<ulong> p_response, int p_wait_ms) {

            p_response = new List<ulong>();

            MessageCollection response_collection;

            if (m_response_pool.TryGetValue(p_command, out response_collection)) {

                if (response_collection.WaitOne(p_wait_ms)) {

                    FmiCanFrame fmi_frame;

                    while (response_collection.TryTake(out fmi_frame)) {

                        p_response.Add(fmi_frame.Data64);
                    }

                    return true;
                }
            }

            return false;
        }

        public void SendRequestSync(ushort p_command, List<ulong> p_request, out List<ulong> p_response, int p_delay = 500) {

            MessageCollection response_collection = m_response_pool.AddOrUpdate(p_command, new MessageCollection(), (key, value) => new MessageCollection());

            MessageFactory factory = new MessageFactory();
            List<TPCANMsg> raw_message;

            factory.BuildRawMessage(p_command, p_request, out raw_message);

            m_canbus.EnqueueSlipringOutgoing(raw_message);

            p_response = new List<ulong>();

            if (response_collection.WaitOne(p_delay)) {

                FmiCanFrame fmi_frame;

                while (response_collection.TryTake(out fmi_frame)) {

                    p_response.Add(fmi_frame.Data64);
                }
            }
            else {

                throw new Exception("response timed out");
            }
        }

        public Notification TakeNotification() {

            return m_notification_queue.Take();
        }

        public string TakeLog() {

            return m_log_queue.Take();
        }

        public void ReadSpareRawFrame(out List<byte> p_data) {

            p_data = new List<byte>();

            TPCANMsg raw_frame;
            m_canbus.DequeueSpareIncoming(out raw_frame);

            for (int i = 0 ; i < raw_frame.LEN ; i++) {

                p_data.Add(raw_frame.DATA[i]);
            }
        }

        private void ReadSlipringRawFrame() {

            TPCANMsg raw_frame;

            StringBuilder s_builder = new StringBuilder();

            while (m_thread_run) {

                m_canbus.DequeueSlipringIncoming(out raw_frame);

                FmiCanFrame fmi_frame = new FmiCanFrame(raw_frame);

                ModuleId source = (ModuleId)fmi_frame.SourceId;
                ModuleId destination = (ModuleId)fmi_frame.DestinationId;
                ushort command = (ushort)(fmi_frame.SourceId << 8 | fmi_frame.CommandBits);

                if (destination == ModuleId.Broadcast) { // notification received

                    if (source == ModuleId.Log) {

                        foreach (byte value in fmi_frame.Data8) {

                            if (value != 0) {

                                s_builder.Append((char)value);
                            }
                        }

                        if ((fmi_frame.ControlBits & 2) == 2) {

                            m_log_queue.Add(s_builder.ToString());

                            s_builder.Clear();
                        }
                    }
                    else {

                        m_notification_queue.Add(new Notification(fmi_frame));
                    }
                }
                else if (destination == ModuleId.Host) {  // normal frame received

                    MessageCollection response_collection;
                    if (!m_response_pool.TryGetValue(command, out response_collection)) {

                        continue;       // unrecognized command, skip the frame
                    }

                    response_collection.Add(fmi_frame);

                    if ((fmi_frame.ControlBits & 2) == 2) {

                        // a complete message is received, signal the event
                        response_collection.Set();
                    }
                }
            }
        }

        private void CheckErrorCode(ulong p_response) {

            if ((ushort)p_response != 0) {

                throw new Exception($"{p_response:X04}");
            }
        }

        #region Acadia
        public void SyncTime() {

            ulong unixTimeStamp = (ulong)(DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

            List<ulong> request = new List<ulong> { unixTimeStamp };
            List<ulong> response;

            SendRequestSync(CMD_SYNCTIME, request, out response, 500);

            CheckErrorCode(response[0]);
        }

        public void SyncTimeAsync() {

            ulong unixTimeStamp = (ulong)(DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

            List<ulong> request = new List<ulong> { unixTimeStamp };

            SendRequestAsync(CMD_SYNCTIME, request);
        }

        public bool WaitSyncTime(int p_wait_ms = 500) {

            List<ulong> response;

            return WaitResponse(CMD_SYNCTIME, out response, p_wait_ms);
        }

        public void StopRcbService() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_STOPBRYCE, request, out response, 500);

            CheckErrorCode(response[0]);
        }

        public void StopRcbServiceAsync() {

            List<ulong> request = new List<ulong> { 0 };

            SendRequestAsync(CMD_STOPBRYCE, request);
        }

        public bool WaitStopRcbService(int p_wait_ms = 500) {

            List<ulong> response;

            return WaitResponse(CMD_STOPBRYCE, out response, p_wait_ms);
        }

        public void StartRcbService() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_STARTBRYCE, request, out response, 500);

            CheckErrorCode(response[0]);
        }

        public void StartRcbServiceAsync() {

            List<ulong> request = new List<ulong> { 0 };

            SendRequestAsync(CMD_STARTBRYCE, request);
        }

        public bool WaitStartRcbService(int p_wait_ms = 500) {

            List<ulong> response;

            return WaitResponse(CMD_STARTBRYCE, out response, p_wait_ms);
        }

        public void SetLogLevelBryce(int p_level) {

            List<ulong> request = new List<ulong> { (ulong)p_level };
            List<ulong> response;

            SendRequestSync(CMD_SETLOGLEVEL_B, request, out response, 1000);

            CheckErrorCode(response[0]);
        }

        public void TurnOffLogBryce() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_TURNOFFLOGOUTPUT_B, request, out response);

            CheckErrorCode(response[0]);
        }

        public void TurnOnFileOutput() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_TURNONFILEOUTPUT_B, request, out response, 1000);

            CheckErrorCode(response[0]);
        }

        public void TurnOnStdOutput() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_TURNONSTDOUTPUT_B, request, out response);

            CheckErrorCode(response[0]);
        }

        public void TurnOnCanOutput() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_TURNONCANOUTPUT_B, request, out response);

            CheckErrorCode(response[0]);
        }

        public void ShutdownLinux() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_SHUTDOWNOS, request, out response, 500);

            CheckErrorCode(response[0]);
        }

        public void ShutdownLinuxAsync() {

            List<ulong> request = new List<ulong> { 0 };

            SendRequestAsync(CMD_SHUTDOWNOS, request);
        }

        public bool WaitShutdownLinux(int p_wait_ms = 500) {

            List<ulong> response;

            return WaitResponse(CMD_SHUTDOWNOS, out response, p_wait_ms);
        }

        public void RestartLinux() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_RESTARTOS, request, out response, 500);

            CheckErrorCode(response[0]);
        }

        public void RestartLinxuAsync() {

            List<ulong> request = new List<ulong> { 0 };

            SendRequestAsync(CMD_RESTARTOS, request);
        }

        public bool WaitRestartLinux(int p_wait_ms = 500) {

            List<ulong> response;

            return WaitResponse(CMD_RESTARTOS, out response, p_wait_ms);
        }

        public void StopEngineeringService() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_STOPDENALI, request, out response, 500);

            CheckErrorCode(response[0]);
        }

        public void StopEngineeringServiceAsync() {

            List<ulong> request = new List<ulong> { 0 };

            SendRequestAsync(CMD_STOPDENALI, request);
        }

        public bool WaitStopEngineeringService(int p_wait_ms = 500) {

            List<ulong> response;

            return WaitResponse(CMD_STOPDENALI, out response, p_wait_ms);
        }

        public void StartEngineeringService() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_STARTDENALI, request, out response, 500);

            CheckErrorCode(response[0]);
        }

        public void StartEngineeringServiceAsync() {

            List<ulong> request = new List<ulong> { 0 };

            SendRequestAsync(CMD_STARTDENALI, request);
        }

        public bool WaitStartEngineeringSerivice(int p_wait_ms = 500) {

            List<ulong> response;

            return WaitResponse(CMD_STARTDENALI, out response, p_wait_ms);
        }

        #region File
        public void TransmitFileInfo(long p_file_size, uint p_permission, string p_remote_path) {

            List<ulong> request = new List<ulong>();
            List<ulong> response;

            request.Add((ulong)p_permission);
            request.Add((ulong)p_file_size);

            byte[] remote_path = Encoding.ASCII.GetBytes(p_remote_path);

            ulong data64 = 0;
            for (int i = 0 ; i < remote_path.Length ; i++) {

                data64 |= (ulong)remote_path[i] << (i % 8 << 3);

                if (i % 8 == 7 || i == remote_path.Length - 1) {

                    request.Add(data64);

                    data64 = 0;
                }
            }

            SendRequestSync(CMD_TXFILEINFO, request, out response, 500);

            CheckErrorCode(response[0]);
        }

        public void TransmitFileContent(List<ulong> p_content, int p_block_size) {

            List<ulong> request = new List<ulong> { (ulong)p_block_size };
            List<ulong> response;

            request.AddRange(p_content);

            SendRequestSync(CMD_TXFILECONTENT, request, out response, 5000);

            CheckErrorCode(response[0]);
        }

        public void RetrieveFileInfo(string p_remote_path, out long p_file_size) {

            List<ulong> request = new List<ulong>();
            List<ulong> response;

            byte[] remote_path = Encoding.ASCII.GetBytes(p_remote_path);

            ulong data64 = 0;
            for (int i = 0 ; i < remote_path.Length ; i++) {

                data64 |= (ulong)remote_path[i] << (i % 8 << 3);

                if (i % 8 == 7 || i == remote_path.Length - 1) {

                    request.Add(data64);

                    data64 = 0;
                }
            }

            SendRequestSync(CMD_RXFILEINFO, request, out response, 500);

            CheckErrorCode(response[0]);

            p_file_size = (long)(response[1]);
        }

        public void RetrieveFileContent(int p_block_number, out List<ulong> p_content, out int p_block_size) {

            List<ulong> request = new List<ulong> { (ulong)p_block_number };
            List<ulong> response;

            SendRequestSync(CMD_RXFILECONTENT, request, out response, 5000);

            CheckErrorCode(response[0]);

            p_block_size = (int)(response[1]);

            response.RemoveRange(0, 2);

            p_content = response;
        }

        public void AbortTransmission() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_ABORTTXRX, request, out response);

            CheckErrorCode(response[0]);
        }

        public void ScanDir(string p_dir, out List<string> p_file_list) {

            List<ulong> request = new List<ulong>();
            List<ulong> response;

            byte[] dir_name = Encoding.ASCII.GetBytes(p_dir);

            ulong req_word = 0;
            for (int i = 0 ; i < dir_name.Length ; i++) {

                req_word |= (ulong)dir_name[i] << (i % 8 << 3);

                if (i % 8 == 7 || i == dir_name.Length - 1) {

                    request.Add(req_word);

                    req_word = 0;

                    if (i == dir_name.Length - 1) {

                        request.Add(0);
                    }
                }
            }

            SendRequestSync(CMD_SCANDIR, request, out response, 10000);

            CheckErrorCode(response[0]);

            response.RemoveAt(0);

            StringBuilder s_builder = new StringBuilder();
            p_file_list = new List<string>();

            foreach (ulong res_word in response) {

                for (int i = 0 ; i < 8 ; i++) {

                    byte value = (byte)(res_word >> (i << 3));

                    if (value != 0) {

                        s_builder.Append((char)value);
                    }
                    else {

                        p_file_list.Add(s_builder.ToString());

                        s_builder.Clear();

                        break;
                    }
                }
            }
        }

        public void DeleteFiles(List<string> p_filename_list) {

            List<ulong> request = new List<ulong>();
            List<ulong> response;

            foreach (string filename in p_filename_list) {

                byte[] log_name = Encoding.ASCII.GetBytes(filename);

                ulong data64 = 0;
                for (int i = 0 ; i < log_name.Length ; i++) {

                    data64 |= (ulong)log_name[i] << (i % 8 << 3);

                    if (i % 8 == 7 || i == log_name.Length - 1) {

                        request.Add(data64);

                        data64 = 0;

                        if (i == log_name.Length - 1) {

                            request.Add(0);
                        }
                    }
                }
            }

            if (p_filename_list.Count > 0) {

                SendRequestSync(CMD_DELETEFILES, request, out response, 10000);

                CheckErrorCode(response[0]);
            }
        }

        public void MountFat() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_MOUNTFAT, request, out response, 1000);

            CheckErrorCode(response[0]);
        }

        public void UmountFat() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_UMOUNTFAT, request, out response, 1000);

            CheckErrorCode(response[0]);
        }

        public void UmountFatAsync() {

            List<ulong> request = new List<ulong> { 0 };

            SendRequestAsync(CMD_UMOUNTFAT, request);
        }
        #endregion File
        #endregion Acadia

        #region Denali
        #region Register
        public ulong ReadRegister(int p_offset) {

            List<ulong> request = new List<ulong> { (ulong)p_offset };
            List<ulong> response;

            SendRequestSync(CMD_READREGISTER, request, out response, 500);

            CheckErrorCode(response[0]);

            return response[1];
        }

        public void WriteRegister(int p_offset, ulong p_content) {

            List<ulong> request = new List<ulong> { (ulong)p_offset, p_content };
            List<ulong> response;

            SendRequestSync(CMD_WRITEREGISTER, request, out response, 500);
        }
        #endregion Register

        #region Diagnostics
        public void DiagnoseDigitalIO() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_DIAG_IO_422, request, out response, 80000);

            CheckErrorCode(response[0]);
        }

        public void DiagnoseRS232(string p_tx, out string p_rx) {

            p_rx = "";

            List<ulong> request = new List<ulong>();
            List<ulong> response;

            int tx_len = 0;
            ulong tx64 = 0;
            foreach(byte c in p_tx) {

                tx64 |= (ulong)c << ((tx_len++ % 8) << 3);

                if (tx_len % 8 == 0) {

                    request.Add(tx64);
                    tx64 = 0;
                }
            }

            if(tx_len % 8 != 0) {

                request.Add(tx64);
            }

            SendRequestSync(CMD_DIAG_RS232, request, out response, 1000);

            CheckErrorCode(response[0]);

            response.RemoveAt(0);

            foreach (ulong rx64 in response) {

                for (int i = 0 ; i < 8 ; i++) {

                    byte c = (byte)(rx64 >> (i << 3));

                    if (c != 0) {

                        p_rx += (char)c;
                    }
                    else {

                        return;
                    }
                }
            }
        }

        public void DiagnoseClock() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_DIAG_CLOCK, request, out response, 1000);

            CheckErrorCode(response[0]);
        }

        public void DiagnoseSFP(List<uint> p_tx, out List<uint> p_rx) {

            p_rx = new List<uint>();

            List<ulong> request = new List<ulong>();
            List<ulong> response;

            for (int i = 0 ; i < 8 ; i += 2) {

                ulong tx64 = ((ulong)p_tx[i + 1] << 32) + (ulong)p_tx[i];

                request.Add(tx64);
            }

            SendRequestSync(CMD_DIAG_SFP, request, out response, 1000);

            CheckErrorCode(response[0]);

            response.RemoveAt(0);

            foreach (ulong rx64 in response) {

                p_rx.Add((uint)rx64);
                p_rx.Add((uint)(rx64 >> 32));
            }
        }

        public void DiagnoseLaser() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_DIAG_LASER, request, out response, 70000);

            CheckErrorCode(response[0]);
        }

        public void DiagnoseRS485() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_DIAG_RS485, request, out response, 40000);

            CheckErrorCode(response[0]);
        }

        public void DiagnoseCAN2() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_DIAG_CAN2, request, out response, 1000);

            CheckErrorCode(response[0]);
        }

        public void DiagnoseEthernet() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_DIAG_ETHERNET, request, out response, 1000);

            CheckErrorCode(response[0]);
        }

        public void AbortDiagnosis() {

            List<ulong> request = new List<ulong> { 0 };

            SendRequestAsync(CMD_DIAG_ABORT, request);
        }
        #endregion Diagnostics

        #region TCU/G-Box
        public void UpdateRequest() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_GBUPDATE, request, out response, 1000);

            CheckErrorCode(response[0]);
        }
        #endregion TCU/G-Box
        #endregion Denali

        #region Bryce
        #region X-ray
        public void InitHighVoltage() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_HVINIT, request, out response, 50000);

            CheckErrorCode(response[0]);
        }

        public void StartAnode() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_START, request, out response, 20000);

            CheckErrorCode(response[0]);
        }

        public void StopAnode() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_STOP, request, out response, 10000);

            CheckErrorCode(response[0]);
        }

        public void Prepare(List<SeriesParameter> p_scan_parameters) {

            List<ulong> request = new List<ulong>();
            List<ulong> response;

            ulong data64 = 0;

            foreach (SeriesParameter series in p_scan_parameters) {

                if(series.ScanType == 0) {  // 0: normal scan, 1: imA scan

                    request.Add(6);
                }
                else {

                    request.Add(6 + 2 + (ulong)series.ImaTable.MaValues.Count / 4);
                }

                data64 = (ulong)series.Kv << 0 | (ulong)(series.Ma & 0xffff) << 8 | (ulong)(series.Fss & 3) << 24;
                data64 |= (ulong)(series.ApertureMode & 0xff) << 32 | (ulong)(series.FilterMode & 0xff) << 40 | (ulong)(series.RowNumber & 0xff) << 48;
                data64 |= (ulong)(series.PhasePercentage & 0xff) << 56;
                request.Add(data64); // 0

                data64 = (ulong)(series.ShotTimeInMSec & 0x03ffff) << 0 | (ulong)(series.NumberOfShots & 0xff) << 18 | (ulong)(series.SeriesTimeInMSec & 0x03ffff) << 26 | (ulong)(series.DelayBeforeNextSeries & 0x0fffff) << 44;
                request.Add(data64); // 1

                data64 = (ulong)(series.TimeoutBetweenArmXrayOn & 0x0fffff) << 0 | (ulong)(series.DelayBetweenShots & 0xffff) << 20;
                request.Add(data64); // 2

                data64 = (ulong)(series.TriggerPosition & 0xffffffff) << 0 | (ulong)(series.TriggerMode & 3) << 32 | (ulong)(series.ScanType & 1) << 34;
                data64 |= (ulong)(series.DitherType & 0xf) << 35 | (ulong)(series.IsRelative & 1) << 43;
                data64 |= (ulong)(series.TicksPerRotation & 0xffff) << 44 | (ulong)(series.CardiacScan & 1) << 60 | (ulong)(series.EmergencyScan & 1) << 61 | (ulong)(series.CineScan & 1) << 62;
                request.Add(data64); // 3

                data64 = (ulong)(series.ErrorRegisterReset & 1) << 0 | (ulong)(series.EncoderSource & 1) << 1 | (ulong)(series.IntegrationTime & 0xffffff) << 2 | (ulong)(series.StartingSlice & 0x1f) << 26 | (ulong)(series.EndingSlice & 0x1f) << 31;
                data64 |= (ulong)(series.DataSource & 1) << 36 | (ulong)(series.InputSource & 0xf) << 37 | (ulong)(series.SampleMode & 1) << 41;
                data64 |= (ulong)(series.Decimation & 0x7) << 42 | (ulong)(series.ClockSpeed & 3) << 45 | (ulong)(series.Range & 0x7) << 47 | (ulong)(series.PostConversionShutdown & 1) << 50;
                request.Add(data64); // 4

                data64 = (ulong)(series.IntegrationAveraging & 7) << 0 | (ulong)(series.DetectorDataSource & 7) << 3 | (ulong)(series.IntegrationLimit & 0xffffff) << 6 | (ulong)(series.OffsetIntegrationLimit & 0x0fff) << 30 | (ulong)(series.TimePerRotationInMSec & 0x0fff) << 42;
                request.Add(data64); // 5

                // load ima table
                if (series.ScanType == 1) {

                    data64 = (ulong)(series.ImaTable.StartingMa & 0xffff) << 0 | (ulong)(series.ImaTable.PeakMa & 0xffff) << 16 | (ulong)(series.ImaTable.AverageMa & 0xffff) << 32 | (ulong)(series.ImaTable.MinMa & 0xffff) << 48;
                    request.Add(data64);

                    data64 = (ulong)(series.ImaTable.PhaseMinus & 0xff) << 0 | (ulong)(series.ImaTable.PhasePlus & 0xff) << 8 | (ulong)(series.ImaTable.TrevQuarter & 0x1ff) << 16 | (ulong)(series.ImaTable.Tup & 0x3ff) << 25;
                    data64 |= (ulong)(series.ImaTable.NTheta & 0xff) << 35 | (ulong)(series.ImaTable.GantryHomeAngleOffset & 0x7ff) << 43;
                    request.Add(data64);

                    for(int i = 0 ; i < series.ImaTable.MaValues.Count ; i += 4) {

                        data64 = (ulong)(series.ImaTable.MaValues[i] & 0xffff) << 0 | (ulong)(series.ImaTable.MaValues[i + 1] & 0xffff) << 16 | (ulong)(series.ImaTable.MaValues[i + 2] & 0xffff) << 32 | (ulong)(series.ImaTable.MaValues[i + 3] & 0xffff) << 48;
                        request.Add(data64);
                    }
                }
            }

            SendRequestSync(CMD_PREPARE, request, out response, 15000);

            CheckErrorCode(response[0]);
        }

        public void Expose(int p_scan_time_ms) {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_EXPOSE, request, out response, p_scan_time_ms);

            CheckErrorCode(response[0]);
        }

        public void ResetHighVoltage() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_RESET, request, out response, 500);

            CheckErrorCode(response[0]);
        }

        public void RebootGbox() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_REBOOT, request, out response, 40000);

            CheckErrorCode(response[0]);
        }

        public void Abort() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_ABORT, request, out response, 5000);

            CheckErrorCode(response[0]);
        }

        public void Estimate() {

        }

        public void Warmup(int p_type) {

            List<ulong> request = new List<ulong> { (ulong)p_type };
            List<ulong> response;

            int wait_time = p_type == 0 ? 600000 : 300000;

            SendRequestSync(CMD_WARMUP, request, out response, wait_time);

            CheckErrorCode(response[0]);
        }

        public void Season() {

            List<ulong> request = new List<ulong> { 0 };

            SendRequestAsync(CMD_SEASON, request);
        }

        public void Filcal(List<byte> p_kv_list) {

            List<ulong> request = new List<ulong>();
            List<ulong> response;

            foreach (byte kv in p_kv_list) {

                request.Add((ulong)kv);
            }

            int wait_time = 90000 * p_kv_list.Count;

            SendRequestSync(CMD_FILCAL, request, out response, wait_time);

            CheckErrorCode(response[0]);
        }
        #endregion

        #region Collimator
        public void CollimatorVersion(out int p_galil_version) {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_CLMTVER, request, out response, 500);

            CheckErrorCode(response[0]);

            p_galil_version = (int)(response[1]);
        }

        public void HomeAperture(out int p_ap1_pos, out int p_ap2_pos, out int p_flt_pos) {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_HOMEAPE, request, out response, 20000);

            CheckErrorCode(response[0]);

            p_ap1_pos = (int)response[1];
            p_ap2_pos = (int)response[2];
            p_flt_pos = (int)response[3];
        }

        public void HomeApertureAsync() {

            List<ulong> request = new List<ulong> { 0 };

            SendRequestAsync(CMD_HOMEAPE, request);
        }

        public bool WaitHomeAperture(int p_wait_ms, out ushort p_result, out int p_ap1_pos, out int p_ap2_pos, out int p_flt_pos) {

            List<ulong> response;

            bool status = WaitResponse(CMD_HOMEAPE, out response, p_wait_ms);

            if (status) {

                p_result = (ushort)(response[0]);

                CheckErrorCode(response[0]);

                p_ap1_pos = (int)response[1];
                p_ap2_pos = (int)response[2];
                p_flt_pos = (int)response[3];
            }
            else {

                p_result = 0;
                p_ap1_pos = 0;
                p_ap2_pos = 0;
                p_flt_pos = 0;
            }

            return status;
        }

        public void HomeFilter(out int p_ap1_pos, out int p_ap2_pos, out int p_flt_pos) {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_HOMEFLT, request, out response, 20000);

            CheckErrorCode(response[0]);

            p_ap1_pos = (int)response[1];
            p_ap2_pos = (int)response[2];
            p_flt_pos = (int)response[3];
        }

        public void HomeFilterAsync() {

            List<ulong> request = new List<ulong> { 0 };

            SendRequestAsync(CMD_HOMEFLT, request);
        }

        public bool WaitHomeFilter(int p_wait_ms, out ushort p_result, out int p_ap1_pos, out int p_ap2_pos, out int p_flt_pos) {

            List<ulong> response;

            bool status = WaitResponse(CMD_HOMEFLT, out response, p_wait_ms);

            if (status) {

                p_result = (ushort)(response[0]);

                CheckErrorCode(response[0]);

                p_ap1_pos = (int)response[1];
                p_ap2_pos = (int)response[2];
                p_flt_pos = (int)response[3];
            }
            else {

                p_result = 0;
                p_ap1_pos = 0;
                p_ap2_pos = 0;
                p_flt_pos = 0;
            }

            return status;
        }

        public void MoveAperture(bool p_aperture1, bool p_aperture2, int p_position1, int p_position2, out int p_ap1_pos, out int p_ap2_pos, out int p_flt_pos) {

            long position1 = p_aperture1 ? (1L << 63) | (uint)p_position1 : 0;

            long position2 = p_aperture2 ? (1L << 63) | (uint)p_position2 : 0;

            List<ulong> request = new List<ulong> { (ulong)position1, (ulong)position2 };
            List<ulong> response;

            SendRequestSync(CMD_MOVEAPE, request, out response, 20000);

            CheckErrorCode(response[0]);

            p_ap1_pos = (int)response[1];
            p_ap2_pos = (int)response[2];
            p_flt_pos = (int)response[3];
        }

        public void MoveApertureAsync(bool p_aperture1, bool p_aperture2, int p_position1, int p_position2) {

            long position1 = p_aperture1 ? (1L << 63) | (uint)p_position1 : 0;

            long position2 = p_aperture2 ? (1L << 63) | (uint)p_position2 : 0;

            List<ulong> request = new List<ulong> { (ulong)position1, (ulong)position2 };

            SendRequestAsync(CMD_MOVEAPE, request);
        }

        public bool WaitMoveAperture(int p_wait_ms, out ushort p_result, out int p_ap1_pos, out int p_ap2_pos, out int p_flt_pos) {

            List<ulong> response;

            bool status = WaitResponse(CMD_MOVEAPE, out response, p_wait_ms);

            if (status) {

                p_result = (ushort)(response[0]);

                CheckErrorCode(response[0]);

                p_ap1_pos = (int)response[1];
                p_ap2_pos = (int)response[2];
                p_flt_pos = (int)response[3];
            }
            else {

                p_result = 0;
                p_ap1_pos = 0;
                p_ap2_pos = 0;
                p_flt_pos = 0;
            }

            return status;
        }

        public void MoveFilter(int p_position, out int p_ap1_pos, out int p_ap2_pos, out int p_flt_pos) {

            List<ulong> request = new List<ulong> { (ulong)p_position };
            List<ulong> response;

            SendRequestSync(CMD_MOVEFLT, request, out response, 20000);

            CheckErrorCode(response[0]);

            p_ap1_pos = (int)response[1];
            p_ap2_pos = (int)response[2];
            p_flt_pos = (int)response[3];
        }

        public void MoveFilterAsync(int p_position) {

            List<ulong> request = new List<ulong> { (ulong)p_position };

            SendRequestAsync(CMD_MOVEFLT, request);
        }

        public bool WaitMoveFilter(int p_wait_ms, out ushort p_result, out int p_ap1_pos, out int p_ap2_pos, out int p_flt_pos) {

            List<ulong> response;

            bool status = WaitResponse(CMD_MOVEFLT, out response, p_wait_ms);

            if (status) {

                p_result = (ushort)(response[0]);

                CheckErrorCode(response[0]);

                p_ap1_pos = (int)response[1];
                p_ap2_pos = (int)response[2];
                p_flt_pos = (int)response[3];
            }
            else {

                p_result = 0;
                p_ap1_pos = 0;
                p_ap2_pos = 0;
                p_flt_pos = 0;
            }

            return status;
        }

        public void StopCollimator() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_STOPCOL, request, out response, 500);

            CheckErrorCode(response[0]);
        }

        public void SetApeture1Speed(int p_speed) {

            List<ulong> request = new List<ulong> { (ulong)p_speed };
            List<ulong> response;

            SendRequestSync(CMD_SETSPDAPE1, request, out response, 500);

            CheckErrorCode(response[0]);
        }

        public void SetApeture2Speed(int p_speed) {

            List<ulong> request = new List<ulong> { (ulong)p_speed };
            List<ulong> response;

            SendRequestSync(CMD_SETSPDAPE2, request, out response, 500);

            CheckErrorCode(response[0]);
        }

        public void SetFilterSpeed(int p_speed) {

            List<ulong> request = new List<ulong> { (ulong)p_speed };
            List<ulong> response;

            SendRequestSync(CMD_SETSPDFILT, request, out response, 500);

            CheckErrorCode(response[0]);
        }
        #endregion

        #region Aggregator
        public void AggregatorControl(List<uint> p_request, out List<uint> p_response, int p_wait_ms) {

            List<ulong> request = new List<ulong>();
            List<ulong> response;

            for (int i = 0 ; i < 8 ; i += 2) {

                ulong data64 = (ulong)p_request[i + 1] << 32 | p_request[i];
                request.Add(data64);
            }

            SendRequestSync(CMD_AGGREGATOR, request, out response, p_wait_ms);

            CheckErrorCode(response[0]);

            p_response = new List<uint>();

            response.RemoveAt(0);

            foreach (ulong value in response) {

                p_response.Add((uint)(value >> 0));
                p_response.Add((uint)(value >> 32));
            }
        }
        #endregion

        #region Thermal
        #endregion

        #region Laser
        public void TurnOnLaser() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_LASERON, request, out response, 500);

            CheckErrorCode(response[0]);
        }

        public void TurnOffLaser() {

            List<ulong> request = new List<ulong> { 0 };
            List<ulong> response;

            SendRequestSync(CMD_LASEROFF, request, out response, 500);

            CheckErrorCode(response[0]);
        }

        #endregion

        #region Log
        #endregion

        #region Notification
        public void NotifyDoorStatus(bool p_closed) {

            List<ulong> request = new List<ulong> { p_closed ? (ulong)1 : (ulong)0 };

            SendNotification(NTF_DOOR, request);
        }

        public void NotifyCStep(int p_step) {

            List<ulong> request = new List<ulong> { (ulong)p_step };

            SendNotification(NTF_CSTEP, request);
        }

        public void NotifyMStep(int p_step) {

            List<ulong> request = new List<ulong> { (ulong)p_step };

            SendNotification(NTF_MSTEP, request);
        }

        public void ResetTablePosition() {

            List<ulong> request = new List<ulong> { 0 };

            SendNotification(NTF_RESETTPOS, request);
        }
        #endregion Notification
        #endregion
    }
}
