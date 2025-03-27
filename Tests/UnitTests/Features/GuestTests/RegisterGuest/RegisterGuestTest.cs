using ViaEventAssociation.Core.Domain.Aggregates.Guests;
using ViaEventAssociation.Core.Domain.Aggregates.Guests.Values;

namespace UnitTests.Features.GuestTests.RegisterGuest;

public class RegisterGuestTest
{
    // ✅ Test successful guest registration
    [Fact]
    public void RegisterGuest_WithValidData()
    {
        // Arrange
        GuestId id = GuestId.Create(Guid.NewGuid()).payLoad;
        GuestName guestName = GuestName.Create("John", "Doe").payLoad;
        Email email = Email.Create("john@via.dk").payLoad;
        ProfilePictureUrl profilePictureUrl = ProfilePictureUrl.Create("https://example.com/profile.jpg").payLoad;

        // Act
        Guest guest = Guest.RegisterGuest(id, guestName, email, profilePictureUrl);

        // Assert
        Assert.NotNull(guest);
        Assert.Equal(id, guest.Id);
        Assert.Equal(guestName, guest.Name);
        Assert.Equal(email, guest.Email);
        Assert.Equal(profilePictureUrl, guest.ProfilePictureUrl);
    }
}
