namespace Dal;
using DalApi;
using DO;
using System.Linq;
using System.Security.Principal;
using System.Xml.Linq;

internal class Product : IProduct
{
    const string s_products = "products"; //Linq to XML

    static DO.Product? getProduct(XElement p) =>
        p.ToIntNullable("ID") is null ? null : new DO.Product()
        {
            ID = (int)p.Element("ID")!,
            Name = (string)p.Element("Name"),
            Category = p.ToEnumNullable<DO.Category>("Category")??Category.All,
            Price = p.ToDoubleNullable("Price")??0,
            InStock = p.ToIntNullable("InStock")??0,
            IsDeleted = (bool)p.Element("IsDeleted"),
            Path = (string)p.Element("Path"),
        };

    static IEnumerable<XElement> createProductElement(DO.Product product)
    {
        yield return new XElement("ID", product.ID);
        if (product.Name is not null)
            yield return new XElement("Name", product.Name);
        yield return new XElement("Category", product.Category);
        yield return new XElement("Price", product.Price);
        yield return new XElement("InStock", product.InStock);
        yield return new XElement("IsDeleted", product.IsDeleted);
    }

    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? filter = null) =>
        filter is null
        ? XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getProduct(s))
        : XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getProduct(s)).Where(filter);

    public DO.Product GetById(int id) =>
        (DO.Product)getProduct(XMLTools.LoadListFromXMLElement(s_products)?.Elements()
        .FirstOrDefault(st => st.ToIntNullable("ID") == id)

        ?? throw new DoesNotExistException(id))!;

    public int Add(DO.Product product)
    {
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products);

        if (XMLTools.LoadListFromXMLElement(s_products)?.Elements()
            .FirstOrDefault(st => st.ToIntNullable("ID") == product.ID) is not null)
             throw new AlreadyExistsException(product.ID);

        studentsRootElem.Add(new XElement("Product", createProductElement(product)));
        XMLTools.SaveListToXMLElement(studentsRootElem, s_products);

        return product.ID; 
    }

    public void Delete(int id)
    {
        DO.Product product = GetById(id);
        product.IsDeleted = true;   

        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products);

        (studentsRootElem.Elements()
            .FirstOrDefault(st => (int?)st.Element("ID") == id) ?? throw new DoesNotExistException(id))
            .Remove();

        XMLTools.SaveListToXMLElement(studentsRootElem, s_products);

        Add(product);
    }

    public void Update(DO.Product doProduct)
    {
        Delete(doProduct.ID);
        Add(doProduct);
    }

}