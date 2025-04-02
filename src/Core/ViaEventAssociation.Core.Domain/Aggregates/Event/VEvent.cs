using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;
using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.EventNS {
    public class VEvent : AggregateRoot<Guid> {
        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public EventDuration Duration { get; private set; }
        public EventStatus Status { get; private set; }
        public Visibility Visibility { get; private set; }
        public MaxNumberOfGuests MaxNumberOfGuests { get; private set; }
        public Location Location { get; private set; }

        private VEvent(Guid id, EventStatus status) : base(id) {
            Title = Title.Default;
            MaxNumberOfGuests = MaxNumberOfGuests.Default;
            Description = Description.Default;
            Visibility = Visibility.Private;
            Status = status;
        }
        public static VEvent Create(Guid id,EventStatus status) {
            return new VEvent(id,status);
        }
        public Result<Title> UpdateTitle(Title title) {
            if (title == null || title == Title.Default) return new Result<Title>(10, "Title must be set");
            if (Status == EventStatus.Active) return new Result<Title>(11, "Title cannot be updated when status is active.");
            if (Status == EventStatus.Cancelled) return new Result<Title>(12, "Title cannot be updated when status is cancelled.");

            Title = title;
            Status = EventStatus.Draft;
            return new Result<Title>(Title);
        }
        public Result<Description> UpdateDescription(Description description) {
            if (Status == EventStatus.Active) return new Result<Description>(21, "Description cannot be updated when status is active.");
            if (Status == EventStatus.Cancelled) return new Result<Description>(22, "Description cannot be updated when status is cancelled.");

            Description = description;
            Status = EventStatus.Draft;
            return new Result<Description>(Description);
        }
        public Result<EventDuration> UpdateDuration(EventDuration duration) {
            if (Status == EventStatus.Active) return new Result<EventDuration>(36, "Duration cannot be updated when status is active.");
            if (Status == EventStatus.Cancelled) return new Result<EventDuration>(37, "Duration cannot be updated when status is cancelled.");
            if (duration.From < DateTime.Now) return new Result<EventDuration>(38, "The 'From' date must be in the future");
            Duration = duration;
            Status = EventStatus.Draft;
            return new Result<EventDuration>(Duration);
        }
        public Result<EventStatus> UpdateStatus(EventStatus status) {
            // New draft status is valid only if the existing status is also draft.
            if (status == EventStatus.Draft && Status != EventStatus.Draft) {
                return new Result<EventStatus>(2, "Invalid status transition");
            }
            // New ready status is valid if the existing status is draft or ready and all required fields are set.
            if (status == EventStatus.Ready) {
                if (Status != EventStatus.Draft && Status != EventStatus.Ready) {
                    return new Result<EventStatus>(2, "Invalid status transition");
                }
                if (Title == Title.Default) {
                    return new Result<EventStatus>(3, "Title must be set before setting status to ready");
                }
                if (Duration == null) {
                    return new Result<EventStatus>(4, "Duration must be set before setting status to ready");
                }
                if (Location == null) {
                    return new Result<EventStatus>(5, "Location must be set before setting status to ready");
                }
                if (Description == null) {
                    return new Result<EventStatus>(6, "Description must be set before setting status to ready");
                }
                if (Duration.From < DateTime.Now) return new Result<EventStatus>(38, "The 'From' date must be in the future");      
            }
            // New active status is valid if the existing status is ready or active.
            if (status == EventStatus.Active && Status != EventStatus.Ready && Status != EventStatus.Active) {
                return new Result<EventStatus>(2, "Invalid status transition");
            }
            // New cancelled status is always valid.

            Status = status;
            return new Result<EventStatus>(Status);
        }
        public Result<Visibility> UpdateVisibility(Visibility visibility) {
            if (Status == EventStatus.Cancelled) return new Result<Visibility>(41, "Visibility cannot be updated when status is cancelled.");
            if (Status == EventStatus.Active && visibility == Visibility.Private) return new Result<Visibility>(42, "Visibility cannot be set to private when status is active.");
            if (Visibility == Visibility.Public && visibility == Visibility.Private) {
                Status = EventStatus.Draft;
            }
            Visibility = visibility;
            return new Result<Visibility>(Visibility);
        }
        public Result<MaxNumberOfGuests> UpdateMaxNumberOfGuests(MaxNumberOfGuests maxNumberOfGuests) {
            if (Status == EventStatus.Active && maxNumberOfGuests.Value < MaxNumberOfGuests.Value) return new Result<MaxNumberOfGuests>(51, "Max number of guests cannot be decreased when status is active.");
            if (Status == EventStatus.Cancelled) return new Result<MaxNumberOfGuests>(52, "Max number of guests cannot be updated when status is cancelled.");
            if(Location != null && maxNumberOfGuests.Value > Location.MaxCapacity.Value) return new Result<MaxNumberOfGuests>(53, "Max number of guests cannot be higher than max capacity of the location.");
            MaxNumberOfGuests = maxNumberOfGuests;
            return new Result<MaxNumberOfGuests>(MaxNumberOfGuests);
        }
        public Result<Location> UpdateLocation(Location location) {
            if(MaxNumberOfGuests.Value > location.MaxCapacity.Value) return new Result<Location>(61, "Max number of guests cannot be higher than max capacity of the location.");
            if (Status == EventStatus.Cancelled) return new Result<Location>(62, "Location cannot be updated when status is cancelled.");
            if (Status == EventStatus.Active) return new Result<Location>(63, "Location cannot be set to private when status is active.");
            Location = location;
            return new Result<Location>(Location);
        }

    }
}
