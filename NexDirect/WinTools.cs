﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NexDirect
{
    static class WinTools
    {
        // https://stackoverflow.com/questions/3120616/wpf-datagrid-selected-row-clicked-event sol #2
        static public object getGridViewSelectedRowItem(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
            if (row == null) return null;
            return row.Item;
        }
    }
}
