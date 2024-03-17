using AccountOwnerServer.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountOwnerServerUnitTest
{
    public class OwnerIdValidationTest
    {
        private readonly OwnerIdValidation _validation;
        public OwnerIdValidationTest() => _validation = new OwnerIdValidation();
        
        [Fact]
        public void IsValid_ValidAccountNumber_ReturnsTrue()
            => Assert.True(_validation.IsValid("08dc45a0-5b87-4c02-8695-901e7f2a04d8"));

        [Theory]
        [InlineData("08dc45a-5b87-4c02-8695-901e7f2a04d8")]
        [InlineData("08dc45a01-5b87-4c02-8695-901e7f2a04d8")]
        [InlineData("08dc45ag-5b87-4c02-8695-901e7f2a04d8")]
        public void IsValid_AccountNumberFirstPartWrong_ReturnsFalse(string accountNumber)
    => Assert.False(_validation.IsValid(accountNumber));

        [Theory]
        [InlineData("08dc45a0-5b873-4c02-8695-901e7f2a04d8")]
        [InlineData("08dc45a0-5b8-4c02-8695-901e7f2a04d8")]
        [InlineData("08dc45a0-5b8g-4c02-8695-901e7f2a04d8")]
        public void IsValid_AccountNumberSecondPartWrong_ReturnsFalse(string accountNumber)
    => Assert.False(_validation.IsValid(accountNumber));

        [Theory]
        [InlineData("08dc45a0+5b87-4c02-8695-901e7f2a04d8")]
        public void IsValid_InvalidDelimiters_ThrowsArgumentException(string accNumber)
    => Assert.Throws<ArgumentException>(() => _validation.IsValid(accNumber));
    }
}
