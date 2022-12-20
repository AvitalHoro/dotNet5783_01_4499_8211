using BLApi;
using BO;
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
using System.Windows.Shapes;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for TrackingOrder.xaml
    /// </summary>
    public partial class TrackingOrder : Window
    {
        IBl bl;
        public TrackingOrder(IBl BL, OrderForList order)
        {
            InitializeComponent();
            bl = BL;
        }
    }
}
