using FluentAssertions;
using FluentValidation;
using HMSBusinessLogic.Helpers.Mappers;
using HMSBusinessLogic.Manager.Identity;
using HMSBusinessLogic.Manager.Patient;
using HMSBusinessLogic.Services.GeneralServices;
using HMSBusinessLogic.Services.PatientService;
using HMSContracts.Constants;
using HMSContracts.Model.Identity;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;
using HMSDataAccess.Repo.Patient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using static HMSContracts.Constants.SysEnums;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;
using static HMSContracts.Language.Resource;

namespace HMSUnitTest.Managers
{
    public class PatientsManagerTest
    {
        private Mock<UserManager<UserEntity>> _mockUserManagerIdentity;
        private Mock<IValidator<UserModel>> _mockValidator;
        private Mock<IFileService> _mockFileService;
        private Mock<IUserManager> _mockUserManager;
        private Mock<IPatientRepo> _mockPatientRepo;
        private Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private Mock<IPatientService> _mockPatientEntityService;
        private PatientsManager _patientsManager;

        public PatientsManagerTest()
        {
            _mockUserManagerIdentity = new Mock<UserManager<UserEntity>>(
                Mock.Of<IUserStore<UserEntity>>(), null, null, null, null, null, null, null, null);
            _mockValidator = new Mock<IValidator<UserModel>>();
            _mockFileService = new Mock<IFileService>();
            _mockUserManager = new Mock<IUserManager>();
            _mockPatientRepo = new Mock<IPatientRepo>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
                Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);
            _mockPatientEntityService = new Mock<IPatientService>();

            _patientsManager = new PatientsManager(
                _mockUserManagerIdentity.Object,
                _mockValidator.Object,
                _mockFileService.Object,
                _mockUserManager.Object,
                _mockPatientRepo.Object,
                _mockRoleManager.Object,
                _mockPatientEntityService.Object);
        }

        [Fact]
        public async Task GetPatient_NotValidId_ThrowException()
        {

            Func<Task> act = () => _patientsManager.GetPatientById("waaaaaaaw");

            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(UseDoesnotExist);
        }

        [Fact]
        public async Task GetPatient_ValidId_returnPatient()
        {
            var _id = "1";
            var _userName = "nada";

            _mockPatientRepo
                .Setup(x => x.GetPatientById(It.IsAny<string>()))
                .ReturnsAsync(new PatientEntity() { Id = _id, UserName = _userName });

            var patient = await _patientsManager.GetPatientById(_id);

            patient.Should().NotBeNull();
            patient.Id.Should().Be(_id);
            patient.UserName.Should().Be(_userName);
            _mockPatientRepo.Verify(x => x.GetPatientById(It.IsAny<string>()), Times.Exactly(1));
        }

        [Fact]
        public async Task DeletePatient_ValidId_DeleteThePatient()
        {
            
            _patientsManager.DeletePatient("12345");

            _mockUserManager.Verify(a=>a.DeleteUser(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetAllPatient()
        {
            var _id = "1";
            var _userName = "nada";

            var Patients = new List<PatientEntity>()
                {
                    new () { Id = _id, UserName = _userName },
                    new (){Id = _id, UserName = _userName}
                };

            _mockPatientRepo
                .Setup(x => x.GetAllPatients())
                .ReturnsAsync(Patients);

            var patient = _patientsManager.GetAllPatients();

            patient.Should().NotBeNull();
            _mockPatientRepo.Verify(x => x.GetAllPatients(), Times.Once);
        }

        [Fact]
        public async Task RegesterPatient_PatientRoleNotExist_ThrowNotFoundExeption()
        {
            var patientModel = new PatientModel();

            _mockRoleManager
               .Setup(x => x.RoleExistsAsync(It.IsAny<string>()))
               .ReturnsAsync(false);

            var act = () => _patientsManager.RegisterPatient(patientModel);

            await act.Should()
                 .ThrowAsync<NotFoundException>()
                 .WithMessage(RolePatientDoesNotExist);
        }

        [Fact]
        public async Task RegesterPatient_PatientRoleExist_CreatePatient()
        {
            var patientModel = new PatientModel()
            {
                Id = "Nada123abc",
                UserName = "Patient1",
                Address = "Kafr",
                Age = 20,
                Allergies = ["Cats"],
                BloodGroup = "O+",
                Email = "Patient1.123@gmail.com",
                Gender = (char)Gender.M,
                MedicalHistory = ["cold"],
                Phone = "01019821370",
                Password = "Nada.123@"
            };

            _mockRoleManager
               .Setup(x => x.RoleExistsAsync(SysConstants.Patient))
               .ReturnsAsync(true);

            _mockFileService
                .Setup(f => f.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("mock-image-path");

            var patientEntity = patientModel.ToEntity();

            _mockUserManagerIdentity
                     .Setup(a => a.CreateAsync(patientEntity, patientModel.Password))
                     .ReturnsAsync(IdentityResult.Success);

            _mockUserManagerIdentity
                .Setup(a => a.AddToRoleAsync(patientEntity, SysConstants.Patient))
                .ReturnsAsync(IdentityResult.Success);


            var patientResource = await _patientsManager.RegisterPatient(patientModel);

            patientResource.Should().NotBeNull();
            patientResource.UserName.Should().Be("Patient1");

        }


        [Fact]
        public async Task UpdatePatient_ThroghValidModel_UpdateEntity()
        {

            var Email = "Patient2222.123@gmail.com";
            var _id = "n199815gg89gfcbhfrw";
            var patientEntity = new PatientEntity();

            var patientModel = new PatientModel
            {
                Id = _id,
                UserName = "Patient1232",
                Address = "Kafr",
                Age = 20,
                Allergies = ["Cats"],
                BloodGroup = "O+",
                Email = "Patient2522.123@gmail.com",
                Gender = 'M',
                MedicalHistory = ["cold"],
                Phone = "01019821370"
            };

            patientEntity = patientModel.ToEntity();

            _mockPatientRepo.Setup(a => a.GetPatientById(_id))
                .ReturnsAsync(patientEntity);

            patientEntity.Email = Email;
           
            _mockPatientRepo.Setup(a => a.UpdatePatient(patientEntity));

           await _patientsManager.UpdatePatient(patientModel.Id, patientModel);

            patientEntity.Should().NotBeNull();
            patientEntity.Should().BeOfType<PatientEntity>();
            patientEntity.Email.Should().Be(Email);
            _mockPatientRepo.Verify(x => x.GetPatientById(It.IsAny<string>()), Times.Exactly(1));
        }


        [Fact]
        public async void UpdatePatient_NotValidModel_ThrowNotFoundException()
        {

            var Email = "Patient2222.123@gmail.com";
            var _id = "n199815gg89gfcbhfrw";
            var patientEntity = new PatientEntity();

            var patientModel = new PatientModel
            {
                Id = _id,
                UserName = "Patient1232",
                Address = "Kafr",
                Age = 20,
                Allergies = ["Cats"],
                BloodGroup = "O+",
                Email = "Patient2522.123@gmail.com",
                Gender = 'M',
                MedicalHistory = ["cold"],
                Phone = "01019821370"
            };

            patientEntity = patientModel.ToEntity();

            _mockPatientRepo.Setup(a => a.GetPatientById(_id))
                .ThrowsAsync(new NotFoundException(UseDoesnotExist));
                
            patientEntity.Email = Email;

            _mockPatientRepo.Setup(a => a.UpdatePatient(patientEntity));

            Func<Task> act = async () => await _patientsManager.UpdatePatient(patientModel.Id, patientModel);

            await act.Should().ThrowAsync<NotFoundException>().WithMessage(UseDoesnotExist);
           
            _mockPatientRepo.Verify(x => x.GetPatientById(It.IsAny<string>()), Times.Exactly(1));
        }

    }
}
