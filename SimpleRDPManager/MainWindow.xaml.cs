using SimpleRDPManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (SimpleRDPManager.Properties.Settings.Default.Groups == null || !SimpleRDPManager.Properties.Settings.Default.Groups.Any())
                SimpleRDPManager.Properties.Settings.Default.Upgrade();
        }

        public ObservableCollection<ConnectionGroup> Groups { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Groups = SimpleRDPManager.Properties.Settings.Default.Groups ?? new ObservableCollection<ConnectionGroup>();


            tree.ItemsSource = Groups;
            txtVersion.Text = this.GetType().Assembly.GetName().Version.ToString();

        }

        private void tree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Connect(null,null);
        }

        private void tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //(wHost.Child as System.Windows.Forms.PropertyGrid).SelectedObject = tree.SelectedItem;
        }

        private void AddGroup(object sender, RoutedEventArgs e)
        {
            Groups.Add(new ConnectionGroup() { Name = "Новая группа" });
        }

        private void AddConnection(object sender, RoutedEventArgs e)
        {
            if (tree.SelectedItem is ConnectionGroup)
            {
                (tree.SelectedItem as ConnectionGroup).Connections.Add(new Connection() { Name = "Новое соединение" });
               
            }
        }

        private void RemoveConnection(object sender, RoutedEventArgs e)
        {
            if (tree.SelectedItem is Connection && System.Windows.Forms.MessageBox.Show("Удалить соединение?", "Удалить соединение?", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Stop, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                var connection = (tree.SelectedItem as Connection);
                Groups.First(g => g.Connections.Contains(connection)).Connections.Remove(connection);
            }
        }

        private void DeleteGroup(object sender, RoutedEventArgs e)
        {
            if (tree.SelectedItem is ConnectionGroup && System.Windows.Forms.MessageBox.Show("Удалить группу соединений со всеми соединениями?", "Удалить группу?", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Stop, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                Groups.Remove(tree.SelectedItem as ConnectionGroup);

            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            SimpleRDPManager.Properties.Settings.Default.Groups = null;
            SimpleRDPManager.Properties.Settings.Default.Save();
            SimpleRDPManager.Properties.Settings.Default.Reset();
            SimpleRDPManager.Properties.Settings.Default.Reload();

            SimpleRDPManager.Properties.Settings.Default.Groups = Groups;
            SimpleRDPManager.Properties.Settings.Default.Save();
        }

        private void Connect(object sender, RoutedEventArgs e)
        {
            if (tree.SelectedItem is Connection)
            {
                var connection = (tree.SelectedItem as Connection);
                var group = Groups.First(g => g.Connections.Contains(connection));

                WindowInteropHelper windowInteropHelper = new WindowInteropHelper(this);
                var screen = System.Windows.Forms.Screen.FromHandle(windowInteropHelper.Handle);
               

                new window(connection, group, screen).Show();
            }
        }
    }
}
