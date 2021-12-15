using REghZyMVVM.IOC;
using REghZyMVVM.Service;
using REghZyMVVM.ViewModels.Base;

namespace REghZyNotepad.Core {
    public static class IoC {
        private static readonly ManagedIoC _ioc = new ManagedIoC();

        /// <summary>
        /// Gets the ViewModel instance of the given type
        /// </summary>
        /// <typeparam name="T">The ViewModel type</typeparam>
        /// <returns>The instance of the ViewModel</returns>
        /// <exception cref="ViewModelNotFoundException">Thrown if there isn't a ViewModel of that type</exception>
        /// <exception cref="InvalidCastException">Thrown if the target ViewModel type doesn't match the actual ViewModel type (somehow)</exception>
        public static T GetViewModel<T>() where T : BaseViewModel {
            return _ioc.GetViewModel<T>();
        }

        /// <summary>
        /// Gets the service instance of the given generic type
        /// </summary>
        /// <typeparam name="T">The service type (typically the base type)</typeparam>
        /// <returns>The instance of the service</returns>
        /// <exception cref="ServiceNotFoundException">Thrown if there isn't a ViewModel of that type</exception>
        /// <exception cref="InvalidCastException">Thrown if the target service type doesn't match the actual service type</exception>
        public static T GetService<T>() where T : IService {
            return _ioc.GetService<T>();
        }

        /// <summary>
        /// Registers (or replaces) a viewmode of the given generic type
        /// </summary>
        /// <typeparam name="T">The ViewModel type</typeparam>
        /// <param name="viewModel"></param>
        public static void SetViewModel<T>(T viewModel) where T : BaseViewModel {
            _ioc.SetViewModel<T>(viewModel);
        }

        /// <summary>
        /// Registers (or replaces) the given service of the given generic type
        /// </summary>
        /// <typeparam name="T">The service type (typically an interface, for an API service)</typeparam>
        /// <param name="service"></param>
        public static void SetService<T>(T service) where T : IService {
            _ioc.SetService<T>(service);
        }

        /// <summary>
        /// Returns whether this IoC manager contains a given ViewModel
        /// </summary>
        /// <typeparam name="T">The viewmodel type</typeparam>
        /// <returns></returns>
        public static bool HasViewModel<T>() where T : BaseViewModel {
            return _ioc.HasViewModel<T>();
        }

        /// <summary>
        /// Returns whether this IoC manager contains a given service
        /// </summary>
        /// <typeparam name="T">The service type</typeparam>
        /// <returns></returns>
        public static bool HasService<T>() where T : IService {
            return _ioc.HasService<T>();
        }
    }
}
