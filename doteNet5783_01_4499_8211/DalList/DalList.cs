using DalApi;

namespace Dal;

//מחלקת "גג". מטרתה לרכז את תפקוד כל ישויות הגישה לנתונים.
//דרכה ניתן לייצר גישה לכל סוגי הנתונים

sealed internal class DalList: IDal
{
    public static IDal Instance { get; }= new DalList();
    private DalList() { } 

    public IOrder Order => new DalOrder(); 

    public IProduct Product => new DalProduct();

    public IOrderItem OrderItem => new DalOrderItem();
}
