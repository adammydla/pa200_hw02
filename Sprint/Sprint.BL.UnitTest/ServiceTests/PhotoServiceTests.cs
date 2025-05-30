using AutoMapper;
using FluentAssertions;
using Moq;
using Sprint.BL.Dto.TrainerPhoto;
using Sprint.BL.Dto.Trainer;
using Sprint.BL.Dto.User;
using Sprint.BL.Services;
using Sprint.BL.Services.Interfaces;
using Sprint.DAL.EFCore.Models;
using Sprint.Infrastructure.UnitOfWork;

namespace Sprint.BL.UnitTest.ServiceTests;

public class PhotoServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<ITrainerService> _trainerServiceMock;

    private readonly User _user;
    private readonly UserDto _userDto;
    private Trainer _trainer;
    private readonly TrainerDto _trainerDto;
    private TrainerPhoto _trainerPhoto;
    private TrainerPhotoDto _trainerPhotoDto;
    private readonly Guid _userGuid = Guid.NewGuid();
    private readonly Guid _trainerGuid = Guid.NewGuid();

    private readonly string _profilePhotoPath = "https://i.redd.it/suza5hkmcyp61.png";


    public PhotoServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _userServiceMock = new Mock<IUserService>();
        _trainerServiceMock = new Mock<ITrainerService>();

        _user = new User
        {
            Id = _userGuid,
            Role = Common.Enums.UserRole.User,
            PhotoPath = null,
        };

        _trainer = new Trainer
        {
            UserId = _userGuid,
            Photos = new List<TrainerPhoto>(),
            Reservations = new List<TrainerReservation>(),
        };

        _user.Trainer = _trainer;
        _trainer.User = _user;

        _trainerDto = new TrainerDto
        {
            Id = _trainerGuid,
            Photos = null,
        };

        _userDto = new UserDto
        {
            Id = _userGuid,
            Role = Common.Enums.UserRole.Trainer,
            PhotoPath = null,
        };

        var trainerPhoto1 = new TrainerPhotoDto
        {
            Hide = false,
            Path = "https://i.kym-cdn.com/photos/images/original/001/270/032/30c.jpg"
        };

        var trainerPhoto2 = new TrainerPhotoDto
        {
            Hide = false,
            Path = "https://i.kym-cdn.com/photos/images/original/001/270/032/30c.jpg"
        };

        var trainerPhotoHidden = new TrainerPhotoDto
        {
            Hide = true,
            Path = "https://i.kym-cdn.com/photos/images/original/001/270/032/30c.jpg"
        };

        _trainerDto.Photos = new List<TrainerPhotoDto>
        {
            trainerPhoto1, trainerPhoto2, trainerPhotoHidden,
        };

        _trainerDto.User = _userDto;
        _userDto.Trainer = _trainerDto;
    }

    [Fact]
    public async Task GetProfilePhotoAsync_Valid()
    {
        _userServiceMock
            .Setup(x => x.GetUserAsync(_userGuid))
            .ReturnsAsync(_userDto);

        _userDto.PhotoPath = _profilePhotoPath;

        var service = new PhotoService(
            _unitOfWorkMock.Object,
            _mapperMock.Object
        );

        var path = service.GetProfilePhoto(_userDto);
        path.Should().Be(_profilePhotoPath);
    }

    [Fact]
    public async Task AddProfilePhotoAsync_Valid()
    {
        _mapperMock
            .Setup(x => x.Map<User>(_userDto))
            .Returns(_user);

        _userServiceMock
            .Setup(x => x.GetUserAsync(_userGuid))
            .ReturnsAsync(_userDto);

        _unitOfWorkMock
            .Setup(x => x.UserRepository.Update(_user));

        var service = new PhotoService(
            _unitOfWorkMock.Object,
            _mapperMock.Object
        );

        var action = () => service.AddProfilePhotoAsync(_userDto, _profilePhotoPath);
        await action.Should().NotThrowAsync();

        var path = service.GetProfilePhoto(_userDto);
          
        path.Should().Be(_profilePhotoPath);
    }

    [Fact]
    public async Task DeleteProfilePhotoAsync_Valid()
    {
        _mapperMock
            .Setup(x => x.Map<User>(_userDto))
            .Returns(_user);

        _userServiceMock
            .Setup(x => x.GetUserAsync(_userGuid))
            .ReturnsAsync(_userDto);

        _unitOfWorkMock
            .Setup(x => x.UserRepository.Update(_user));

        _user.PhotoPath = _profilePhotoPath;

        var service = new PhotoService(
            _unitOfWorkMock.Object,
            _mapperMock.Object
        );

        var action = () => service.DeleteProfilePhotoAsync(_userDto);
        await action.Should().NotThrowAsync();

        var path = service.GetProfilePhoto(_userDto);

        path.Should().Be("");
        //path.Should().Equal(new string(""));
    }

    [Fact]
    public async Task GetTrainerPhotos_Valid()
    {
        _trainerServiceMock
            .Setup(x => x.GetTrainerAsync(_trainerGuid))
            .ReturnsAsync(_trainerDto);

        var service = new PhotoService(
            _unitOfWorkMock.Object,
            _mapperMock.Object
        );

        var result = service.GetTrainerPhotos(_trainerDto);
        // trainer has 3 photos but 1 is marked as hidden
        _trainerDto.Photos.Should().HaveCount(3);
        result.Should().HaveCount(2);
    }
}
