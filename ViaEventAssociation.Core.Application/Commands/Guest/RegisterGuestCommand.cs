﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.Guest {
    public class RegisterGuestCommand {
        public Email Email { get; }
        public GuestName Name { get; }
        public ProfilePictureUrl ProfilePictureUrl { get; }

        private RegisterGuestCommand(Email email, GuestName name, ProfilePictureUrl profilePictureUrl) {
            Email = email;
            Name = name;
            ProfilePictureUrl = profilePictureUrl;
        }
        public static Result<RegisterGuestCommand> Create(string email, string Fname, string Lname, string profilePictureUrl) {
            Result<Email> emailResult = Email.Create(email);
            Result<GuestName> nameResult = GuestName.Create(Fname, Lname);
            Result<ProfilePictureUrl> profilePictureUrlResult = ProfilePictureUrl.Create(profilePictureUrl);
            return null; // Todo:  //Result<RegisterGuestCommand>.CombineResultsInto(emailResult, nameResult, profilePictureUrlResult).WithPayLoadIfSuccess(() => new RegisterGuestCommand(emailResult.payLoad, nameResult.payLoad, profilePictureUrlResult.payLoad));
        }
    }
}
