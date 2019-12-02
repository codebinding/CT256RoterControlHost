using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Peak.Can.Basic;

namespace RoterControlSupport {
    class MessageFactory {

        private ModuleId m_destination_id;
        private byte m_command;
        private List<ulong> m_content;

        public MessageFactory() {

        }

        public MessageFactory(ModuleId p_destination_id, byte p_command) {

            m_destination_id = p_destination_id;
            m_command = p_command;

            m_content = new List<ulong>();
        }

        public void BuildFmiMessage(ushort p_command, List<ulong> p_data) {

            
        }

        public void BuildRawMessage(ushort p_command, List<ulong> p_data, out List<TPCANMsg> p_raw_message) {

            p_raw_message = new List<TPCANMsg>();

            byte destination_id = (byte)(p_command >> 8);
            byte command = (byte)p_command;

            FmiCanFrame fmi_frame = new FmiCanFrame();

            for (int i = 0 ; i < p_data.Count ; i++) {

                if (i == 0) {

                    if (p_data.Count == 1) {

                        fmi_frame.ControlBits = (byte)(ControlBit.Start | ControlBit.End);
                    }
                    else {

                        fmi_frame.ControlBits = (byte)ControlBit.Start;
                    }

                    fmi_frame.ParameterBits = (ushort)p_data.Count;
                }
                else if (i == p_data.Count - 1) {

                    fmi_frame.ControlBits = (byte)ControlBit.End;
                    fmi_frame.ParameterBits = (ushort)i;
                }
                else {

                    fmi_frame.ControlBits = (byte)ControlBit.Middle;
                    fmi_frame.ParameterBits = (ushort)i;
                }

                fmi_frame.SourceId = (byte)ModuleId.Host;
                fmi_frame.DestinationId = destination_id;
                fmi_frame.CommandBits = command;
                fmi_frame.Data64 = p_data[i];

                p_raw_message.Add(fmi_frame.GetRawFrame());
            }
        }
    }
}
