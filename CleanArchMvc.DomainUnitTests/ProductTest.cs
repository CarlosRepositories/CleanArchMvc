using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.DomainUnitTests
{
	public class ProductTest
	{
		[Fact]
		public void CreateProduct_WithValidParameters_ValidObject()
		{
			//arrange act
			Action action = () => new Product(1, "NameTest", "descriptionTest", 10.75m, 1, "imageUrl");

			//assert
			action.Should().NotThrow<Domain.Validation.DomainExceptionValidation>();
		}

		[Fact]
		public void CreateProduct_WithNegativeId_DomainExceptionValidation()
		{
			//arrange act
			Action action = () => new Product(-1, "NameTest", "descriptionTest", 10.75m, 1, "imageUrl");

			//assert
			action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
				.WithMessage("Invalid Id value.");
		}

		[Fact]
		public void CreateProduct_MissingName_DomainExceptionValidation()
		{
			//arrange act

			Action action = () => new Product(1, "", "descriptionTest", 10.75m, 1, "imageUrl");

			//assert
			action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
				.WithMessage("Invalid name. Name is required.");
		}

		[Fact]
		public void CreateProduct_WithNullName_DomainExceptionValidation()
		{
			//arrange act

			Action action = () => new Product(1, null, "descriptionTest", 10.75m, 1, "imageUrl");

			//assert
			action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
				.WithMessage("Invalid name. Name is required.");
		}

		[Fact]
		public void CreateProduct_WithShortName_DomainExceptionValidation()
		{
			//arrange act

			Action action = () => new Product(1, "ab", "descriptionTest", 10.75m, 1, "imageUrl");

			//assert

			action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
				.WithMessage("Invalid name, minimum 3 characters.");
		}

		[Fact]
		public void CreateProduct_MissingDescription_DomainExceptoinValidation()
		{
			//arrange act

			Action action = () => new Product(1, "nameTest", "", 10.75m, 1, "imageUrl");

			//assert
			action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
				.WithMessage("Invalid description. Description is required.");
		}

		[Fact]
		public void CreateProduct_WithNullDescription_DomainExceptoinValidation()
		{
			//arrange act

			Action action = () => new Product(1, "nameTest", null, 10.75m, 1, "imageUrl");

			//assert
			action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
				.WithMessage("Invalid description. Description is required.");
		}

		[Fact]
		public void CreateProduct_WithShortDescription_DomainExceptoinValidation()
		{
			//arrange act

			Action action = () => new Product(1, "nameTest", "Test", 10.75m, 1, "imageUrl");

			//assert
			action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
				.WithMessage("Invalid description, minimum 5 characters.");
		}

		[Fact]
		public void CreateProduct_WithNegativePrice_DomainExceptoinValidation()
		{
			//arrange act

			Action action = () => new Product(1, "nameTest", "descriptionTest", -1, 1, "imageUrl");

			//assert
			action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
				.WithMessage("Invalid price value.");
		}

		[Fact]
		public void CreateProduct_WithNegativeStock_DomainExceptoinValidation()
		{
			//arrange act

			Action action = () => new Product(1, "nameTest", "DescriptionTest", 10.75m, -1, "imageUrl");

			//assert
			action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
				.WithMessage("Invalid stock.");
		}

		[Fact]
		public void CreateProduct_WithLongUrl_DomainExceptoinValidation()
		{
			//arrange act
			var UrlTest = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed non risus. " +
				"Suspendisse lectus tortor, dignissim sit amet, adipiscing nec, ultricies sed, dolor." +
				" Cras elementum ultrices diam. Maecenas ligula massa, varius a, semper congue, euismod non, mi. " +
				"Proin porttitor, orci nec nonummy molestie, enim est eleifend mi, non fermentum diam nisl sit amet erat. Duis semper. " +
				"Duis arcu massa, scelerisque vitae, consequat in, pretium a, enim. Pellentesque congue. " +
				"Ut in risus volutpat libero pharetra tempor. " +
				"Cras vestibulum bibendum augue. Praesent egestas leo in pede." +
				" Praesent blandit odio eu enim. Pellentesque sed dui ut augue blandit sodales. " +
				"Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; " +
				"Aliquam nibh. Mauris ac mauris sed pede pellentesque fermentum. " +
				"Maecenas adipiscing ante non diam sodales hendrerit.";

			Action action = () => new Product(1, "nameTest", "descriptionTest", 10.75m, 1, UrlTest);

			//assert
			action.Should().Throw<Domain.Validation.DomainExceptionValidation>()
				.WithMessage("Invalid URL. Url too long");
		}

		[Fact]
		public void CreateProduct_MissingUrl_DomainExceptionValidation()
		{
			//arrange act
			Action action = () => new Product(1, "NameTest", "descriptionTest", 10.75m, 1, "");

			//assert
			action.Should().NotThrow<Domain.Validation.DomainExceptionValidation>();
		}

		[Fact]
		public void CreateProduct_WithNullUrl_DomainExceptionValidation()
		{
			//arrange act
			Action action = () => new Product(1, "NameTest", "descriptionTest", 10.75m, 1, null);

			//assert
			action.Should().NotThrow<NullReferenceException>();
		}
	}
}
