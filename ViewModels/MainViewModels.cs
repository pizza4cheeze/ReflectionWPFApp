using System;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace ReflectionWPFApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _libraryPath;
        private Type _selectedType;
        private MethodInfo _selectedMethod;
        private object _selectedObject;
        private ObservableCollection<MethodInfo> _methods;
        private ObservableCollection<Type> _types;
        private ObservableCollection<object> _parameters;

        public string LibraryPath
        {
            get => _libraryPath;
            set
            {
                _libraryPath = value;
                OnPropertyChanged();
                LoadTypes();
            }
        }

        public ObservableCollection<Type> Types
        {
            get => _types;
            set
            {
                _types = value;
                OnPropertyChanged();
            }
        }

        public Type SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged();
                LoadMethods();
            }
        }

        public ObservableCollection<MethodInfo> Methods
        {
            get => _methods;
            set
            {
                _methods = value;
                OnPropertyChanged();
            }
        }

        public MethodInfo SelectedMethod
        {
            get => _selectedMethod;
            set
            {
                _selectedMethod = value;
                OnPropertyChanged();
                LoadParameters();
            }
        }

        public ObservableCollection<object> Parameters
        {
            get => _parameters;
            set
            {
                _parameters = value;
                OnPropertyChanged();
            }
        }

        public ICommand ExecuteMethodCommand { get; }

        public MainViewModel()
        {
            Types = new ObservableCollection<Type>();
            Methods = new ObservableCollection<MethodInfo>();
            Parameters = new ObservableCollection<object>();
            ExecuteMethodCommand = new RelayCommand(ExecuteMethod);
        }

        private void LoadTypes()
        {
            if (File.Exists(LibraryPath))
            {
                Assembly assembly = Assembly.LoadFrom(LibraryPath);
                var types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(AnimalLibrary.LivingCreature)));
                Types.Clear();
                foreach (var type in types)
                {
                    Types.Add(type);
                }
            }
        }

        private void LoadMethods()
        {
            if (SelectedType != null)
            {
                Methods.Clear();
                var methods = SelectedType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (var method in methods)
                {
                    Methods.Add(method);
                }
            }
        }

        private void LoadParameters()
        {
            if (SelectedMethod != null)
            {
                Parameters.Clear();
                foreach (var parameter in SelectedMethod.GetParameters())
                {
                    Parameters.Add(Activator.CreateInstance(parameter.ParameterType));
                }
            }
        }

        private void ExecuteMethod()
        {
            if (SelectedMethod != null && SelectedType != null)
            {
                _selectedObject = Activator.CreateInstance(SelectedType);
                SelectedMethod.Invoke(_selectedObject, Parameters.ToArray());
            }
        }
    }
}

