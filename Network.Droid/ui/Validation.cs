using Android.Widget;
using AndroidX.Lifecycle;

namespace Network.Droid.ui
{
    public class Validation
    {
         public static MutableLiveData getLoginFormState() {
        return loginFormState;
    }

    public static MutableLiveData<NetworkFormState> loginFormState = new MutableLiveData<>();
    private NetworkRepository networkRepository;

    public Validation(NetworkRepository networkRepository) {
        this.networkRepository = networkRepository;
    }

    public void network(string numberHidden, string numberCycle, string learningRate, string error) {
        // can be launched in a separate asynchronous job
        Result<LoggedInNetwork> result = networkRepository.network(numberHidden, numberCycle, learningRate, error);

        if (result is Result.Success) {
            LoggedInNetwork data = ((Result.Success<LoggedInNetwork>) result).getData();
        }
    }

    public static void NetworkDataChanged(string numberHidden, string numberCycle, string learningRate, string error) {
        if (!isNumberHiddenValid(numberHidden)) {
            loginFormState.SetValue(new NetworkFormState(Resource.String.invalid_number_hidden, null, null, null));
        } else if (!isLearningRate(learningRate)) {
            loginFormState.SetValue(new NetworkFormState(null, Resource.String.invalid_learning_rate, null, null));
        } else if (!isNumberCycleValid(numberCycle) && error.isEmpty()) {
            loginFormState.SetValue(new NetworkFormState(null, null, Resource.String.invalid_number_cycle, null));
        } else if (isNumberCycleValid(numberCycle) && !error.isEmpty()) {
            loginFormState.SetValue(new NetworkFormState(null, null, Resource.String.invalid_number_cycle_and_error, null));
        } else if (!isError(error) && numberCycle.Trim().Length() == 0) {
            loginFormState.SetValue(new NetworkFormState(null, null, null, Resource.String.invalid_error));
        } else {
            loginFormState.SetValue(new NetworkFormState(null, null, null, null));
            loginFormState.SetValue(new NetworkFormState(true));
        }
        //loginFormState.setValue(new NetworkFormState(null, null, null, null));
    }

    public static bool isNetworkImage(ImageView imageView) {
        if (imageView.GetDrawableState() == null) {
            loginFormState.SetValue(new NetworkFormState(Resource.String.invalid_image));
        } else {
            loginFormState.SetValue(new NetworkFormState(1));
            return true;
        }
        return false;
    }

    public static bool isNumberHiddenValid(string numberHidden) {
        if (!numberHidden.matches("[0-9]+") || int.Parse(numberHidden) > 500
                || int.Parse(numberHidden) < 0) {
            return false;
        }
        return int.Parse(numberHidden) >= 26;
    }

    public static bool isLearningRate(string learningRate) {
        if (!learningRate.matches("[0-9]+[\\.\\,][0-9]+") || double.Parse(learningRate) > 1.0
                || double.Parse(learningRate) < 0) {
            return false;
        }
        return double.Parse(learningRate) > 0;
    }

    public static bool isNumberCycleValid(string numberCycle) {
        if ((!numberCycle.matches("[0-9]+") || int.Parse(numberCycle) > 100
                || int.Parse(numberCycle) < 0)) {
            return false;
        }
        return int.Parse(numberCycle) > 0;
    }

    public static bool isError(string error) {
        if (!error.Equals("[0-9]+[\\.][0-9]+") || double.Parse(error) > 10.0
                || double.Parse(error) < 0) {
            return false;
        }
        return double.Parse(error) > 0;
    }
    }
}