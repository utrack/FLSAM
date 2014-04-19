using System;
using System.Windows.Forms;

namespace WaitWindow
{
    public partial class Window : Form
    {

        public event EventHandler CancelPressed;
        private readonly Timer _timer;
        private readonly Form _ownerForm;
        public Window(Form parentForm,
            Action<EventHandler> eventFinished, Action<EventHandler> eventCancelled,
            int timeout = 0,bool supportsCancellation = false)
        {
            InitializeComponent();
            GC.KeepAlive(this);
            button1.Visible = supportsCancellation;

            StartPosition = FormStartPosition.CenterParent;
            eventFinished(EventFinished);
            eventCancelled(EventCancelled);

            _ownerForm = parentForm;
            
            _timer = new Timer {Interval = timeout};
            _timer.Tick += _timer_Tick;
            _timer.Start();

            if (supportsCancellation)
                button1.Visible = true;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Enabled = false;
            _timer.Stop();
            if (IsDisposed) return;
            ShowDialog(_ownerForm);
            
        }


        public void EventFinished(object o,EventArgs e)
        {
            if (InvokeRequired)
            {
                Action<object, EventArgs> ac = EventFinished;
                Invoke(ac);
                return;
            }
            

            Close();
            Dispose();
        }

        public void EventCancelled(object o, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CancelPressed != null)
                CancelPressed(sender,e);
        }
    }
}
