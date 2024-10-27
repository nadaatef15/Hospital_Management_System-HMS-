using FluentValidation;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Validators
{

   public class ImageUploadValidator : AbstractValidator<UserEntity>
    {
        public ImageUploadValidator()
        {

            RuleFor(x => x.ImagePath)
                .NotEmpty()
                .When(x => x.ImagePath == null)
                .WithMessage("You must upload a file or provide an image path.");

            RuleFor(x => x.ImagePath)
                .Must(IsValidUrl)
                .When(x => !string.IsNullOrEmpty(x.ImagePath))
                .WithMessage("The provided image path must be a valid URL.");
        }

        private bool IsValidUrl(string imagePath)
        {
            return Uri.IsWellFormedUriString(imagePath, UriKind.Absolute);
        }
    }

}

