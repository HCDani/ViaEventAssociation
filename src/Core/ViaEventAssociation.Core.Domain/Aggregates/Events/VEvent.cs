using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Locations;
using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events {
    public class VEvent : AggregateRoot<Guid> {
        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public EventDuration Duration { get; private set; }
        public Status Status { get; private set; }
        public Visibility Visibility { get; private set; }
        public MaxNumberOfGuests MaxNumberOfGuests { get; private set; }
        public LocationId LocationId { get; private set; }

        private VEvent(Guid id, Status status) : base(id) {
            Title = Title.Default;
            MaxNumberOfGuests = MaxNumberOfGuests.Default;
            Description = Description.Default;
            Visibility = Visibility.Private;
            Status = status;
        }
        public static VEvent Create(Guid id,Status status) {
            return new VEvent(id,status);
        }
        public Result<Title> UpdateTitle(Title title) {
            if (title == null || title == Title.Default) return new Result<Title>(10, "Title must be set");
            if (Status == Status.Active) return new Result<Title>(11, "Title cannot be updated when status is active.");
            if (Status == Status.Cancelled) return new Result<Title>(12, "Title cannot be updated when status is cancelled.");

            Title = title;
            Status = Status.Draft;
            return new Result<Title>(Title);
        }
        public Result<Description> UpdateDescription(Description description) {
            if (Status == Status.Active) return new Result<Description>(21, "Description cannot be updated when status is active.");
            if (Status == Status.Cancelled) return new Result<Description>(22, "Description cannot be updated when status is cancelled.");

            Description = description;
            Status = Status.Draft;
            return new Result<Description>(Description);
        }
        public Result<EventDuration> UpdateDuration(EventDuration duration) {
            if (Status == Status.Active) return new Result<EventDuration>(36, "Duration cannot be updated when status is active.");
            if (Status == Status.Cancelled) return new Result<EventDuration>(37, "Duration cannot be updated when status is cancelled.");
            if (duration.From < DateTime.Now) return new Result<EventDuration>(38, "The 'From' date must be in the future");
            Duration = duration;
            Status = Status.Draft;
            return new Result<EventDuration>(Duration);
        }
        public Result<Status> UpdateStatus(Status status) {
            // New draft status is valid only if the existing status is also draft.
            if (status == Status.Draft && Status != Status.Draft) {
                return new Result<Status>(2, "Invalid status transition");
            }
            // New ready status is valid if the existing status is draft or ready and all required fields are set.
            if (status == Status.Ready) {
                if (Status != Status.Draft && Status != Status.Ready) {
                    return new Result<Status>(2, "Invalid status transition");
                }
                if (Title == Title.Default) {
                    return new Result<Status>(3, "Title must be set before setting status to ready");
                }
                if (Duration == null) {
                    return new Result<Status>(4, "Duration must be set before setting status to ready");
                }
                if (LocationId == null) {
                    return new Result<Status>(5, "Location must be set before setting status to ready");
                }
                if (Description == null) {
                    return new Result<Status>(6, "Description must be set before setting status to ready");
                }
                if (Duration.From < DateTime.Now) return new Result<Status>(38, "The 'From' date must be in the future");      
            }
            // New active status is valid if the existing status is ready or active.
            if (status == Status.Active && Status != Status.Ready && Status != Status.Active) {
                return new Result<Status>(2, "Invalid status transition");
            }
            // New cancelled status is always valid.

            Status = status;
            return new Result<Status>(Status);
        }
        public Result<Visibility> UpdateVisibility(Visibility visibility) {
            if (Status == Status.Cancelled) return new Result<Visibility>(41, "Visibility cannot be updated when status is cancelled.");
            if (Status == Status.Active && visibility == Visibility.Private) return new Result<Visibility>(42, "Visibility cannot be set to private when status is active.");
            if (Visibility == Visibility.Public && visibility == Visibility.Private) {
                Status = Status.Draft;
            }
            Visibility = visibility;
            return new Result<Visibility>(Visibility);
        }
        public Result<MaxNumberOfGuests> UpdateMaxNumberOfGuests(MaxNumberOfGuests maxNumberOfGuests) {
            if (Status == Status.Active && maxNumberOfGuests.Value < MaxNumberOfGuests.Value) return new Result<MaxNumberOfGuests>(51, "Max number of guests cannot be decreased when status is active.");
            if (Status == Status.Cancelled) return new Result<MaxNumberOfGuests>(52, "Max number of guests cannot be updated when status is cancelled.");
            MaxNumberOfGuests = maxNumberOfGuests;
            return new Result<MaxNumberOfGuests>(MaxNumberOfGuests);
        }
        public void UpdateLocationId(LocationId locationId) {
            LocationId = locationId;
        }

    }
}
