using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.ComponentModel;

namespace Core.General
{
    public class BindingListExAutoSort<T> : BindingListEx<T>
    {
        public BindingListExAutoSort(string nProperty)
        {
            this.theSortedPropertyName = nProperty;
            this.theSortedProperty = FindPropertyDescriptor(nProperty);
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItemSorted(index, item, this.theSortedProperty);
        }

        // Public Overloads Sub ResetItem(ByVal index As Integer)
        // MyBase.ResetItem(index)
        // End Sub

        protected override void OnListChanged(System.ComponentModel.ListChangedEventArgs e)
        {
            // If e.ListChangedType = ListChangedType.ItemChanged AndAlso e.PropertyDescriptor IsNot Nothing AndAlso e.PropertyDescriptor.Name = Me.theSortedPropertyName Then
            // Dim obj As Object = Me.Items(e.NewIndex)
            // MyBase.ApplySortCore(Me.theSortedProperty, ListSortDirection.Ascending)
            // Dim aEventArgs As New ListChangedEventArgs(ListChangedType.ItemMoved, Me.IndexOf(CType(obj, T)), e.NewIndex)
            // MyBase.OnListChanged(aEventArgs)
            // Else
            // MyBase.OnListChanged(e)
            // End If
            // ======
            // NOTE: Raise an extra new event, ItemMoved, so that widgets can know when an item moved because of auto-sorting.
            if (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor != null && e.PropertyDescriptor.Name == this.theSortedPropertyName)
            {
                object obj = this.Items[e.NewIndex];
                this.Items.RemoveAt(e.NewIndex);
                int insertionIndex;
                insertionIndex = this.FindInsertionIndex(0, (T)obj, this.theSortedProperty);
                this.Items.Insert(insertionIndex, (T)obj);
                ListChangedEventArgs aEventArgs = new ListChangedEventArgs(ListChangedType.ItemMoved, insertionIndex, e.NewIndex);
                base.OnListChanged(aEventArgs);
            }

            base.OnListChanged(e);
        }

        private string theSortedPropertyName;
        private PropertyDescriptor theSortedProperty;
    }
}
