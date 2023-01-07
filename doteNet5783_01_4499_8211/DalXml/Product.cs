namespace Dal;
using DalApi;
using DO;
using System.Security.Principal;
using System.Xml.Linq;

internal class Product : IProduct
{
    const string s_students = "students"; //Linq to XML

    static DO.Product? getProduct(XElement p) =>
        p.ToIntNullable("ID") is null ? null : new DO.Product()
        {
            ID = (int)p.Element("ID")!,
            Name = (string?)p.Element("Name"),
            Category = p.ToEnumNullable<DO.Category>("Category"),
            Price = (Double)p.Element("Price"),
            InStock = (int)p.Element("InStock"),
            IsDeleted = (bool)p.Element("IsDeleted"),
            Path = (string?)p.Element("Path"),
        };

    static IEnumerable<XElement> createStudentElement(DO.Product student)
    {
        yield return new XElement("ID", student.ID);
        if (student.Name is not null)
            yield return new XElement("LastName", student.Name);
         yield return new XElement("StudentStatus", student.Category);
         yield return new XElement("BirthDate", student.Price);
         yield return new XElement("Grade", student.IsDeleted);
        if (student.Path is not null)
            yield return new XElement("Grade", student.Path);
    }

    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? filter = null) =>
        filter is null
        ? XMLTools.LoadListFromXMLElement(s_students).Elements().Select(s => getProduct(s))
        : XMLTools.LoadListFromXMLElement(s_students).Elements().Select(s => getProduct(s)).Where(filter);

    public DO.Product GetById(int id) =>
        (DO.Product)getProduct(XMLTools.LoadListFromXMLElement(s_students)?.Elements()
        .FirstOrDefault(st => st.ToIntNullable("ID") == id)
        // fix to: throw new DalMissingIdException(id);
        ?? throw new Exception("missing id"))!;

    public int Add(DO.Product student)
    {
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_students);

        if (XMLTools.LoadListFromXMLElement(s_students)?.Elements()
            .FirstOrDefault(st => st.ToIntNullable("ID") == student.ID) is not null)
            // fix to: throw new DalMissingIdException(id);;
            throw new Exception("id already exist");

        studentsRootElem.Add(new XElement("Student", createStudentElement(student)));
        XMLTools.SaveListToXMLElement(studentsRootElem, s_students);

        return student.ID; ;
    }

    public void Delete(int id)
    {
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_students);

        (studentsRootElem.Elements()
            // fix to: throw new DalMissingIdException(id);
            .FirstOrDefault(st => (int?)st.Element("ID") == id) ?? throw new Exception("missing id"))
            .Remove();

        XMLTools.SaveListToXMLElement(studentsRootElem, s_students);
    }

    public void Update(DO.Product doStudent)
    {
        Delete(doStudent.ID);
        Add(doStudent);
    }
}