using System;

namespace Network.Droid.ui
{
    public class NetworkFormState
    {
        public static void setNumberHiddenError(int numberHiddenError) {
            NetworkFormState.numberHiddenError = numberHiddenError;
        }

        private static int numberHiddenError;

        public static void setNumberCycleError(int numberCycleError) {
            NetworkFormState.numberCycleError = numberCycleError;
        }

        private static int numberCycleError;

        public static void setLearningRateError(int learningRateError) {
            NetworkFormState.learningRateError = learningRateError;
        }

        private static int learningRateError;

        public static void setError(int error) {
            NetworkFormState.error = error;
        }

        private static int error;
        private bool isDataValid;

        public static int getIsImage() {
            return isImage;
        }

        private static int isImage;

        public NetworkFormState(int numberHiddenError, int learningRateError,
        int numberCycleError, int error) {
            this.numberHiddenError = numberHiddenError;
            this.learningRateError = learningRateError;
            this.numberCycleError = numberCycleError;
            this.error = error;
            this.isDataValid = false;
        }

        NetworkFormState(bool isDataValid) {
            this.isDataValid = isDataValid;
        }

        NetworkFormState(int isImage) {
            NetworkFormState.isImage = isImage;
        }

        @Nullable
        public static int getNumberHiddenError() {
            return numberHiddenError;
        }

        public static int getNumberCycleError() {
            return numberCycleError;
        }

        public static int getError() {
            return error;
        }

        @Nullable
        public static int getLearningRate() {
            return learningRateError;
        }

        public bool isDataValid() {
            return isDataValid;
        }
    }
}