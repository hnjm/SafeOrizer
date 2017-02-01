namespace SafeOrizer.Droid.Helpers
{
    public class LogTraceListener : System.Diagnostics.TraceListener
    {
        public override void Write(string message) => 
            Android.Util.Log.WriteLine(Android.Util.LogPriority.Debug, nameof(SafeOrizer), message);

        public override void WriteLine(string message) => 
            Android.Util.Log.WriteLine(Android.Util.LogPriority.Debug, nameof(SafeOrizer), message);
    }

}