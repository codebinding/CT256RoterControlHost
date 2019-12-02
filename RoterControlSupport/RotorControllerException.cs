using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoterControlSupport {
    public static class RoterControllerException {

        public const UInt16 E_SUCCESS = 0x0000;
        public const UInt16 E_ADB_NotPresent = 0x1000;
        public const UInt16 E_ADB_TxNotReady = 0x1001;
        public const UInt16 E_ADB_RxTimeout = 0x1002;
        public const UInt16 E_ADB_InvalidTxLen = 0x1003;
        public const UInt16 E_ADB_BadResponse = 0x1004;
        public const UInt16 E_ADB_AuthFailed = 0x1005;
        public const UInt16 E_ADB_AuthTimeout = 0x1006;

        public const UInt16 E_TCU_NotPresent = 0x2000;
        public const UInt16 E_TCU_TxNotReady = 0x2001;
        public const UInt16 E_TCU_RxTimeout = 0x2002;
        public const UInt16 E_TCU_InvalidTxLen = 0x2003;
        public const UInt16 E_TCU_BadResponseLength = 0x2004;
        public const UInt16 E_TCU_BadResponseCRC = 0x2005;
        public const UInt16 E_TCU_BadResponse = 0x2006;
        public const UInt16 E_TCU_NackResponse = 0x2007;
        public const UInt16 E_TCU_KvNotInRange = 0x2008;
        public const UInt16 E_TCU_MaNotInRange = 0x2009;
        public const UInt16 E_TCU_FocusOn = 0x200a;
        public const UInt16 E_TCU_TechniqueNotMatch = 0x200b;
        public const UInt16 E_TCU_NotOperational = 0x200c;
        public const UInt16 E_TCU_NotReady4Shot = 0x200d;

        public const UInt16 E_GBX_NotPresent = 0x3000;
        public const UInt16 E_GBX_TxNotReady = 0x3001;
        public const UInt16 E_GBX_NotInStateWC = 0x3002;
        public const UInt16 E_GBX_NotInStateOS = 0x3003;
        public const UInt16 E_GBX_NotInStatePS = 0x3004;
        public const UInt16 E_GBX_InStateER = 0x3005;

        public const UInt16 E_COL_NotPresent = 0x4000;
        public const UInt16 E_COL_TxNotReady = 0x4001;
        public const UInt16 E_COL_RxTimeout = 0x4002;
        public const UInt16 E_COL_InvalidTxLen = 0x4003;
        public const UInt16 E_COL_BadResponse = 0x4004;
        public const UInt16 E_COL_GalilErrorCodeBase = 0x4100;  // 4101 ~ 4109

        public const UInt16 E_AGG_NotPresent = 0x5000;
        public const UInt16 E_AGG_TxNotReady = 0x5001;
        public const UInt16 E_AGG_RxTimeout = 0x5002;
        public const UInt16 E_AGG_InvalidTxLen = 0x5003;
        public const UInt16 E_AGG_CorruptedMessage = 0x5005;

        public const UInt16 E_ENV = 0x6000;

        public const UInt16 E_DAT_AdcInitAll = 0x7001;
        public const UInt16 E_DAT_InitDataAquisition = 0x7002;
        public const UInt16 E_DAT_SetIntegrationTime = 0x7003;
        public const UInt16 E_DAT_StartDataAcquisition = 0x7004;
        public const UInt16 E_DAT_StopDataAcquisition = 0x7005;
        public const UInt16 E_DAT_PowerAdcOn = 0x7006;
        public const UInt16 E_DAT_PowerAdcOff = 0x7007;
        public const UInt16 E_DAT_OffsetTimeout = 0x7008;

        public const UInt16 E_XRY_NotPresent = 0x8000;
        public const UInt16 E_XRY_FailCreateThread = 0x8001;
        public const UInt16 E_XRY_MissingPrepareInfo = 0x8002;
        public const UInt16 E_XRY_XRayNotOn = 0x8003;
        public const UInt16 E_XRY_SafetyTimeout = 0x8004;
        public const UInt16 E_XRY_DoorOpened = 0x8005;
        public const UInt16 E_XRY_GBoxFatalError = 0x8006;
        public const UInt16 E_XRY_TCUCriticalError = 0x8007;
        public const UInt16 E_XRY_AbortReceived = 0x8008;
        public const UInt16 E_XRY_PrepareMessageLength = 0x8009;
        public const UInt16 E_XRY_IncompletedPrepare = 0x800a;
        public const UInt16 E_XRY_CDWNotReady = 0x800b;
        public const UInt16 E_XRY_TableNotReady = 0x800c;
        public const UInt16 E_XRY_IllegalAnodeSpeed = 0x800e;
        public const UInt16 E_XRY_RatingError = 0x800f;
        public const UInt16 E_XRY_ECGError = 0x8010;
        public const UInt16 E_XRY_WarmupProtocolNotFound = 0x8011;
        public const UInt16 E_XRY_ShortOfViews = 0x8012;
        public const UInt16 E_XRY_GBoxErrorTableNotFound = 0x8013;
        // Illegal Scan Parameter
        public const UInt16 E_XRY_IllegalKv = 0x8101;
        public const UInt16 E_XRY_IllegalMa = 0x8102;
        public const UInt16 E_XRY_IllegalFss = 0x8103;
        public const UInt16 E_XRY_IllegalInputKw = 0x8104;

        public const UInt16 W_XRY_QuickWarmupNeeded = 0x8801;
        public const UInt16 W_XRY_DailyWarmupNeeded = 0x8802;

        public const UInt16 E_CRI_CreateThread = 0x9001;
        public const UInt16 E_CRI_InitMutex = 0x9002;
        public const UInt16 E_CRI_InitCond = 0x9003;
        public const UInt16 E_CRI_OpenFile = 0x9004;

        public const UInt16 E_CRI_Unknown = 0x9999;     // fatal unknown error

        public const UInt16 E_ACA_OPENFILE = 0xa001;
        public const UInt16 E_ACA_GETFILESTATUS = 0xa002;
        public const UInt16 E_ACA_WRITEFILE = 0xa003;
        public const UInt16 E_ACA_READFILE = 0xa004;
        public const UInt16 E_ACA_CLOSEFILE = 0xa005;
        public const UInt16 E_ACA_SEEKFILE = 0xa006;
        public const UInt16 E_ACA_CREATETHREAD = 0xa007;
        public const UInt16 E_ACA_INITMUTEX = 0xa008;
        public const UInt16 E_ACA_INITCOND = 0xa009;
        public const UInt16 E_ACA_OPENSOCKET = 0xa00a;
        public const UInt16 E_ACA_BINDSOCKET = 0xa00b;
        public const UInt16 E_ACA_READSOCKET = 0xa00c;
        public const UInt16 E_ACA_WRITESOCKET = 0xa00d;
        public const UInt16 E_ACA_NOMORECONTENT = 0xa00e;
        public const UInt16 E_ACA_FILENOTOPEN = 0xa00f;
        public const UInt16 E_ACA_TIMENOTSYNCED = 0xa010;
        public const UInt16 E_ACA_STARTMINIONFAIL = 0xa011;
        public const UInt16 E_ACA_FORKFAIL = 0xa012;
        public const UInt16 E_ACA_MINIONSTARTED = 0xa013;
        public const UInt16 E_ACA_SCANDIR = 0xa014;




        public static Dictionary<UInt16, string> ErrorTable = new Dictionary<UInt16, string> {

            { E_SUCCESS , "Success" }, // 0x0000
            { E_ADB_NotPresent , "ADB not present" }, // 0x1000
            { E_ADB_TxNotReady , "ADB Tx not ready" }, // 0x1001
            { E_ADB_RxTimeout , "ADB Rx timeout" }, // 0x1002
            { E_ADB_InvalidTxLen , "ADB invalid Tx length" }, // 0x4003
            { E_ADB_BadResponse , "ADB bad response" }, // 0x1004
            { E_ADB_AuthFailed , "ADB authentication failed" }, // 0x1003
            { E_ADB_AuthTimeout , "ADB authentication timeout" }, // 0x1004

            { E_TCU_NotPresent , "TCU not present" }, // 0x2000
            { E_TCU_TxNotReady , "TCU Tx not ready" }, // 0x2001
            { E_TCU_RxTimeout , "TCU Rx timeout" }, // 0x2002
            { E_TCU_InvalidTxLen , "TCU invalid Tx length" }, // 0x2003
            { E_TCU_BadResponseLength , "TCU bad response length" }, // 0x2004
            { E_TCU_BadResponseCRC , "TCU bad response crc" }, // 0x2005
            { E_TCU_BadResponse , "TCU bad response" }, // 0x2006
            { E_TCU_NackResponse , "TCU NACK response" }, // 0x2007
            { E_TCU_KvNotInRange , "TCU kv not in range" }, // 0x2008
            { E_TCU_MaNotInRange , "TCU ma not in range" }, // 0x2009
            { E_TCU_FocusOn , "TCU focus is on" }, // 0x200a
            { E_TCU_TechniqueNotMatch , "TCU technique not match" }, // 0x200b
            { E_TCU_NotOperational , "TCU not operatoinal" }, // 0x200c
            { E_TCU_NotReady4Shot , "TCU not ready for shot" }, // 0x200d

            { E_GBX_NotPresent , "GBX not present" }, // 0x3000
            { E_GBX_TxNotReady , "GBX Tx not ready" }, // 0x3001
            { E_GBX_NotInStateWC , "GBX not in WC state" }, // 0x3002
            { E_GBX_NotInStateOS , "GBX not in OS state" }, // 0x3003
            { E_GBX_NotInStatePS , "GBX not in PS state" }, // 0x3004
            { E_GBX_InStateER , "GBX in ER state" }, // 0x3005

            { E_COL_NotPresent , "Galil not present" }, // 0x4000
            { E_COL_TxNotReady , "Galil Tx not ready" }, // 0x4001
            { E_COL_RxTimeout , "Galil Rx timeout" }, // 0x4002
            { E_COL_InvalidTxLen , "Galil invalid Tx length" }, // 0x4003
            { E_COL_BadResponse , "Galil bad response" }, // 0x4004
            { E_COL_GalilErrorCodeBase+1 , "Galil error code 1" }, // 0x4101
            { E_COL_GalilErrorCodeBase+2 , "Galil error code 2" }, // 0x4102
            { E_COL_GalilErrorCodeBase+3 , "Galil error code 3" }, // 0x4103
            { E_COL_GalilErrorCodeBase+4 , "Galil error code 4" }, // 0x4104
            { E_COL_GalilErrorCodeBase+5 , "Galil error code 5" }, // 0x4105
            { E_COL_GalilErrorCodeBase+6 , "Galil error code 6" }, // 0x4106
            { E_COL_GalilErrorCodeBase+7 , "Galil error code 7" }, // 0x4107
            { E_COL_GalilErrorCodeBase+8 , "Galil error code 8" }, // 0x4108
            { E_COL_GalilErrorCodeBase+9 , "Galil error code 9" }, // 0x4100

            { E_AGG_NotPresent , "AGG not present" }, // 0x5000
            { E_AGG_TxNotReady , "AGG Tx not ready" }, // 0x5001
            { E_AGG_RxTimeout , "AGG Rx timeout" }, // 0x5002
            { E_AGG_InvalidTxLen , "AGG invalid Tx length" }, // 0x5003
            { E_AGG_CorruptedMessage , "AGG corrupted message" }, // 0x5005

            { E_ENV , "" }, // 0x6000

            { E_DAT_AdcInitAll , "failed to init ADC" }, // 0x7001
            { E_DAT_InitDataAquisition , "failed to init data acquisition" }, // 0x7002
            { E_DAT_SetIntegrationTime , "failed to set integration time" }, // 0x7003
            { E_DAT_StartDataAcquisition , "failed to start data acquisition" }, // 0x7004
            { E_DAT_StopDataAcquisition , "failed to stop acquisition" }, // 0x7005
            { E_DAT_PowerAdcOn , "failed to power on ADC" }, // 0x7006
            { E_DAT_PowerAdcOff , "failed to power off ADC" }, // 0x7007
            { E_DAT_OffsetTimeout , "timed out to collect offset data" }, // 0x7008

            { E_XRY_NotPresent , "XRY not present" }, // 0x8000
            { E_XRY_FailCreateThread , "XRY failed to create thread" }, // 0x8001
            { E_XRY_MissingPrepareInfo , "missing prepare info" }, // 0x8002
            { E_XRY_XRayNotOn , "x-ray not on" }, // 0x8003
            { E_XRY_SafetyTimeout , "safety timer timed out" }, // 0x8004
            { E_XRY_DoorOpened , "door is open" }, // 0x8005
            { E_XRY_GBoxFatalError , "g-box fatal error" }, // 0x8006
            { E_XRY_TCUCriticalError , "tcu critical error" }, // 0x8007
            { E_XRY_AbortReceived , "abort received" }, // 0x8008
            { E_XRY_PrepareMessageLength , "invalid prepare message length" }, // 0x8009
            { E_XRY_IncompletedPrepare , "incompleted prepare message" }, // 0x800a
            { E_XRY_CDWNotReady , "crude data writer not ready" }, // 0x800b
            { E_XRY_TableNotReady , "table not ready" }, // 0x800c
            { E_XRY_IllegalAnodeSpeed , "illegal anode speed" }, // 0x800e
            { E_XRY_RatingError , "rating error" }, // 0x800f
            { E_XRY_ECGError , "ecg not connected" }, // 0x8010
            { E_XRY_WarmupProtocolNotFound , "warm-up protocol not found" }, // 0x8011
            { E_XRY_ShortOfViews , "views short" }, // 0x8012
            { E_XRY_GBoxErrorTableNotFound , "GBox error table not found" }, // 0x8013
            // Illegal Scan Parameter
            { E_XRY_IllegalKv , "illegal kv" }, // 0x8101
            { E_XRY_IllegalMa , "illegal ma" }, // 0x8102
            { E_XRY_IllegalFss , "illegal fss" }, // 0x8103
            { E_XRY_IllegalInputKw , "illegal input kw" }, // 0x8104

            { W_XRY_QuickWarmupNeeded , "warning: quick warm-up needed" }, // 0x8801
            { W_XRY_DailyWarmupNeeded , "warning: daily warm-up needed" }, // 0x8802

            { E_CRI_CreateThread , "failed to create thread" }, // 0x9001
            { E_CRI_InitMutex , "failed to init mutex" }, // 0x9002
            { E_CRI_InitCond , "failed to init cond" }, // 0x9003
            { E_CRI_OpenFile , "failed to open file" }, // 0x9004

            { E_CRI_Unknown , "fatal unknown error" }, // 0x9999     // fatal unknown error

            { E_ACA_OPENFILE , "ACA failed to open file" }, // 0xa001
            { E_ACA_GETFILESTATUS , "ACA failed to get file status" }, // 0xa002
            { E_ACA_WRITEFILE , "ACA failed to write file" }, // 0xa003
            { E_ACA_READFILE , "ACA failed to read file" }, // 0xa004
            { E_ACA_CLOSEFILE , "ACA failed to close file" }, // 0xa005
            { E_ACA_SEEKFILE , "ACA failed to seek file" }, // 0xa006
            { E_ACA_CREATETHREAD , "ACA failed to create thread" }, // 0xa007
            { E_ACA_INITMUTEX , "ACA failed to init mutex" }, // 0xa008
            { E_ACA_INITCOND , "ACA failed to init cond" }, // 0xa009
            { E_ACA_OPENSOCKET , "ACA failed to open socket" }, // 0xa00a
            { E_ACA_BINDSOCKET , "ACA failed to bind socket" }, // 0xa00b
            { E_ACA_READSOCKET , "ACA failed to read socket" }, // 0xa00c
            { E_ACA_WRITESOCKET , "ACA failed to write socket" }, // 0xa00d
            { E_ACA_NOMORECONTENT , "ACA no more content" }, // 0xa00e
            { E_ACA_FILENOTOPEN , "ACA file not open" }, // 0xa00f
            { E_ACA_TIMENOTSYNCED , "ACA time not synced" }, // 0xa010
            { E_ACA_STARTMINIONFAIL , "ACA failed to start minion" }, // 0xa011
            { E_ACA_FORKFAIL , "ACA failed to fork child" }, // 0xa012
            { E_ACA_MINIONSTARTED , "ACA minion has started" }, // 0xa013
            { E_ACA_SCANDIR , "ACA failed to scan directory" } // 0xa014
        };

        public static string GetErrorString(UInt16 errorCode) {

            string message = "unknown error";

            ErrorTable.TryGetValue(errorCode, out message);

            return message;
        }
    }
}
