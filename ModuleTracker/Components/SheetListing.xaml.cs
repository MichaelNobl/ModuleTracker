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
    /// Interaction logic for SheetListing.xaml
    /// </summary>
    public partial class SheetListing : UserControl
    {
        public SheetListing()
        {
            InitializeComponent();
        }

        #region DependencyProperties

        public ICommand SheetItemInsertetCommand
        {
            get { return (ICommand)GetValue(SheetItemInsertetCommandProperty); }
            set { SetValue(SheetItemInsertetCommandProperty, value); }
        }

        public static readonly DependencyProperty SheetItemInsertetCommandProperty =
            DependencyProperty.Register("SheetItemInsertetCommand", typeof(ICommand), typeof(SheetListing), new PropertyMetadata(null));

        public object TargetSheetItem
        {
            get { return (object)GetValue(TargetSheetItemProperty); }
            set { SetValue(TargetSheetItemProperty, value); }
        }

        public static readonly DependencyProperty TargetSheetItemProperty =
            DependencyProperty.Register("TargetSheetItem", typeof(object), typeof(SheetListing), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object InsertetSheetItem
        {
            get { return (object)GetValue(InsertetSheettemProperty); }
            set { SetValue(InsertetSheettemProperty, value); }
        }

        public static readonly DependencyProperty InsertetSheettemProperty =
            DependencyProperty.Register("InsertetSheetItem", typeof(object), typeof(SheetListing), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        #endregion

        #region Events

        private void SheetItem_DragOver(object sender, DragEventArgs e)
        {
            if (SheetItemInsertetCommand?.CanExecute(null) ?? false)
            {
                if (sender is FrameworkElement element)
                {
                    TargetSheetItem = element.DataContext;
                    InsertetSheetItem = e.Data.GetData(DataFormats.Serializable);

                    SheetItemInsertetCommand.Execute(null);
                }
            }
        }

        private void SheetItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is FrameworkElement element)
            {
                object sheetItem = element.DataContext;

                DragDrop.DoDragDrop(element,
                    new DataObject(DataFormats.Serializable, sheetItem),
                    DragDropEffects.Move);
            }
        }

        #endregion
    }
}
