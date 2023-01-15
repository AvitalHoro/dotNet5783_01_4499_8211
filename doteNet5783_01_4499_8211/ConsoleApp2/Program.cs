
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
            string[] costumerName = { "גלי אסרף", "שאול בילינקי", "גיל ברגמן", "מאור זלמנוביץ'", "ציון זלצמן", "אביב טוקר", "רונן ישראולביץ", "הראל כהן", "ערן כהנא", "הדר לוינשטיין", "שיר לוינסקי", "גילת מורביה", "נועה מגורי", "אורי מלכא", "ליאל מרציאנו", "שולה גנץ", "הילה סבן", "יגאל סגרון", "דורון פיטוסי", "עדן בן ארי", "עופר פרץ", "ירון אלמוג", "ארז כהן", "דנה לביא", "רן לוי", "אליה ששון", "בר גליס", "דור שטרית", "ישי אליאס", "עזרא מזרחי", "אליענה פוזנסקי" };
            string[] costumerEmail = {"galiasa@gmail.com", "shaulbi12@gmail.com", "gilhamelech@walla.co.il", "maorz2001@gmail.com", "theking@acad.il","avivtoker@gmail.com", "roneny@gmail.com", "harelcohen863@gmail.com", "erankahana@walla.co.il", "hadarlevinshtern@gmail.com", "levinski54@gmail.com", "gilat@gmail.com", "noamagory3@gmail.com", "turhnkft@gmail.com", "liel174@gmail.com", "sholamitganz@gmail.com", "hilasaban1@gmail.com", "igalsigron@gmail.com", "doron999@gmail.com", "edenbenari@gmail.com", "oferperez3@gmail.com", "yaronalmog@gmail.com", "erezcohen123@gmail.com", "danalavi@gmail.com", "barlevi@gmail.com", "eliya143@gmail.com", "bargeliss@gmail.com", "dshitrit@gmail.com", "ishayelias@gmail.com", "ezramizrachi@gmail.com", "elianapozzanski@gmail.com"};
            string[] costumerAdress = { "הכלנית 16", "עמרם גאון 10", "נזר דוד 32", "קהתי 20", "חיים ויטאל 35ב", "זאב חקלאי 14", "ריינס 6", "ימימה 7", "הנופך 242", "הרב צבי יהודה 18", "מרגלית 306ב", "קרית משה 10/8", "נגארה 18", "גת 12א", "יקינטון 576", "בית חורון", "עמרם גאון 3", "בית חורון", "העילוי 10", "הפסגה 15", "הברון הירש 14", "קרית משה 10", "מעלות דפנה 117", "יונתן בן עוזיאל 15", "גבעת שאול 32", "שדרות המאירי 3", "חייט תורן 1", "אליעזר הלוי 32", "ארבעת המינים 10", "חפץ חיים 37", "בזל 9", "נחליאלי 11", "גת 15", "פני קדם 56", "תקוע 7" };
            for (int i = 0; i < 30; i++)
            {
                BO.Cart cart2 = new BO.Cart();
                cart2.OrderItems = new();
                cart2.CostumerName = costumerName[i];
                cart2.CostumerAdress = costumerAdress[i];
                cart2.CostumerEmail = costumerEmail[i];
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
