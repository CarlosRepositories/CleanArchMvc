using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.DomainUnitTests
{
	public class CategoryTest
	{
		[Fact]
		public void CreateCategory_WithValidParameters_ValidObject()
		{
			// arrange act
			Action action = () => new Category(1, "Category Name Test");

			//assert
			action.Should().NotThrow<Domain.Validation.DomainExceptionValidation>();
		}

		[Fact]
		public void CreateCategory_WithNegativeId_DomainExceptionValidation()
		{
			// arrange act
			Action action = () => new Category(-1, "Category Name Test");

			//assert
			action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
				.WithMessage("Invalid Id value");
		}

		[Fact]
		public void CreateCategory_WithNullName_DomainExceptionValidation()
		{
			// arrange act
			Action action = () => new Category(-1, null);

			//assert
			action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
				.WithMessage("Invalid Id value");
		}

		[Fact]
		public void CreateCategory_MissingName_DomainExceptionValidation()
		{
			// arrange act
			Action action = () => new Category(1, "");

			//assert
			action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
				.WithMessage("Invalid name. Name is required.");
		}

		[Fact]
		public void CreateCategory_WithShortName_DomainExceptionValidation()
		{
			// arrange act
			Action action = () => new Category(1, "ab");

			//assert
			action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
				.WithMessage("Invalid name. Minimum 3 characters.");
		}
	}
}