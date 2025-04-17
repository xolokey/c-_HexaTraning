using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PetPalsApp.Entity;
using PetPalsApp.Util;

namespace PetPalsApp.DAO
{
    public interface IAdoptionEventDao
    {
        List<AdoptionEvent> GetUpcomingEvents();
        void RegisterParticipant(string name, string type, int eventId);
    }

}
