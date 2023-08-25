using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModuleTracker.Wpf.Components
{
    /// <summary>
    /// Interaction logic for ModulesListing.xaml
    /// </summary>
    public partial class ModulesListing : UserControl
    {
        public ModulesListing()
        {
            InitializeComponent();
        }

        #region DependencyProperties

        public ICommand ModuleItemInsertetCommand
        {
            get { return (ICommand)GetValue(ModuleItemInsertetCommandProperty); }
            set { SetValue(ModuleItemInsertetCommandProperty, value); }
        }

        public static readonly DependencyProperty ModuleItemInsertetCommandProperty =
            DependencyProperty.Register("ModuleItemInsertetCommand", typeof(ICommand), typeof(ModulesListing), new PropertyMetadata(null));

        public object TargetModuleItem
        {
            get { return (object)GetValue(TargetModuleItemProperty); }
            set { SetValue(TargetModuleItemProperty, value); }
        }

        public static readonly DependencyProperty TargetModuleItemProperty =
            DependencyProperty.Register("TargetModuleItem", typeof(object), typeof(ModulesListing), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object InsertetModuleItem
        {
            get { return (object)GetValue(InsertetModuleItemProperty); }
            set { SetValue(InsertetModuleItemProperty, value); }
        }

        public static readonly DependencyProperty InsertetModuleItemProperty =
            DependencyProperty.Register("InsertetModuleItem", typeof(object), typeof(ModulesListing), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        #endregion

        #region Events

        private void ModuleItem_DragOver(object sender, DragEventArgs e)
        {
            if(ModuleItemInsertetCommand?.CanExecute(null) ?? false)
            {
                if(sender is FrameworkElement element)
                {
                    TargetModuleItem = element.DataContext;
                    InsertetModuleItem = e.Data.GetData(DataFormats.Serializable);

                    ModuleItemInsertetCommand.Execute(null);
                }     
            }
        }

        private void ModuleItem_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && sender is FrameworkElement element) 
            {
                object moduleItem = element.DataContext;

                DragDrop.DoDragDrop(element,
                    new DataObject(DataFormats.Serializable, moduleItem),
                    DragDropEffects.Move);
            }
        }

        #endregion
    }
}
