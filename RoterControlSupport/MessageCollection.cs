using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

namespace RoterControlSupport {
    class MessageCollection {

        private AutoResetEvent EndReceivedEvent;
        private BlockingCollection<FmiCanFrame> FrameQueue;

        public MessageCollection() {

            EndReceivedEvent = new AutoResetEvent(false);
            FrameQueue = new BlockingCollection<FmiCanFrame>();
        }

        public void Set() {

            EndReceivedEvent.Set();
        }

        public void Reset() {

            EndReceivedEvent.Reset();
        }

        public bool WaitOne(int p_ms) {

            return EndReceivedEvent.WaitOne(p_ms);
        }

        public void Add(FmiCanFrame p_fmi_frame) {

            FrameQueue.Add(p_fmi_frame);
        }

        public bool TryTake(out FmiCanFrame p_fmi_frame) {

            return FrameQueue.TryTake(out p_fmi_frame);
        }

        public FmiCanFrame Take() {

            return FrameQueue.Take();
        }

        public void Clear() {

            FmiCanFrame item;

            while (FrameQueue.TryTake(out item))
                ;
        }
    }
}
