﻿using BO;
using DO;
using PL.Cart;
using PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PL;
public static class Tools
{
    public static ObservableCollection<T> IEnumerableToObservable<T>(ObservableCollection<T> ListToCreate, IEnumerable<T> ExsitingList)
    {
        ListToCreate.Clear();
        foreach (var item in ExsitingList)
            ListToCreate.Add(item);
        return ListToCreate;
    }

    public static IEnumerable<T> ObservableToList<T>(List<T> ListToCreate, ObservableCollection<T> ExsitingList)
    {
        ListToCreate.Clear();
        foreach (var item in ExsitingList)
            ListToCreate.Add(item);
        return ListToCreate;
    }

    public static ObservableCollection<PO.OrderPO> ListOrderBoToPo(ObservableCollection<PO.OrderPO> ListToCreate, IEnumerable<BO.OrderForList> ExsitingList)
    {
        ListToCreate.Clear();
        foreach (var order in ExsitingList)
            ListToCreate.Add(Tools.CopyPropTo(order, new PO.OrderPO()));
        return ListToCreate;
    }

    public static void EnterNumbersOnly(object sender, KeyEventArgs e)
    {
        TextBox text = sender as TextBox;
        if (text == null) return;
        if (e == null) return;

        //allow get out of the text box
        if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            return;

        //allow list of system keys (add other key here if you want to allow)
        if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
            e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
         || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
            return;

        char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

        //allow control system keys
        if (Char.IsControl(c)) return;

        //allow digits (without Shift or Alt)
        if (Char.IsDigit(c))
            if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                return; //let this key be written inside the textbox

        //forbid letters and signs (#,$, %, ...)
        e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
        return;
    }

    public static void EnterOnlyNumbersAndPoint(object sender, KeyEventArgs e)
    {
        TextBox text = sender as TextBox;
        if (text == null) return;
        if (e == null) return;

        //allow get out of the text box
        if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            return;

        //allow list of system keys (add other key here if you want to allow)
        if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
            e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
         || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down
         || e.Key == Key.Right || e.Key == Key.OemPeriod)
            return;

        char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

        //allow control system keys
        if (Char.IsControl(c)) return;

        //allow digits (without Shift or Alt)
        if (Char.IsDigit(c))
            if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                return; //let this key be written inside the textbox

        //forbid letters and signs (#,$, %, ...)
        e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
        return;
    }

    //פונקצית הרחבה שמעתיקה שדות עם שם דומה מעצם מקור לעצם אחר  
    public static Target CopyPropTo<Source, Target>(this Source source, Target target)
    {

        if (source is not null && target is not null) //אם שני העצמים לא ריקים
        {
            Dictionary<string, PropertyInfo> propertiesInfoTarget = target.GetType().GetProperties()
                .ToDictionary(p => p.Name, p => p); //יוצר מילון של צמדים עם שם של שדה והערך בו

            IEnumerable<PropertyInfo> propertiesInfoSource = source.GetType().GetProperties();

            foreach (var propertyInfo in propertiesInfoSource)
            {
                if (propertiesInfoTarget.ContainsKey(propertyInfo.Name)
                    && (propertyInfo.PropertyType == typeof(string) || !(propertyInfo.PropertyType.IsClass)))
                {
                    propertiesInfoTarget[propertyInfo.Name].SetValue(target, propertyInfo.GetValue(source));
                }
            }
        }
        return target;
    }

    public static void PoCartToBoCart(CartPO cartPo, BO.Cart cartBo)
    {
        cartBo.OrderItems = new();
        CopyPropTo(cartPo, cartBo);
        //  ObservableToList(cartBo.OrderItems, cartPo.OrderItems);
        cartBo.OrderItems.Clear();
        foreach (var item in cartPo.OrderItems)
            cartBo.OrderItems.Add(CopyPropTo(item, new BO.OrderItem()));
    }

    public static void BoCartToPoCart(CartPO cartPo, BO.Cart cartBo)
    {
        cartPo.OrderItems = new();
        CopyPropTo(cartBo, cartPo);
        cartPo.OrderItems.Clear();
        if(cartBo.OrderItems != null)
            foreach (var item in cartBo.OrderItems)
                cartPo.OrderItems.Add(CopyPropTo(item, new PO.OrderItemPO()));
    }



    public static BO.Category? HebrewToCategory(string category)
    {
        if (category == "בקבוקים ומוצצים" || category == "בקבוקים_ומוצצים")
            return BO.Category.Bottles;
        if (category == "עגלות וטיולונים" || category == "עגלות_וטיולונים")
            return BO.Category.Carts;
        if (category == "צעצועים ומשחקים" || category == "צעצועים_ומשחקים")
            return BO.Category.Toys;
        if (category == "ביגוד והנעלה" || category == "ביגוד_והנעלה")
            return BO.Category.Clothes;
        if (category == "היגיינה והחתלה" || category == "היגיינה_והחתלה")
            return BO.Category.Diapers;
        return null;
    }

    public static BO.Category? StringToCategory(string category)
    {
        if (category == "Bottles")
            return BO.Category.Bottles;
        if (category == "Carts")
            return BO.Category.Carts;
        if (category == "Toys")
            return BO.Category.Toys;
        if (category == "Clothes")
            return BO.Category.Clothes;
        if (category == "Diapers")
            return BO.Category.Diapers;
        return null;
    }

    public static BO.Status? stringToState(string state)
    {
        if (state == "הזמנות שאושרו")
            return BO.Status.approved;
        if (state == "הזמנות שנשלחו")
            return BO.Status.sent;
        if (state == "הזמנות שנמסרו")
            return BO.Status.delivered;
        return null;
    }

    public static IEnumerable<string> CategoryToHebrew()
    {
        List<string> categories = new List<string>();
        categories.Add("בקבוקים ומוצצים");
        categories.Add("עגלות וטיולונים");
        categories.Add("צעצועים ומשחקים");
        categories.Add("ביגוד והנעלה");
        categories.Add("היגיינה והחתלה");
        categories.Add("הכל");
        return categories;  
    }

    public static IEnumerable<string> StateToHebrew()
    {
        List<string> states = new List<string>();
        states.Add("הזמנות שאושרו");
        states.Add("הזמנות שנשלחו");
        states.Add("הזמנות שנמסרו");
        states.Add("הכל");
        return states;
    }

    public static BO.Category PLCategoryToBL(PL.Category category)
    {
        if(category == PL.Category.בקבוקים_ומוצצים)
            return BO.Category.Bottles;
        if (category == PL.Category.עגלות_וטיולונים)
            return BO.Category.Carts;
        if (category == PL.Category.צעצועים_ומשחקים)
            return BO.Category.Toys;
        if (category == PL.Category.ביגוד_והנעלה)
            return BO.Category.Clothes;
        else 
            return BO.Category.Diapers;
    }
}
