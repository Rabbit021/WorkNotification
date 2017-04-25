using System.Windows;
using System.Windows.Controls;

namespace Alarm.Control
{
    public class ListViewItemStyleSelector : StyleSelector
    {
        Style ListViewItemOdd = null;
        Style ListViewItemEven = null;
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (ListViewItemOdd == null) ListViewItemOdd = Application.Current.Resources["ListViewItemOdd"] as Style;
            if (ListViewItemEven == null) ListViewItemEven = Application.Current.Resources["ListViewItemEven"] as Style;
            Style rst = null;
            ListView listView = ItemsControl.ItemsControlFromItemContainer(container) as ListView;
            int index = listView.ItemContainerGenerator.IndexFromContainer(container);
            if (index % 2 == 0)
            {
                rst = ListViewItemOdd;
            }
            else
            {
                rst = ListViewItemEven;
            }
            return rst;
        }
    }
}