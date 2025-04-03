using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestNS;

public class RegisterGuestTest
{
    // ID 10 use case.

    [Fact]
    public void RegisterGuest_WithValidData()
    {
        // Arrange S1
        Guid id = Guid.NewGuid();
        GuestName guestName = GuestName.Create("John", "Doe").payLoad;
        Email email = Email.Create("john@via.dk").payLoad;
        ProfilePictureUrl profilePictureUrl = ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad;

        // Act S1
        Result<Guest> guestResult = Guest.RegisterGuest(id, guestName, email, profilePictureUrl);

        // Assert S1
        Assert.NotNull(guestResult.payLoad);
        Assert.Equal(id, guestResult.payLoad.Id);
        Assert.Equal(guestName, guestResult.payLoad.Name);
        Assert.Equal(email, guestResult.payLoad.Email);
        Assert.Equal(profilePictureUrl, guestResult.payLoad.ProfilePictureUrl);

        // Arrange F1
        Result<Email> emailResult = Email.Create("john@via.sk");
        // Act F1
        // Assert F1
        Assert.Equal(122, emailResult.resultCode);

        // Arrange F2
        Result<Email> emailResultF2 = Email.Create("Johnvia.dk");
        // Act F2
        // Assert F2
        Assert.Equal(121, emailResultF2.resultCode);

        // Arrange F3
        Result<GuestName> guestNameResult = GuestName.Create("J", "Doe");
        // Act F3
        // Assert F3
        Assert.Equal(131, guestNameResult.resultCode);

        // Arrange F4
        Result<GuestName> guestNameResultF4 = GuestName.Create("John", "D");
        // Act F4
        // Assert F4
        Assert.Equal(133, guestNameResultF4.resultCode);

        // TODO: F5 This requires a database to check if there is an already existing email address.

        // Arrange F6
        Result<GuestName> guestNameResultF6A = GuestName.Create("Jo;hn", "Doe");
        // Act F6
        // Assert F6
        Assert.Equal(135, guestNameResultF6A.resultCode);
        Result<GuestName> guestNameResultF6B = GuestName.Create("John", "Do2e");
        Assert.Equal(136, guestNameResultF6B.resultCode);

        // Arrange F7
        Result<ProfilePictureUrl> profilePictureUrlResult = ProfilePictureUrl.Create("example.comprofile");
        // Act F7
        // Assert F7
        Assert.Equal(141, profilePictureUrlResult.resultCode);
    }
}
