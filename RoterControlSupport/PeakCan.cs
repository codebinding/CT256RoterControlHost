using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

using Peak.Can.Basic;
using TPCANHandle = System.Byte;

namespace RoterControlSupport {
    public class PeakCan {

        private List<TPCANHandle> USB_CHANNELS = new List<TPCANHandle> {

                PCANBasic.PCAN_USBBUS1,
                PCANBasic.PCAN_USBBUS2,
                PCANBasic.PCAN_USBBUS3,
                PCANBasic.PCAN_USBBUS4,
                PCANBasic.PCAN_USBBUS5,
                PCANBasic.PCAN_USBBUS6,
                PCANBasic.PCAN_USBBUS7,
                PCANBasic.PCAN_USBBUS8
        };

        private TPCANHandle m_sock = 0;
        private bool m_thread_stop = true;
        private Thread m_thread_rx = null;
        private Thread m_thread_tx = null;

        private BlockingCollection<TPCANMsg> m_queue_rx = null;
        private BlockingCollection<TPCANMsg> m_queue_tx = null;

        public PeakCan(int p_peak_id, TPCANBaudrate p_baud_rate = TPCANBaudrate.PCAN_BAUD_1M) {

            TPCANStatus status;
            uint condition;
            uint device_id;

            foreach (TPCANHandle channel in USB_CHANNELS) {

                status = PCANBasic.GetValue(channel, TPCANParameter.PCAN_CHANNEL_CONDITION, out condition, sizeof(UInt32));
                if (status == TPCANStatus.PCAN_ERROR_OK && (condition & PCANBasic.PCAN_CHANNEL_AVAILABLE) == PCANBasic.PCAN_CHANNEL_AVAILABLE) {

                    status = PCANBasic.Initialize(channel, p_baud_rate);
                    if (status == TPCANStatus.PCAN_ERROR_OK) {

                        status = PCANBasic.GetValue(channel, TPCANParameter.PCAN_DEVICE_NUMBER, out device_id, sizeof(UInt32));
                        if (status == TPCANStatus.PCAN_ERROR_OK && device_id == p_peak_id) {

                            m_sock = channel;
                        }

                        PCANBasic.Uninitialize(channel);
                    }
                }
            }

            if (m_sock == 0) {

                throw new Exception($"PEAK CAN USB adapt with id 0x{p_peak_id:X} not found");
            }
            else {

                status = PCANBasic.Initialize(m_sock, p_baud_rate);

                if (status != TPCANStatus.PCAN_ERROR_OK) {

                    throw new Exception($"Error initializing CAN with id 0x{p_peak_id:X}");
                }

                status = PCANBasic.Reset(m_sock);
                if (status != TPCANStatus.PCAN_ERROR_OK) {

                    throw new Exception(GetFormatedError(status));
                }

                uint numeric_buffer = PCANBasic.PCAN_PARAMETER_ON;
                status = PCANBasic.SetValue(m_sock, TPCANParameter.PCAN_RECEIVE_EVENT, ref numeric_buffer, sizeof(UInt32));

                if (status != TPCANStatus.PCAN_ERROR_OK) {

                    throw new Exception(GetFormatedError(status));
                }

                status = PCANBasic.GetStatus(m_sock);
                if (status != TPCANStatus.PCAN_ERROR_OK) {

                    throw new Exception(GetFormatedError(status));
                }
            }

            m_queue_rx = new BlockingCollection<TPCANMsg>();
            m_queue_tx = new BlockingCollection<TPCANMsg>();

            m_thread_stop = false;
            m_thread_rx = new Thread(new ThreadStart(ReadRawFrame));
            m_thread_rx.IsBackground = true;
            m_thread_rx.Start();

            m_thread_tx = new Thread(new ThreadStart(WriteRawFrame));
            m_thread_tx.IsBackground = true;
            m_thread_tx.Start();
        }

        ~PeakCan() {

            m_thread_stop = false;

            Thread.Sleep(1000);

            if (m_thread_rx != null) {

                m_thread_rx.Join();
            }

            if(m_thread_tx != null) {

                m_thread_tx.Join();
            }

            if (m_sock != 0) {

                PCANBasic.Uninitialize(m_sock);
            }
        }

        public void EnqueueTx(TPCANMsg p_frame) {

            m_queue_tx.Add(p_frame);
        }

        public void EnqueueTx(List<TPCANMsg> p_frames) {

            foreach (TPCANMsg frame in p_frames) {

                m_queue_tx.Add(frame);
            }
        }

        public void DequeueRx(out TPCANMsg p_frame) {

            p_frame = m_queue_rx.Take();
        }

        private void ReadRawFrame() {

            AutoResetEvent can_event = new AutoResetEvent(false);

            uint numeric_buffer = Convert.ToUInt32(can_event.SafeWaitHandle.DangerousGetHandle().ToInt32());
            TPCANStatus status = PCANBasic.SetValue(m_sock, TPCANParameter.PCAN_RECEIVE_EVENT, ref numeric_buffer, sizeof(UInt32));

            if (status != TPCANStatus.PCAN_ERROR_OK) {

                throw new Exception(GetFormatedError(status));
            }

            TPCANMsg raw_frame;

            while (!m_thread_stop) {

                if (can_event.WaitOne(50)) {
                
                    do {

                        if ((status = PCANBasic.Read(m_sock, out raw_frame)) == TPCANStatus.PCAN_ERROR_OK) {

                            m_queue_rx.Add(raw_frame);
                        }

                    } while (!Convert.ToBoolean(status & TPCANStatus.PCAN_ERROR_QRCVEMPTY));
                }
            }
        }

        private void WriteRawFrame() {

            TPCANStatus status;
            TPCANMsg raw_frame;

            while (!m_thread_stop) {

                raw_frame = m_queue_tx.Take();

                if ((status = PCANBasic.Write(m_sock, ref raw_frame)) != TPCANStatus.PCAN_ERROR_OK) {

                    throw new Exception(GetFormatedError(status));
                }
            }
        }

        private string GetFormatedError(TPCANStatus p_error) {

            StringBuilder sb_error_text;

            sb_error_text = new StringBuilder(256);

            if (PCANBasic.GetErrorText(p_error, 0, sb_error_text) != TPCANStatus.PCAN_ERROR_OK) {

                return $"An error occurred. Error-code's text ({p_error:X}) couldn't be retrieved";
            }
            else {

                return sb_error_text.ToString();
            }
        }
    }
}
