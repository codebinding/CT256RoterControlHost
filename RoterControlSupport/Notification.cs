using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoterControlSupport {
    public class Notification {

        public byte SourceId {
            
            get { return m_source_id; }
        }

        public byte CommandBits {

            get { return m_command_bits; }
        }

        public ulong Data64 {

            get { return m_data64; }
        }

        private byte m_source_id;
        private byte m_command_bits;
        private ulong m_data64;

        public Notification(FmiCanFrame p_fmi_frame) {

            m_source_id = p_fmi_frame.SourceId;
            m_command_bits = p_fmi_frame.CommandBits;
            m_data64 = p_fmi_frame.Data64;
        }
    }
}
