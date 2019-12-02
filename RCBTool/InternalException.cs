using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCBTool {
    class InternalException {

        public const UInt16 E_OPENFILE = 0xa001;
        public const UInt16 E_GETFILESTATUS = 0xa002;
        public const UInt16 E_WRITEFILE = 0xa003;
        public const UInt16 E_READFILE = 0xa004;
        public const UInt16 E_CLOSEFILE = 0xa005;
        public const UInt16 E_SEEKFILE = 0xa006;
        public const UInt16 E_CREATETHREAD = 0xa007;
        public const UInt16 E_INITMUTEX = 0xa008;
        public const UInt16 E_INITCOND = 0xa009;
        public const UInt16 E_OPENSOCKET = 0xa00a;
        public const UInt16 E_BINDSOCKET = 0xa00b;
        public const UInt16 E_READSOCKET = 0xa00c;
        public const UInt16 E_WRITESOCKET = 0xa00d;
        public const UInt16 E_NOMORECONTENT = 0xa00e;
        public const UInt16 E_FILENOTOPEN = 0xa00f;
        public const UInt16 E_TIMENOTSYNCED = 0xa010;

        private static Dictionary<UInt16, string> ErrorTable = new Dictionary<UInt16, string> {

            { E_OPENFILE, "fail to open file" },
            { E_GETFILESTATUS, "fail to get file status" },
            { E_WRITEFILE, "fail to write file" },
            { E_READFILE, "fail to read file" },
            { E_CLOSEFILE, "fail to close file" },
            { E_SEEKFILE, "fail to seek file" },
            { E_CREATETHREAD, "fail to create thread" },
            { E_INITMUTEX, "fail to init mutex" },
            { E_INITCOND, "fail to init cond" },
            { E_OPENSOCKET, "fail to open socket" },
            { E_BINDSOCKET, "fail to bind socket" },
            { E_READSOCKET, "fail to read socket" },
            { E_WRITESOCKET, "fail to write socket" },
            { E_NOMORECONTENT, "no more content" },
            { E_FILENOTOPEN, "file not opened yet" },
            { E_TIMENOTSYNCED, "time is not synced" }
        };

        public static string GetErrorString(UInt16 errorCode) {

            string message = "unknown error";

            ErrorTable.TryGetValue(errorCode, out message);

            return message;
        }
    }
}
