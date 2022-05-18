using AndroidX.Lifecycle;
using Java.Lang;

namespace Network.Droid.ui
{
    public class NetworkViewModelFactory : ViewModelProvider.IFactory
    {
        public <T : ViewModel> T Create(Class<T> modelClass)
        {
            if (modelClass.isAssignableFrom(Validation.class)) {
                return (T) new Validation(NetworkRepository.getInstance(new NetworkDataSource()));
            } else {
                throw new IllegalArgumentException("Unknown ViewModel class");
            }
        }
    }
}