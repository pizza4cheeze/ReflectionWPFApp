using ReflectionWPFApp.ViewModels;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

using System.Windows;

< Window x: Class = "ReflectionWPFApp.Views.MainWindow"
        xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns: x = "http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns: d = "http://schemas.microsoft.com/expression/blend/2008"
        xmlns: mc = "http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns: local = "clr-namespace:ReflectionWPFApp.Views"
        xmlns: viewModel = "clr-namespace:ReflectionWPFApp.ViewModels"
        mc: Ignorable = "d"
        Title = "Reflection WPF App" Height = "450" Width = "800" >
    < Window.DataContext >
        < viewModel:MainViewModel />
    </ Window.DataContext >
    < Grid >
        < StackPanel Margin = "10" >
            < TextBox Text = "{Binding LibraryPath, UpdateSourceTrigger=PropertyChanged}" Width = "300" Margin = "5" PlaceholderText = "Enter path to the library" />
            < Button Content = "Load Library" Command = "{Binding LoadLibraryCommand}" Width = "100" Margin = "5" />
            < ComboBox ItemsSource = "{Binding Types}" SelectedItem = "{Binding SelectedType}" DisplayMemberPath = "Name" Width = "300" Margin = "5" />
            < ComboBox ItemsSource = "{Binding Methods}" SelectedItem = "{Binding SelectedMethod}" DisplayMemberPath = "Name" Width = "300" Margin = "5" />
            < ItemsControl ItemsSource = "{Binding Parameters}" >
                < ItemsControl.ItemTemplate >
                    < DataTemplate >
                        < TextBox Text = "{Binding}" Width = "200" Margin = "5" />
                    </ DataTemplate >
                </ ItemsControl.ItemTemplate >
            </ ItemsControl >
            < Button Content = "Execute Method" Command = "{Binding ExecuteMethodCommand}" Width = "100" Margin = "5" />
        </ StackPanel >
    </ Grid >
</ Window >
