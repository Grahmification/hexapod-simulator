using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Hexapod_Simulator
{
    public class Simulation
    {

        private bool _running = false;
        private ManualResetEvent _finishedStop = new ManualResetEvent(false); //flag to notify that all running loops have ended
        private ManualResetEvent _workDoneHandle = new ManualResetEvent(false); //flag to notify foreground work has been finished for cycle, must be set after doing work from the eventArgs
        private double _timeIncrement = 30; //simulation time increment in ms
        private StopWatchPrecision _sw = new StopWatchPrecision(); //keeps track of simulation time
        private int _cycleCount = 0; 
        

        public event EventHandler<SimulationTimeStepEventArgs> SimulationDoWorkRequest;
        public event EventHandler<int> RunFreqUpdated;
 
        public bool Running
        {
            get { return _running; }
        }

        public void Start(double timeIncrement)
        {
            var RequestCallBack = new Progress<int>(s => this.runDoWorkCallback());
            var FPSCallBack = new Progress<int>(s => this.FrequencyCallback(s));
            
            _timeIncrement = timeIncrement;          
            _running = true;
            _cycleCount = 0; 
            _finishedStop.Reset();

            _sw.StartNew(); //log starting time

            Task.Factory.StartNew(() => runDoWork(FPSCallBack, RequestCallBack), TaskCreationOptions.LongRunning);        
        }
        public void Stop()
        {
            this._running = false; //set flag to stop

            _workDoneHandle.Set(); //don't need to wait anymore
            _finishedStop.WaitOne(); //loops still running, need to wait            

            //----------- now safely run clean up code -------------

            _sw.Stop();
        }



        private void runDoWork(IProgress<int> FreqCallBack, IProgress<int> RequestCallBack)
        {                      
            while (this._running == true)
            {
                double loopStartTime = _sw.ElapsedMilliseconds;
                double loopEndTime = loopStartTime + this._timeIncrement; //time loop should be ending in ms

                //-------------- Do work here ---------------------------------

                //if (SimulationDoWorkRequest != null) //raise event if there are subscribers
                    //SimulationDoWorkRequest(this, new SimulationTimeStepEventArgs(_timeIncrement/1000.0, _sw.ElapsedMilliseconds));
                
                _workDoneHandle.Reset();
                RequestCallBack.Report(-1); //allows work to be done in foreground thread
                _workDoneHandle.WaitOne(); //wait for work to be done
      
                // ------------- sleep if needed ------------------------------

                if (_sw.ElapsedMilliseconds < loopEndTime)
                { Thread.Sleep((int)(loopEndTime - _sw.ElapsedMilliseconds)); }

                // ------------- report operating frequency ------------------------------

                double freq = 1000.0 / (_sw.ElapsedMilliseconds - loopStartTime);
                //FreqCallBack.Report((int)freq); //seems to make simulation slow. Trigger every 10 cycles?
                

                _cycleCount++;
            }
                

            _finishedStop.Set(); //all running loops have ended
        }
        private void runDoWorkCallback()
        {
            if (SimulationDoWorkRequest != null) //raise event if there are subscribers
                SimulationDoWorkRequest(this, new SimulationTimeStepEventArgs(_timeIncrement / 1000.0, _sw.ElapsedMilliseconds, _workDoneHandle));
        } //makes event get raised in foreground thread
        private void FrequencyCallback(int runFrequency)
        {
            if (RunFreqUpdated != null) //raise event if there are subscribers
                RunFreqUpdated(this, runFrequency);
        } //makes event get raised in foreground thread

    }
    public class SimulationTimeStepEventArgs
    {
        public double TimeIncrement { get; private set; } // [s]
        public double Time { get; private set; } // [s]

        public ManualResetEvent WorkDoneCallback { get; private set; }
        public SimulationTimeStepEventArgs(double timeIncrement, double time, ManualResetEvent workDoneCallBack)
        {
            this.TimeIncrement = timeIncrement;
            this.Time = time;
            this.WorkDoneCallback = workDoneCallBack; 
        }
    }




    public class StopWatchPrecision
    {
        //Created because the timer operates much more accurately working in ticks than ms

        private Stopwatch _sw = new Stopwatch();
        private double _freq = Stopwatch.Frequency / 1000.0; //number of milliseconds in one timer tick

        public bool IsRunning { get { return _sw.IsRunning; } }
        public double ElapsedMilliseconds { get { return this._sw.ElapsedTicks / _freq; } }
        public double ElapsedSeconds { get { return this._sw.ElapsedTicks / (_freq * 1000.0); } }

        public StopWatchPrecision()
        {
            _sw = new Stopwatch();
            _freq = Stopwatch.Frequency / 1000.0;
        }

        public void StartNew()
        {
            _sw = Stopwatch.StartNew();
        }
        public void Stop()
        {
            _sw.Stop();
        }

    }

}
