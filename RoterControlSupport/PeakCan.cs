using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

using Peak.Can.Basic;
using TPCANHandle = System.Byte;

namespace RoterControlSupport {
    class PeakCan {

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

        private bool m_thread_run = false;
        private Thread m_thread_read_incoming = null;
        private Thread m_thread_write_outgoing = null;

        private BlockingCollection<TPCANMsg> m_incoming_queue;
        private BlockingCollection<TPCANMsg> m_outgoing_queue;

        public void Init(int p_device_id) {

            TPCANStatus status;
            uint condition;
            uint device_id;

            foreach (TPCANHandle channel in USB_CHANNELS) {

                status = PCANBasic.GetValue(channel, TPCANParameter.PCAN_CHANNEL_CONDITION, out condition, sizeof(UInt32));
                if (status == TPCANStatus.PCAN_ERROR_OK && (condition & PCANBasic.PCAN_CHANNEL_AVAILABLE) == PCANBasic.PCAN_CHANNEL_AVAILABLE) {

                    status = PCANBasic.Initialize(channel, TPCANBaudrate.PCAN_BAUD_1M);
                    if (status == TPCANStatus.PCAN_ERROR_OK) {

                        status = PCANBasic.GetValue(channel, TPCANParameter.PCAN_DEVICE_NUMBER, out device_id, sizeof(UInt32));
                        if (status == TPCANStatus.PCAN_ERROR_OK && device_id == p_device_id) {

                            m_sock = channel;

                            break;
                        }
                        else {

                            PCANBasic.Uninitialize(channel);
                        }
                    }
                }
            }

            if (m_sock == 0) {

                throw new Exception("Error initializing CANBus");
            }

            m_incoming_queue = new BlockingCollection<TPCANMsg>();
            m_outgoing_queue = new BlockingCollection<TPCANMsg>();

            m_thread_run = true;
            m_thread_read_incoming = new Thread(new ThreadStart(ReadIncoming));
            m_thread_read_incoming.IsBackground = true;
            m_thread_read_incoming.Start();

            m_thread_write_outgoing = new Thread(new ThreadStart(WriteOutgoing));
            m_thread_write_outgoing.IsBackground = true;
            m_thread_write_outgoing.Start();
        }

        public void Close() {

            m_thread_run = false;

            Thread.Sleep(1000);

            if (m_thread_read_incoming != null) {

                m_thread_read_incoming.Join();
                m_thread_read_incoming = null;
            }

            if(m_thread_write_outgoing != null) {

                m_thread_write_outgoing.Join();
                m_thread_write_outgoing = null;
            }

            PCANBasic.Uninitialize(m_sock);
        }

        public void EnqueueOutgoing(TPCANMsg p_frame) {

            m_outgoing_queue.Add(p_frame);
        }

        public void EnqueueOutgoing(List<TPCANMsg> p_frame_list) {

            foreach (TPCANMsg frame in p_frame_list) {

                m_outgoing_queue.Add(frame);
            }
        }

        public void DequeueIncoming(out TPCANMsg p_frame) {

            p_frame = m_incoming_queue.Take();
        }

        private void ReadIncoming() {

            AutoResetEvent can_event = new AutoResetEvent(false);

            uint numeric_buffer = Convert.ToUInt32(can_event.SafeWaitHandle.DangerousGetHandle().ToInt32());
            // Sets the handle of the Receive-Event.
            //
            TPCANStatus status = PCANBasic.SetValue(m_sock, TPCANParameter.PCAN_RECEIVE_EVENT, ref numeric_buffer, sizeof(UInt32));

            if (status != TPCANStatus.PCAN_ERROR_OK) {

                throw new Exception(GetFormatedError(status));
            }

            TPCANMsg raw_frame;

            while (m_thread_run) {

                if (can_event.WaitOne(50)) {
                
                    do {

                        if ((status = PCANBasic.Read(m_sock, out raw_frame)) == TPCANStatus.PCAN_ERROR_OK) {

                            m_incoming_queue.Add(raw_frame);
                        }

                    } while (!Convert.ToBoolean(status & TPCANStatus.PCAN_ERROR_QRCVEMPTY));
                }
            }
        }

        private void WriteOutgoing() {

            TPCANStatus status;
            TPCANMsg raw_frame;

            while (m_thread_run) {

                raw_frame = m_outgoing_queue.Take();

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
