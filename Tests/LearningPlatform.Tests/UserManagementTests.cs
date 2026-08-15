using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using learning_platform.Areas.Admin.Models;
using Xunit;

namespace LearningPlatform.Tests
{
    public class UserManagementTests
    {
        [Fact]
        public void CreateAdminViewModel_WithValidData_PassesValidation()
        {
            // Arrange
            var model = new CreateAdminViewModel
            {
                Email = "newadmin@learningplatform.com",
                Password = "AdminPassword@123",
                ConfirmPassword = "AdminPassword@123",
                Role = "Admin"
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
        public void CreateAdminViewModel_WithMismatchedPasswords_FailsValidation()
        {
            // Arrange
            var model = new CreateAdminViewModel
            {
                Email = "newadmin@learningplatform.com",
                Password = "AdminPassword@123",
                ConfirmPassword = "MismatchedPassword",
                Role = "Admin"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("ConfirmPassword"));
        }

        [Fact]
        public void CreateAdminViewModel_WithShortPassword_FailsValidation()
        {
            // Arrange
            var model = new CreateAdminViewModel
            {
                Email = "newadmin@learningplatform.com",
                Password = "short",
                ConfirmPassword = "short",
                Role = "Admin"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Password"));
        }

        [Fact]
        public void CreateAdminViewModel_WithMissingRole_FailsValidation()
        {
            // Arrange
            var model = new CreateAdminViewModel
            {
                Email = "newadmin@learningplatform.com",
                Password = "AdminPassword@123",
                ConfirmPassword = "AdminPassword@123",
                Role = "" // Missing required role
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            var isValid = Validator.TryValidateObject(model, context, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Role"));
        }
    }
}
