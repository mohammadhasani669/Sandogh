using ApplicationTest.Builders;
using Sandogh.Application.Emails;
using Sandogh.Domain.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationTest.EmailsTests
{
    public class GetListOfEmail
    {
        [Fact]
        public void List_return_Emails()
        {
            //Arrange
            DatabaseBuilder databaseBuilder = new DatabaseBuilder();
            var dbContext = databaseBuilder.GetDbContext();

           

            var service = new EmailService(dbContext);
            service.Insert(new Email 
            {
                Body = "Body",
                Code = "1234",
                Id = 1,
                Subject ="test",
                To = "anybody@gmail.com"
            });
            service.Insert(new Email
            {
                Body = "Body",
                Code = "1234",
                Id = 2,
                Subject = "test",
                To = "anybody@gmail.com"
            });

            //Assert
            var result = service.GetAll();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
