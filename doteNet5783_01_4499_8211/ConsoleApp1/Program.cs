using System;
using BLApi;
using BlApi;

partial class Program
{
    static void Main(string[] args)
    {
        {
            IBl bl = BlFactory.GetBl();
            Random randNum = new Random(); //enters a random number to "randNum"
            DateTime date = DateTime.Now - new TimeSpan(randNum.NextInt64(10L * 1000L * 3600L * 24L * 10000));
            List<string> costumerName = new();
            List<string> costumerEmail = new();
            List<string> costumerAdress = new();
            for (int i = 0; i < 30; i++)
                costumerName.Add(Console.ReadLine());
            for (int i = 0; i < 30; i++)
                costumerEmail.Add(Console.ReadLine());
            for (int i = 0; i < 30; i++)
                costumerAdress.Add(Console.ReadLine());
            for (int i = 0; i < 30; i++)
            {
                BO.Cart cart2 = new BO.Cart();
                cart2.CostumerName = costumerName.First();
                costumerName.Remove(cart2.CostumerName);
                cart2.CostumerAdress = costumerAdress.First();
                costumerAdress.Remove(cart2.CostumerAdress);
                cart2.CostumerEmail = costumerEmail.First();
                costumerEmail.Remove(cart2.CostumerEmail);
                int s = randNum.Next(1, 6);
                for (int j = 0; j < s; j++)
                {
                    bl.Cart.AddProduct(cart2, randNum.Next(100004, 100078));
                }
                s = randNum.Next(0, 3);
                for (int j = 0; j < s && j < cart2.OrderItems.Count; j++)
                {
                    bl.Cart.UpdateAmountProduct(cart2, cart2.OrderItems[j].ProductID, randNum.Next(0, 4));
                }
                bl.Cart.MakeOrder(cart2);
            }
        }
    }
}
