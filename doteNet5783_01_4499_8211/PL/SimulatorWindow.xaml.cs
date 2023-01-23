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

    Thickness RandNum = new Thickness(100,0,0,0);
    BackgroundWorker SentOrder;
    BackgroundWorker DelivredOrder;

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
        SentOrder = new BackgroundWorker();
        SentOrder.DoWork += SentOrder_DoWork;
        SentOrder.ProgressChanged += SentOrder_ProgressChanged;
        SentOrder.RunWorkerCompleted += SentOrder_RunWorkerCompleted;
        DelivredOrder = new BackgroundWorker();
        DelivredOrder.DoWork += DelivredOrder_DoWork;
        DelivredOrder.ProgressChanged += DelivredOrder_ProgressChanged;
        DelivredOrder.RunWorkerCompleted += DelivredOrder_RunWorkerCompleted;

        SentOrder.WorkerReportsProgress = true;
        SentOrder.WorkerSupportsCancellation = true;

        DelivredOrder.WorkerReportsProgress = true;
        DelivredOrder.WorkerSupportsCancellation = true;
    }

    private void DelivredOrder_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (progBarTime.Value < 100)
        {
            progBarTime.Value = 100;

        }
        MessageBox.Show("התהליך הושלם בהצלחה");
    }

    private void DelivredOrder_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        //int precent = e.ProgressPercentage;
        //Thickness t = new Thickness(100 - precent*10, 0, 0, 0);
        //RandNum = t;

        Tools.ListOrderBoToPo(ListOrders, bl.Order.GetOrderList()!);
      
        double precent1 = progBarTime.Value + (100 - progBarTime.Value) / 10;
        progBarTime.Value = precent1;
    }

    private void DelivredOrder_DoWork(object? sender, DoWorkEventArgs e)
    {
       
        bool notAllOrderDelivired = true;
        int i = 1;
        while (notAllOrderDelivired)
        {

            List<PO.OrderPO> list = (from PO.OrderPO order in (bl.Order.GetOrderList()).Select(order => Tools.CopyPropTo(order, new PO.OrderPO()))
                                     let fullOrder = bl.Order.GetDetailsOrder(order.ID)
                                     where (fullOrder.State == Status.sent)
                                     select order).ToList();
            if (list.Count() == 0)
                notAllOrderDelivired = false;
            else
            {
                TimeSpan day = new TimeSpan(24, 0, 0);
                date = date.Add(day);
                i++;
                DateTime dateToDel = date.Subtract(day * 14);
                var del = (from PO.OrderPO order in list
                           let fullOrder = bl.Order.GetDetailsOrder(order.ID)
                           where (fullOrder.ShipDate <= dateToDel)
                           select bl.Order.UpdateDeliveryDate(order.ID, date)).ToList();
                Thread.Sleep(100);
                if (DelivredOrder.WorkerReportsProgress == true)
                    DelivredOrder.ReportProgress(i);
            }

        }
    }

    private void SentOrder_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (DelivredOrder.IsBusy != true)
        {
            this.Cursor = Cursors.Wait;
            DelivredOrder.RunWorkerAsync();
            if(progBarTime.Value<50)
            {
                progBarTime.Value = 50;
            }
        }
    }

    private void SentOrder_DoWork(object? sender, DoWorkEventArgs e)
    {
        bool notAllOrderSent = true;
        int i = 1;
        while (notAllOrderSent)
        {
          
            List<PO.OrderPO> list = (from PO.OrderPO order in (bl.Order.GetOrderList()).Select(order => Tools.CopyPropTo(order, new PO.OrderPO()))
                                     let fullOrder = bl.Order.GetDetailsOrder(order.ID)
                                     where (fullOrder.State == Status.approved)
                                     select order).ToList();
            if (list.Count() == 0)
                notAllOrderSent = false;
            else
            {
                TimeSpan day = new TimeSpan(24, 0, 0);
                date = date.Add(day);
                i++;
                DateTime dateToShip = date.Subtract(day * 21);

                var del = (from PO.OrderPO order in list
                           let fullOrder = bl.Order.GetDetailsOrder(order.ID)
                           where (fullOrder.OrderDate <= dateToShip)
                           select bl.Order.UpdateShipDate(order.ID)).ToList();
                Thread.Sleep(300);
                if (SentOrder.WorkerReportsProgress == true)
                    SentOrder.ReportProgress(i);
            }

        }
       
    }

    private void SentOrder_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        Tools.ListOrderBoToPo(ListOrders, bl.Order.GetOrderList()!);

        double precent = progBarTime.Value + (50 - progBarTime.Value) / 10;
        progBarTime.Value = precent;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (DelivredOrder.IsBusy != true)
        {
            this.Cursor = Cursors.Wait;
            SentOrder.RunWorkerAsync();
        }
    }

    private void OrderTrackingButton_Click(object sender, RoutedEventArgs e)
    {
        var b = sender as Button;
        PO.OrderPO order = (PO.OrderPO)b!.DataContext;
        new SimulatorOrderTracking(bl.Order.Tracking(order.ID)).Show();
    }
}






//    //    bwMarry = new BackgroundWorker();
//    //    bwMarry.DoWork += BwMarry_DoWork;
//    //    bwMarry.ProgressChanged += BwMarry_ProgressChanged;
//    //    bwMarry.RunWorkerCompleted += BwMarry_RunWorkerCompleted;

//    //bwMarry.WorkerReportsProgress = true;
//    //    bwMarry.WorkerSupportsCancellation = true;

//    //}

//    //private void BwMarry_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
//    //{
//    //    if (e.Cancelled == true)
//    //    {
//    //        Thickness t = new Thickness(0, 0, 0, 0);
//    //        airplane.Margin = t;
//    //        MessageBox.Show("Try next time...");
//    //    }
//    //    else
//    //    {

//    //        MessageBox.Show("Mazal Tov!!");
//    //    }
//    //    this.Cursor = Cursors.Arrow;
//    //}

//    //private void BwMarry_ProgressChanged(object sender, ProgressChangedEventArgs e)
//    //{
//    //    int precent = e.ProgressPercentage;
//    //    Thickness t = new Thickness(precent, 0, 0, 0);
//    //    airplane.Margin = t;
//    //    //int precent = e.ProgressPercentage;
//    //    //progBarTime.Value = precent;
//    //    if (precent > 50)
//    //    {
//    //    }
//    //}

//    //private void BwMarry_DoWork(object sender, DoWorkEventArgs e)
//    //{
//    //    //bl.simultor(cuurent drone, e.Argument, bw ) // 
//    //    int timeToMarry = 30;
//    //    for (int i = 0; i <= timeToMarry; i++)
//    //    {
//    //        if (bwMarry.CancellationPending == true)
//    //        {
//    //            e.Cancel = true;
//    //            break;
//    //        }
//    //        else
//    //        {
//    //            Thread.Sleep(500);
//    //            if (bwMarry.WorkerReportsProgress == true)
//    //                bwMarry.ReportProgress(i * 100 / timeToMarry);

//    //        }
//    //    }
//    //}

//    //private void btnMarry_Click(object sender, RoutedEventArgs e)
//    //{
//        if (bwMarry.IsBusy != true)
//        {
//            this.Cursor = Cursors.Wait;
//            bwMarry.RunWorkerAsync(30);
//        }
//    }

//    private void btnCancel_Click(object sender, RoutedEventArgs e)
//    {
//        if (bwMarry.WorkerSupportsCancellation == true)
//            bwMarry.CancelAsync(); // Cancel the asynchronous operation.
//    }

//    private void Window_Closing(object sender, CancelEventArgs e)
//    {
//        if (bwMarry.WorkerSupportsCancellation == true)
//            bwMarry.CancelAsync(); // Cancel the asynchronous operation.
//        Thread.Sleep(1000);
//    }
//}
