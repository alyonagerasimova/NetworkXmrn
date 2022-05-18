using System;

namespace Network.Droid.ui
{
    public class NetworkResult
    {
        @Nullable
        private LoggedInNetworkView success;
        @Nullable
        private int error;

        NetworkResult(@Nullable int error) {
            this.error = error;
        }

        NetworkResult(@Nullable LoggedInNetworkView success) {
            this.success = success;
        }

        @Nullable
            LoggedInNetworkView getSuccess() {
            return success;
        }

        @Nullable
            int getError() {
            return error;
        }
    }
}