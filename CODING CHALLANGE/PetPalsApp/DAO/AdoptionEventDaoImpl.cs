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
    public class AdoptionEventDaoImpl : IAdoptionEventDao
    {
        public List<AdoptionEvent> GetUpcomingEvents()
        {
            List<AdoptionEvent> events = new List<AdoptionEvent>();

            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    conn.Open();
                    string query = "SELECT eventid, eventname, eventdate, location FROM adoption_events";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        AdoptionEvent evt = new AdoptionEvent
                        {
                            EventId = Convert.ToInt32(reader["eventid"]),
                            EventName = reader["eventname"].ToString(),
                            EventDate = Convert.ToDateTime(reader["eventdate"]),
                            Location = reader["location"].ToString()
                        };

                        events.Add(evt);
                    }
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine("[DB ERROR - Get Events] " + ex.Message);
            }

            return events;
        }

        public void RegisterParticipant(string name, string type, int eventId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    conn.Open();
                    string query = "INSERT INTO participants (participantname, participanttype, eventid) " +
                                   "VALUES (@name, @type, @eventId)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@eventId", eventId);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Participant registered successfully.");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("[DB ERROR - Register Participant] " + ex.Message);
            }
        }
    }

}
