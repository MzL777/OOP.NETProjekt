using System;
using System.Collections;
using System.Windows.Forms;

namespace WinForms
{
    class ListViewItemComparer : IComparer
    {
        private static int columnIndex = 0;
        private static bool sortingOrder = true;

        public ListViewItemComparer(int column)
        {
            if (column == columnIndex)
            {
                sortingOrder = !sortingOrder;
            }
            columnIndex = column;
        }
        public int Compare(object x, object y)
        {
            return (sortingOrder ? -1 : 1) * String.Compare(((ListViewItem)x).SubItems[columnIndex].Text, ((ListViewItem)y).SubItems[columnIndex].Text);
        }
    }
}
