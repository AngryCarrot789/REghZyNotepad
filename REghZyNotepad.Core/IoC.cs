using System;
using System.Collections.Generic;
using REghZyNotepad.Core.Exceptions;
using REghZyNotepad.Core.ViewModels.Base;

namespace REghZyNotepad.Core {
    public static class IoC {
        private static Dictionary<Type, BaseViewModel> _viewModels;
        private static Dictionary<Type, object> _services;

        static IoC() {
            _viewModels = new Dictionary<Type, BaseViewModel>();
            _services = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Gets the singleton ViewModel instance of the given type
        /// </summary>
        /// <typeparam name="T">The ViewModel type</typeparam>
        /// <returns>The instance of the ViewModel</returns>
        /// <exception cref="NoSuchViewModelException">Thrown if there isn't a ViewModel of that type</exception>
        public static T GetVM<T>() where T : BaseViewModel {
            if (_viewModels.TryGetValue(typeof(T), out BaseViewModel viewModel)) {
                return (T)viewModel;
            }

            throw new NoSuchViewModelException(typeof(T));
        }

        /// <summary>
        /// Gets the singleton service instance of the given generic type
        /// </summary>
        /// <typeparam name="T">The service type (typically the base type)</typeparam>
        /// <returns>The instance of the service</returns>
        /// <exception cref="NoSuchViewModelException">Thrown if there isn't a ViewModel of that type</exception>
        /// <exception cref="InvalidCastException">Thrown if the target service type doesn't match the actual service type</exception>
        public static T GetService<T>() {
            if (_services.TryGetValue(typeof(T), out object service)) {
                if (service is T t) {
                    return t;
                }

                throw new InvalidCastException($"The target service type '{typeof(T).Name}' is incompatiable with actual service type '{service.GetType().Name}'");
            }

            throw new NoSuchServiceException(typeof(T));
        }

        /// <summary>
        /// Registers (or replaces) a viewmode of the given generic type
        /// </summary>
        /// <typeparam name="T">The ViewModel type</typeparam>
        /// <param name="viewModel"></param>
        public static void SetViewModel<T>(T viewModel) where T : BaseViewModel {
            _viewModels[typeof(T)] = viewModel;
        }

        /// <summary>
        /// Registers (or replaces) the given service of the given generic type
        /// </summary>
        /// <typeparam name="T">The service type (typically an interface, for an API service)</typeparam>
        /// <param name="service"></param>
        public static void SetService<T>(object service) {
            _services[typeof(T)] = service;
        }
    }
}
