using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Peak.Can.Basic;

namespace RoterControlSupport {

    enum ModuleId : byte { Broadcast = 0x00, Host = 0x01, Acadia = 0x05, File = 0x6, Denali = 0x0A, Bryce = 0x10, Xray = 0x11, Collimator = 0x12, Aggregator = 0x13, Dtcb = 0x14, Laser = 0x15, Log=0x1F };
    enum ControlBit : byte { Middle = 0, Start = 1, End = 2 };

    public class FmiCanFrame {

        public uint CanId {

            get { return m_can_id; }
            set { m_can_id = value; }
        }

        public byte[] Data8 {

            get { return m_data8; }
        }

        public ulong Data64 {

            get { return m_data64; }
            set { m_data64 = value; }
        }

        public ushort ParameterBits {

            get { return (ushort)(m_can_id & 0x000003ffu); }
            set { m_can_id = (m_can_id & 0xfffffc00u) | (value & 0x000003ffu); }
        }

        public byte CommandBits {

            get { return (byte)((m_can_id >> 10) & 0x0000007fu); }
            set { m_can_id = (m_can_id & 0xfffe03ffu) | (uint)value << 10; }
        }

        public byte DestinationId {

            get { return (byte)((m_can_id >> 17) & 0x0000001fu); }
            set { m_can_id = (m_can_id & 0xffc1ffffu) | (uint)value << 17; }
        }

        public byte SourceId {

            get { return (byte)((m_can_id >> 22) & 0x0000001fu); }
            set { m_can_id = (m_can_id & 0xf83fffffu) | (uint)value << 22; }
        }

        public byte ControlBits {

            get { return (byte)((m_can_id >> 27) & 0x03); }
            set { m_can_id = (m_can_id & 0xe7ffffffu) | (uint)value << 27; }
        }

        private uint m_can_id;
        private ulong m_data64;
        private byte[] m_data8;

        public FmiCanFrame() {

            m_can_id = 0;
            m_data64 = 0;
        }

        public FmiCanFrame(TPCANMsg p_raw_frame) {

            m_can_id = p_raw_frame.ID;

            m_data8 = new byte[8];

            for (int i = 0 ; i < p_raw_frame.LEN ; i++) {

                m_data64 |= (ulong)p_raw_frame.DATA[i] << (i * 8);
                m_data8[i] = p_raw_frame.DATA[i];
            }
        }

        public TPCANMsg GetRawFrame() {

            TPCANMsg raw_frame = new TPCANMsg();

            raw_frame.ID = m_can_id;
            raw_frame.LEN = 8;
            raw_frame.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_EXTENDED;

            /*for (int i = 0 ; i < 8 ; i++) {

                raw_frame.DATA[i] = (byte)(m_data64 >> (i * 8));
            }*/

            raw_frame.DATA = BitConverter.GetBytes(m_data64);

            return raw_frame;
        }

        public void PrintContent() {


        }
    }
}
