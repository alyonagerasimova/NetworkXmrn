namespace Network.Droid.ui
{
    public class LoggedInNetworkView
    {
        private string displayName;
        //... other data fields that may be accessible to the UI

        LoggedInNetworkView(string displayName) {
            this.displayName = displayName;
        }

        string getDisplayName() {
            return displayName;
        }
    }
}