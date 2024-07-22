using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace PAKDial.Implementation.PakDialServices
{
    public class PaymentModesService : IPaymentModesService
    {
        private readonly IPaymentModesRepository paymentModesRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public PaymentModesService(IPaymentModesRepository paymentModesRepository,
            IHostingEnvironment hostingEnvironment)
        {
            this.paymentModesRepository = paymentModesRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public decimal Add(PaymentModes instance)
        {
            decimal Result = 0;
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            bool result = paymentModesRepository.CheckExistance(instance);
            if(!result)
            {
                paymentModesRepository.Add(instance);
                Result = paymentModesRepository.SaveChanges();
                if(Result == 1)
                {
                    Result = instance.Id;
                }
            }
            else
            {
                Result = -2;
            }
            return Result;
        }

        public int Update(PaymentModes instance)
        {
            int Result = 0;
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            bool result = paymentModesRepository.CheckExistance(instance);
            if (!result)
            {
                var EditMode = FindById(instance.Id);
                EditMode.Name = instance.Name;
                EditMode.Description = instance.Description;
                EditMode.UpdatedBy = instance.UpdatedBy;
                EditMode.UpdatedDate = instance.UpdatedDate;
                EditMode.IsActive = instance.IsActive;
                paymentModesRepository.Update(EditMode);
                Result = paymentModesRepository.SaveChanges(); 
            }
            else
            {
                Result = -2;
            }
            return Result;
        }

        public int Delete(decimal Id)
        {
            int Results = 0;
            var Icons = Path.Combine(new string[]
                  {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","PaymentsModes",Id.ToString()
                  });
            if (Id > 0)
            {
                paymentModesRepository.Delete(FindById(Id));
                Results = paymentModesRepository.SaveChanges();
            }
            else
            {
                Results = -2;
            }

            if (Results == 1)
            {
                if (Directory.Exists(Icons))
                {
                    Directory.Delete(Icons, true);
                }
            }
            return Results;
        }

        public PaymentModes FindById(decimal id)
        {
            return paymentModesRepository.Find(id);
        }

        public IEnumerable<PaymentModes> GetAll()
        {
            return paymentModesRepository.GetAll();
        }

        public PaymentModesResponse GetPaymentModes(PaymentModesRequestModel request)
        {
            return paymentModesRepository.GetPaymentModes(request);
        }

        public GetPaymentModesResponse GetPaymentModesList(PaymentModesRequestModel request)
        {
            return paymentModesRepository.GetPaymentModesList(request);
        }

        public int UploadIcons(decimal Id, IFormFile file, string AbsolutePath)
        {
            int Save = 0;
            var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
            var ExistingIcons = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","PaymentsModes",Id.ToString()
                    });
            if (Directory.Exists(ExistingIcons))
            {
                Directory.Delete(ExistingIcons, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","PaymentsModes",Id.ToString(),
                           Guid.NewGuid() + filename
                    });

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            path = path.Replace(hostingEnvironment.WebRootPath, "").Replace("\\", "/");
            if (!string.IsNullOrEmpty(path))
            {
                paymentModesRepository.UploadImages(Id, path, AbsolutePath + path);
                Save = paymentModesRepository.SaveChanges();
            }
            return Save;
        }
    }
}
