using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
	public sealed class Category : Entity
	{
		public string Name { get; private set; }

		public Category(string name)
		{
			ValidateDomain(name);
		}

		public Category(int id, string name)
		{
			DomainExceptionValidation
				.when(id < 0, "Invalid Id value");
			Id = id;
			ValidateDomain(name);
		}
		public ICollection<Product> Products { get; private set; }

		private void ValidateDomain(string name)
		{
			DomainExceptionValidation
				.when(string.IsNullOrEmpty(name), "Invalid name. Name is required.");
			DomainExceptionValidation.when(name.Length < 3, "Invalid name. Minimum 3 characters.");

			Name = name;
		}
		public void Update(string name)
		{
			ValidateDomain(name);
		}
	}
}
