using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Urlaubsplaner.Entity
{
    public class VacationApplicationEntity
    {
        [XmlElement("Id")]
        public int Id { get; set; }
        [XmlElement("From")]
        public DateTime From { get; set; }
        [XmlElement("To")]
        public DateTime To { get; set; }
        [XmlElement("UserId")]
        public int UserId { get; set; }
        [XmlElement("Confirmed")]
        public bool Confirmed { get; set; }
        [XmlElement("ConfirmedBy")]
        public int? ConfirmedBy {  get; set; }
        [XmlElement("LastEdited")]
        public DateTime LastEdited { get; set; }
        [XmlElement("Added")]
        public DateTime Added { get; set; }
    }
}
