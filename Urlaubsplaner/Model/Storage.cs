using System.Xml.Serialization;
using Urlaubsplaner.Entity;

namespace Urlaubsplaner.Model;

[XmlRoot("Storage")]
[Serializable]
public class Storage
{
    [XmlArray("applications")]
    [XmlArrayItem("application")]
    public VacationApplicationEntity[] vacationApplications { get; set; }

    public Storage()
    {
        vacationApplications = Array.Empty<VacationApplicationEntity>();
    }
}

