using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCBTool {
    class RcbException : Exception{

        public ushort Code {

            get { return m_error; }
        }

        private ushort m_error;

        public RcbException(ushort p_error) : base($"Error: {p_error:X04}") {

            m_error = p_error;
        }

        public RcbException(ushort p_error, string p_message) : base(p_message) {

            m_error = p_error;
        }
    }
}
