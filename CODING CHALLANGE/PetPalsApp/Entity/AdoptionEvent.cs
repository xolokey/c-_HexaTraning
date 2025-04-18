using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsApp.Entity
{
    public class AdoptionEvent
    {
        //  OOP: Maintain list of adoptable participants
        private List<IAdoptable> Participants = new List<IAdoptable>();

        //  DB Integration Properties
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }


        public AdoptionEvent() { }


        public AdoptionEvent(int eventId, string eventName, DateTime eventDate, string location)
        {
            EventId = eventId;
            EventName = eventName;
            EventDate = eventDate;
            Location = location;
        }


        public void RegisterParticipant(IAdoptable participant)
        {
            if (participant != null)
            {
                Participants.Add(participant);
                Console.WriteLine("Participant registered for the adoption event.");
            }
        }

        public void HostEvent()
        {
            Console.WriteLine("Adoption event is now live!");
            foreach (var participant in Participants)
            {
                participant.Adopt();
            }
        }

        public override string ToString()
        {
            return $"ID: {EventId}, Name: {EventName}, Date: {EventDate.ToShortDateString()}, Location: {Location}";
        }
    }
}
