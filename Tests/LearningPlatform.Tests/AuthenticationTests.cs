using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using learning_platform.Models.ViewModels;
using Xunit;

namespace LearningPlatform.Tests
{
    public class AuthenticationTests
    {
        [Fact]
        public void LoginViewModel_WithValidData_PassesValidation()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Email = "test@example.com",
                Password = "password123"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            // Assert
            Assert.True(isValid);
            Assert.Empty(validationResults);
        }

        [Fact]
        public void LoginViewModel_WithMissingEmail_FailsValidation()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Email = "",
                Password = "password123"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Email"));
        }

        [Fact]
        public void LoginViewModel_WithInvalidEmailFormat_FailsValidation()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Email = "invalid-email",
                Password = "password123"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Email"));
        }

        [Fact]
        public void LoginViewModel_WithMissingPassword_FailsValidation()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Email = "test@example.com",
                Password = ""
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Password"));
        }
    }
}
