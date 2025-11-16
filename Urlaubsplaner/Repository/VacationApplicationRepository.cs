using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using Urlaubsplaner.Model;
using Urlaubsplaner.Entity;
using Urlaubsplaner.Mapper;
using System.Diagnostics;

namespace Urlaubsplaner.Repository;

public class VacationApplicationRepository
{

    private readonly String _storageFileName = "storage.xml";

    private ObservableCollection<VacationApplication> _applications;

    public VacationApplicationRepository()
    {
        _applications = new ObservableCollection<VacationApplication>();
    }

    public VacationApplication[]? GetVacationApplications()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Storage));
        VacationApplication[]? applications;

        using (Stream stream = new FileStream(_storageFileName, FileMode.OpenOrCreate))
        {
            if (stream.Length == 0)
            {
                applications = [];
            }
            else
            {
                var entities = (serializer.Deserialize(stream) as Storage)?.vacationApplications ?? Array.Empty<VacationApplicationEntity>();
                applications = [.. entities.Select(e => VacationApplicationMapper.Map(e))];
            }

        }
        _applications = new ObservableCollection<VacationApplication>(applications);
        return applications;
    }

    public void AddApplication(VacationApplication vacationApplication)
    {
        // very basic Id counting
        if(_applications.Count == 0)
        {
            vacationApplication.Id = 1;
        }else
        {
            vacationApplication.Id = _applications.Max(e => e.Id) + 1;
        }
            
        _applications.Add(vacationApplication);
        SaveApplications();
    }

    // we always save the whole array, this isn't very efficient
    public void SaveApplications()
    {

        XmlSerializer serializer = new XmlSerializer(typeof(Storage));
        using (Stream stream = new FileStream(_storageFileName, FileMode.Create))
        {
            var storage = new Storage();
            storage.vacationApplications = [.. _applications.Select(e => VacationApplicationMapper.Map(e))];
            serializer.Serialize(stream, storage);
        }

    }

    public void ConfirmApplication(int vacationApplicationId, int userId)
    {
        var application = _applications.Where(e => e.Id == vacationApplicationId).FirstOrDefault();
        if(application != null)
        {
            application.Confirmed = true;
            application.ConfirmedById = userId;
            application.LastEdited = DateTime.Now;
            SaveApplications();
        } else
        {
            Trace.WriteLine($"Failed to find application to confirm with id {vacationApplicationId}");
        }
        
        
    }
}

