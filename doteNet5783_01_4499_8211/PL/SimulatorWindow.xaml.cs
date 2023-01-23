using BLApi;
using BO;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PO;
using System.ComponentModel;
using System.Threading;
using DO;
using MaterialDesignThemes.Wpf;
using PL.Order;

namespace PL;
/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{

    Thickness RandNum=new Thickness(100,0,0,0);
    BackgroundWorker SentAndDeliveredOrder;

    private IBl bl;
    private ObservableCollection<PO.OrderPO> listOrders;

    public event PropertyChangedEventHandler PropertyChanged;
    public ObservableCollection<PO.OrderPO> ListOrders
    {
        get
        { return listOrders; }
        set
        {
            listOrders = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ListOrders"));
            }
        }
    }



    DateTime date = DateTime.Now;

   //public double Progress { get; set; }

    public SimulatorWindow(IBl BL, ObservableCollection<PO.OrderPO> listOrders)
    {
        InitializeComponent();
        bl = BL;
        DataContext = listOrders;
        ListOrders = listOrders;
        progBarTime.Value = 0;
        SentAndDeliveredOrder = new BackgroundWorker();
        SentAndDeliveredOrder.DoWork += SentAndDeliveredOrder_DoWork;
        SentAndDeliveredOrder.ProgressChanged += SentAndDeliveredOrder_ProgressChanged;
        SentAndDeliveredOrder.RunWorkerCompleted += SentAndDeliveredOrder_RunWorkerCompleted;

        SentAndDeliveredOrder.WorkerReportsProgress = true;
        SentAndDeliveredOrder.WorkerSupportsCancellation = true;

        Date.Text = date.ToShortDateString();
    }

    private void SentAndDeliveredOrder_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        var list1 = (bl.Order.GetOrderList()).Select(order => Tools.CopyPropTo(order, new PO.OrderPO()));
        Tools.IEnumerableToObservable(ListOrders, list1);
        OrdersListAdmin.DataContext = ListOrders;
        if (progBarTime.Value < 100)
        {
            progBarTime.Value = 100;
        }
        MessageBox.Show("התהליך הושלם בהצלחה");
    }



    private void SentAndDeliveredOrder_DoWork(object? sender, DoWorkEventArgs e)
    {
        bool notAllOrderDelivired = true;
        bool notAllOrderSent = true;
        int i = 1;
        while (notAllOrderSent||notAllOrderDelivired)
        {
          
            List<PO.OrderPO> list = (from PO.OrderPO order in (bl.Order.GetOrderList()).Select(order => Tools.CopyPropTo(order, new PO.OrderPO()))
                                     let fullOrder = bl.Order.GetDetailsOrder(order.ID)
                                     where (fullOrder.State == Status.approved)
                                     select order).ToList();

            List<PO.OrderPO> list1 = (from PO.OrderPO order in (bl.Order.GetOrderList()).Select(order => Tools.CopyPropTo(order, new PO.OrderPO()))
                                     let fullOrder = bl.Order.GetDetailsOrder(order.ID)
                                     where (fullOrder.State == Status.sent)
                                     select order).ToList();

            if (list1.Count() == 0)
                notAllOrderDelivired = false;
            if (list.Count() == 0)
                notAllOrderSent = false;
            if (notAllOrderSent || notAllOrderDelivired)
            {
                TimeSpan day = new TimeSpan(24, 0, 0);
                date = date.Add(day);
                i++;
                DateTime dateToShip = date.Subtract(day * 14);

                var ship = (from PO.OrderPO order in list
                           let fullOrder = bl.Order.GetDetailsOrder(order.ID)
                           where (fullOrder.OrderDate <= dateToShip)
                           select bl.Order.UpdateShipDate(order.ID)).ToList();
                Thread.Sleep(1000);
                if (SentAndDeliveredOrder.WorkerReportsProgress == true)
                    SentAndDeliveredOrder.ReportProgress(i);

                DateTime dateToDel = date.Subtract(day * 21);
                var del = (from PO.OrderPO order in list1
                           let fullOrder = bl.Order.GetDetailsOrder(order.ID)
                           where (fullOrder.ShipDate <= dateToDel)
                           select bl.Order.UpdateDeliveryDate(order.ID, date)).ToList();
                Thread.Sleep(1000);
                if (SentAndDeliveredOrder.WorkerReportsProgress == true)
                    SentAndDeliveredOrder.ReportProgress(i);
            }
        }
    }

    private void SentAndDeliveredOrder_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        Tools.ListOrderBoToPo(ListOrders, bl.Order.GetOrderList()!);

        Date.Text = date.ToShortDateString();

        double precent = progBarTime.Value + e.ProgressPercentage/8;
        if(precent>100)
            precent=progBarTime.Value;
        progBarTime.Value = precent;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (SentAndDeliveredOrder.IsBusy != true)
        {
            this.Cursor = Cursors.Wait;
            SentAndDeliveredOrder.RunWorkerAsync();
        }
    }

    private void OrderTrackingButton_Click(object sender, RoutedEventArgs e)
    {
        var b = sender as Button;
        PO.OrderPO order = (PO.OrderPO)b!.DataContext;
        new SimulatorOrderTracking(bl.Order.Tracking(order.ID)).Show();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        if (SentAndDeliveredOrder.IsBusy == true)
        {
            this.Cursor = Cursors.Wait;
            SentAndDeliveredOrder.CancelAsync();
            MessageBox.Show("ישנן הזמנות שעוד לא נמסרו");
        }
    }
}

