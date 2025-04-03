using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;
using ViaEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Values;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation.Contracts;

namespace ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation {
    public class EventParticipation : Entity<Guid> {
        public ParticipationStatus ParticipationStatus { get; private set; }
        public Guest Guest { get; }
        public VEvent Event { get; }

        private EventParticipation(Guid id, Guest guest, VEvent vEvent, ParticipationStatus status) : base(id) {
            Guest = guest;
            Event = vEvent;
            ParticipationStatus = status;
        }

        public static Result<EventParticipation> Create(Guid id, Guest guest, VEvent vEvent, ParticipationStatus participationStatus, IGetEventParticipants eventParticipants) {
            if (guest == null) return new Result<EventParticipation>(151, "Guest cannot be null.");
            if (vEvent == null) return new Result<EventParticipation>(152, "Event cannot be null.");
            if (vEvent.Status == EventStatus.Cancelled) return new Result<EventParticipation>(157, "Guest cannot participate in a cancelled event.");
            if (vEvent.Visibility == Visibility.Private) return new Result<EventParticipation>(158, "Guest cannot participate in a private event.");
            List<EventParticipation> participations = eventParticipants.GetParticipants(vEvent.Id);
            if (participations.FindAll(participant => participant.Guest.Id == guest.Id).Count()>0){
                return new Result<EventParticipation>(153, "Guest is already participating in the event.");
            }
            if (participationStatus == ParticipationStatus.Participating && vEvent.Status != EventStatus.Active) return new Result<EventParticipation>(154, "Guest cannot participate in a non-active event.");
            if (participationStatus == ParticipationStatus.Invited && vEvent.Status != EventStatus.Active && vEvent.Status != EventStatus.Ready) return new Result<EventParticipation>(159, "Event is not ready for participation.");
            if (participations.FindAll(participant => participant.ParticipationStatus == ParticipationStatus.Participating).Count() == vEvent.MaxNumberOfGuests.Value) {
                return new Result<EventParticipation>(155, "The event is full.");
            }
            if (vEvent.Status == EventStatus.Active && vEvent.Duration.From < DateTime.Now) return new Result<EventParticipation>(156, "Guest cannot participate in an already active event.");
            
            return new Result<EventParticipation>(new EventParticipation(id, guest, vEvent, participationStatus));
        }
        public Result<EventParticipation> UpdateStatus(ParticipationStatus status, IGetEventParticipants eventParticipants) {
            if (Event.Status == EventStatus.Cancelled) return new Result<EventParticipation>(157, "Guest cannot participate in a cancelled event.");
            if (Event.Status == EventStatus.Ready) return new Result<EventParticipation>(161, "The event is not ready to be joined to.");
            if (status == ParticipationStatus.Invited) return new Result<EventParticipation>(160, "Cannot set Participation status to invited.");
            if (Event.Status == EventStatus.Active && Event.Duration.From < DateTime.Now) return new Result<EventParticipation>(156, "Guest cannot participate in an already active event.");
            List<EventParticipation> participations = eventParticipants.GetParticipants(Event.Id);
            EventParticipation? participant = participations.Find(participant => participant.Guest.Id == Guest.Id);
            if (participant == null) return new Result<EventParticipation>(151, "Guest cannot be null.");
            int participationCount = participations.FindAll(participant => participant.ParticipationStatus == ParticipationStatus.Participating).Count();
            int maxNumberOfGuests = Event.MaxNumberOfGuests.Value;
            if (participationCount == maxNumberOfGuests) {
                return new Result<EventParticipation>(155, "The event is full.");
            }
            ParticipationStatus = status;
            return new Result<EventParticipation>(this);
        }
    }
}
