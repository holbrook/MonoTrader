using Tangle.PluginModel;
using System.Timers;
using TangleTrading.Adapter;

namespace Plugin1
{
    [Broker("Mock适配器", typeof(MockConfig1))]
    public class MockBroker : IPart
    {
        public IPartContext Context { get; set; }

        private Timer _timer = new Timer();

        public void Initialize(dynamic param)
        {
            _timer.Elapsed += new ElapsedEventHandler(Timer1_Elapsed);
        }

        public void Start()
        {
            _timer.Enabled = true;
            _timer.Interval = 2000;//执行间隔时间,单位为毫秒
            _timer.Start();
        }


        private int _count = 0;

        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            Context.Logger.Info("Part1 send Apple");
            Context.Publish(new Apple());
            _count++;
            if (_count > 5)
                _timer.Stop();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
