using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Locations;
using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events
{
    public class VEvent : AggregateRoot<EventId>
    {
        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public EventDuration Duration { get; private set;}
        public Status Status { get; private set; }
        public Visibility Visibility { get; private set; }
        public MaxNumberOfGuests MaxNumberOfGuests { get; private set; }
        public LocationId LocationId { get; private set; }

        private VEvent(EventId id,Title title, Status status): base(id)
        {
            Title = title;
            Status = status;
        }
        public static VEvent Create(EventId id, Title title, Status status)
        {
            return new VEvent(id,title,status);
        }
        public Result<Title> UpdateTitle(Title title)
        {
           if (title == null) return new Result<Title>(10, "Title must be set");
           if(Status.Value == Status.StatusEnum.Active) return new Result<Title>(11, "Title cannot be updated when status is active.");
           if(Status.Value == Status.StatusEnum.Cancelled) return new Result<Title>(12, "Title cannot be updated when status is cancelled.");

           Title = title;
           Status = Status.Create(Status.StatusEnum.Draft).payLoad;
           return new Result<Title>(Title);
        }
        public Result<Description> UpdateDescription(Description description)
        {
            if (Status.Value == Status.StatusEnum.Active) return new Result<Description>(21, "Description cannot be updated when status is active.");
            if (Status.Value == Status.StatusEnum.Cancelled) return new Result<Description>(22, "Description cannot be updated when status is cancelled.");

            Description = description;
            Status = Status.Create(Status.StatusEnum.Draft).payLoad;
            return new Result<Description>(Description);
        }
        public Result<EventDuration> UpdateDuration(EventDuration duration)
        {
            if (Status.Value == Status.StatusEnum.Active) return new Result<EventDuration>(36, "Duration cannot be updated when status is active.");
            if (Status.Value == Status.StatusEnum.Cancelled) return new Result<EventDuration>(37, "Duration cannot be updated when status is cancelled.");
            if(duration.From < DateTime.Now) return new Result<EventDuration>(38, "The 'From' date must be in the future");
            Duration = duration;
            Status = Status.Create(Status.StatusEnum.Draft).payLoad;
            return new Result<EventDuration>(Duration);
        }
        public Result<Status> UpdateStatus(Status status)
        {
            // New draft status is valid only if the existing status is also draft.
            if (status.Value == Status.StatusEnum.Draft && Status.Value != Status.StatusEnum.Draft)
            {
                return new Result<Status>(2, "Invalid status transition");
            }
            // New ready status is valid if the existing status is draft or ready and all required fields are set.
            if (status.Value == Status.StatusEnum.Ready)
            {
                if (Status.Value != Status.StatusEnum.Draft && Status.Value != Status.StatusEnum.Ready)
                {
                    return new Result<Status>(2, "Invalid status transition");
                }
                else if (Title == null || Duration == null || LocationId == null || Description == null || Visibility == null || MaxNumberOfGuests == null)
                {
                    return new Result<Status>(3, "Title, duration, location, description, visiblity and the max number of guests must be set before setting status to ready");
                }
            }
            // New active status is valid if the existing status is ready.
            if (status.Value == Status.StatusEnum.Active && Status.Value != Status.StatusEnum.Ready)
            {
                return new Result<Status>(2, "Invalid status transition");
            }
            // New cancelled status is always valid.

            Status = status;
            return new Result<Status>(Status);
        }
        public void UpdateVisibility(Visibility visibility)
        {
            Visibility = visibility;
        }
        public void UpdateMaxNumberOfGuests(MaxNumberOfGuests maxNumberOfGuests)
        {
            MaxNumberOfGuests = maxNumberOfGuests;
        }
        public void UpdateLocationId(LocationId locationId)
        {
            LocationId = locationId;
        }

    }
}
