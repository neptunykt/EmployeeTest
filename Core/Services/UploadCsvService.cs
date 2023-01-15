using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Model;
using Core.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public class UploadCsvService : ILoadService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UploadCsvService> _logger;
  

        public UploadCsvService(IMediator mediator, ILogger<UploadCsvService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task<LoadResult> ReadAsync(IFormFile upload)
        {
            var recordCount = 0;
            using (var sreader = new StreamReader(upload.OpenReadStream()))
            {
                while (!sreader.EndOfStream) //get all the content in rows 
                {
                    // read first line
                    if (recordCount == 0)
                    {
                        await sreader.ReadLineAsync();
                    }

                    var rows = (await sreader.ReadLineAsync())?.Split(',');
                    var payrollNumber = rows?[0].Trim();
                    var forenames = rows?[1].Trim();
                    var surname = rows?[2].Trim();
                    DateTime.TryParseExact(DateConverter.ConvertDateToAcceptedFormat(rows?[3], _logger), "dd/MM/yyyy", null,
                        System.Globalization.DateTimeStyles.None, out var dateOfBirth);
                    var telephone = rows?[4].Trim();
                    var mobile = rows?[5].Trim();
                    var address = rows?[6].Trim();
                    var address2 = rows?[7].Trim();
                    var postCode = rows?[8].Trim();
                    var email = rows?[9].Trim();
                    DateTime.TryParseExact(DateConverter.ConvertDateToAcceptedFormat(rows?[10], _logger), "dd/MM/yyyy", null,
                        System.Globalization.DateTimeStyles.None, out var startDate);
                    var createEmployee = new CreateEmployee
                    {
                        PayRollNumber = payrollNumber,
                        Address = address,
                        Address2 = address2,
                        DateOfBirth = dateOfBirth,
                        EmailHome = email,
                        Forenames = forenames,
                        Mobile = mobile,
                        PostCode = postCode,
                        StartDate = startDate,
                        Surname = surname,
                        Telephone = telephone
                    };
                    try
                    {
                        await _mediator.Send(createEmployee);
                        recordCount++;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation(ex.Message);
                    }
                }
            }

            return new LoadResult { Result = true, Message = $"{recordCount} employees successfully saved to database" };
        }
        
    }
}